using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    [SerializeField] List<AudioClip> landingSounds;
    [SerializeField] List<AudioClip> playerGrunts;
    [SerializeField] AudioClip JumpSFX;
    [SerializeField] AudioClip JumpSFXsmall;
    [SerializeField] List<AudioClip> stickySFX;

    [SerializeField] AudioClip powerUp;

    private AudioSource au;
    
    // Start is called before the first frame update
    void Start()
    {
        au = GetComponent<AudioSource>();
    }

    public void playPlayerJump()
    {
        if (JumpSFX)
        {
            au.PlayOneShot(JumpSFX, 1f);
        }
    }

    public void playPlayerJump(float f)
    {
        if (f >.3)
        {
            au.PlayOneShot(JumpSFX, 1f);
        }
        else
        {
            au.PlayOneShot(JumpSFXsmall, 1f);
        }
    }

    public void playPowerUp()
    {
        if (JumpSFX)
        {
            au.PlayOneShot(powerUp, 1f);
        }
    }

    public void playPlayerStick()
    {
        if (stickySFX.Count > 0)
        {
            au.PlayOneShot(stickySFX[Random.Range(0, landingSounds.Count)], .4f);
        }
    }

    public void playLandingSound(AudioSource au)
    {
        if (landingSounds.Count > 0)
        {
            au.PlayOneShot(landingSounds[Random.Range(0, landingSounds.Count)], .4f);
        }
    }

    public void playPlayerGrunt(AudioSource au)
    {
        //float pitch = au.pitch;
        //au.pitch = Random.Range(0f, 3f);
        if (playerGrunts.Count>0)
        {
            au.PlayOneShot(playerGrunts[Random.Range(0, playerGrunts.Count)], .05f);
        }
        //au.pitch = pitch;
    }

}
