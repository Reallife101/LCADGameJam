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

    public void enterStickyMode()
    {
        exitPowerMode();
        stickyMode = true;
    }
    public void exitStickyMode()
    {
        stickyMode = false;
    }

    public void enterPowerMode()
    {
        barTransform = 1.5f;
        currentLaunchPower = launchPower * 1.5f;
        exitStickyMode();
    }
    public void exitPowerMode()
    {
        barTransform = 1;
        currentLaunchPower = launchPower;
    }

    public void setCanJump(bool b)
    {
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
        rb.AddForce(v * currentLaunchPower, ForceMode2D.Impulse);
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
        }

    }


}
