using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmActivate : MonoBehaviour
{
    public AudioSource au;

    private bool start;

    private void Start()
    {
        start = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && start)
        {
            StartCoroutine(Lerp());
            start = false;
        }
    }

    IEnumerator Lerp()
    {
        float timeElapsed = 0;
        while (timeElapsed < 2f)
        {
            au.volume = Mathf.Lerp(0, .8f, timeElapsed / 2f);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        au.volume = .8f;
    }
}
