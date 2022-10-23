using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float launchPower = 10f;
    public Transform bar;
    public SpriteRenderer sr;
    public SpriteRenderer player;
    public Animator ai;
    public Transform groundCheck;

    [SerializeField] GameObject smoke;
    [SerializeField] List<GameObject> crashes;
    [SerializeField] LayerMask lm;

    private float oldX;
    private float oldY;

    private Rigidbody2D rb;
    private Vector2 jumpVector;
    private Vector3 mousePos;
    private bool canJump;
    private bool stickyMode;
    private float barTransform;
    private float currentLaunchPower;
    private Vector3 oldPosition;
    private float timeElapsed;
    private AudioSource au;
    private audioManager am;

    public void enterStickyMode()
    {
        exitPowerMode();
        stickyMode = true;
        //am.playPowerUp();
        player.color = new Color(255, 255, 0);
    }
    public void exitStickyMode()
    {
        stickyMode = false;
        player.color = new Color(255, 255, 255);
    }

    public void enterPowerMode()
    {
        barTransform = 1.5f;
        currentLaunchPower = launchPower * 1.5f;
        exitStickyMode();
        player.color = new Color(255, 0, 0);
    }
    public void exitPowerMode()
    {
        barTransform = 1;
        currentLaunchPower = launchPower;
        player.color = new Color(255, 255, 255);
    }

    public void setCanJump(bool b)
    {
        if (b)
        {
            player.color = new Color(0, 255, 228);
        }
        else
        {
            player.color = new Color(255, 255, 255);
        }

        canJump = b;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        oldX = 0;
        oldY = 0;
        canJump = true;
        bar.gameObject.SetActive(false);
        barTransform = 1;
        currentLaunchPower = launchPower;
        oldPosition = transform.position;
        timeElapsed = 0;
        au = GetComponent<AudioSource>();
        am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<audioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse Position
        mousePos = Input.mousePosition;

        //Deal with being stuck
        if (Mathf.Approximately(transform.position.x, oldPosition.x) && Mathf.Approximately(transform.position.y, oldPosition.y))
        {
            timeElapsed += Time.deltaTime;
        }
        else
        {
            timeElapsed = 0;
            oldPosition = transform.position;
        }

        if (!canJump && timeElapsed >= 0.5f)
        {
            canJump = true;
            Debug.Log("Fix!");
            ai.SetBool("isJumping", false);
        }

        // Handle Color Change
        if (canJump)
        {
            sr.color = Color.green;
        }
        else
        {
            sr.color = Color.red;
        }

        // Handle drawing the vector
        if (Input.GetButtonDown("Fire1"))
        {
            // begin recording the jump vector
            oldX = mousePos.x;
            oldY = mousePos.y;
            bar.gameObject.SetActive(true);
            ai.SetBool("isCrouching", true);
            ai.SetBool("Squish", false);

        }

        // Set Current Jump Vector based on let go position
        jumpVector = new Vector2((oldX - mousePos.x)/(Screen.width/4), (oldY - mousePos.y)/(Screen.height/4));

        setJumBar(jumpVector);

        if (Input.GetButtonUp("Fire1"))
        {
            bar.gameObject.SetActive(false);
            if (canJump)
            {
                jump(jumpVector);
            }

            ai.SetBool("isCrouching", false);
        }

    }

    // Applies force to player
    void jump(Vector2 v)
    {
        // turn gravity back on aftr jumping if sticky
        if (stickyMode)
        {
            rb.gravityScale = 1;
        }

        //Normalize anything that is too big
        if (v.magnitude >1)
        {
            v.Normalize();
        }
        //Account for "Bad jumps" and stop them
        else if (v.y <= 0 || v.magnitude < 0.1f)
        {
            return;
        }

        ai.SetBool("isJumping", true);
        am.playPlayerJump();
        am.playPlayerGrunt(au);
        rb.AddForce(v * currentLaunchPower, ForceMode2D.Impulse);
        GameObject go = Instantiate(smoke, new Vector3(transform.position.x, transform.position.y, -0.1f), Random.rotation);
        float mag = Mathf.Max(v.magnitude, .25f);
        go.transform.localScale = new Vector3(mag, mag, mag);
        
        if (v.magnitude > 0.2f)
        {
            timeElapsed = 0f;
            canJump = false;
        }


        //debug 
        //Debug.Log(v.magnitude);
        //Debug.Log(Vector2.Angle(new Vector2(1, 0), v));
    }

    //Change Angle of Jumpbar and length
    void setJumBar(Vector2 v)
    {
        float rotation = Vector2.Angle(new Vector2(1, 0), v);

        if (v.y < 0)
        {
            rotation = 360f - rotation;
        }
        
        bar.eulerAngles = new Vector3(
        bar.eulerAngles.x,
        bar.eulerAngles.y,
        rotation
        );

        bar.localScale = new Vector3(Mathf.Min(v.magnitude, barTransform),  bar.localScale.y,  bar.localScale.z);

        // Set Direction of player
        if (bar.gameObject.activeInHierarchy)
        {
            if (v.x < 0)
            {
                player.flipX = true;
            }
            else
            {
                player.flipX = false;
            }
        }
        
    }

    // Reset Jump
    void OnCollisionEnter2D(Collision2D col)
    {
        canJump = true;
        ai.SetBool("isJumping", false);

        //If sticky stop and turn off gravity
        if (stickyMode)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            if (!(Physics2D.OverlapCircle(new Vector2(groundCheck.position.x, groundCheck.position.y), .25f, ~lm)))
            {
                ai.SetBool("Squish", true);
            }

            am.playPlayerStick();
        }
        else
        {
            am.playLandingSound(au);
            GameObject go = Instantiate(crashes[Random.Range(0, crashes.Count)], new Vector3(transform.position.x, transform.position.y -.5f, -0.1f), Random.rotation);
            float mag = Mathf.Min(rb.velocity.magnitude/5, .5f);
            go.transform.localScale = new Vector3(mag, mag, mag);
        }

    }


}
