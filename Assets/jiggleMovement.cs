using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jiggleMovement : MonoBehaviour
{
    [SerializeField] float speed;
    

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), transform.position.y, transform.position.z);

        if (transform.position.x < -50f)
        {
            transform.position = new Vector3(700, transform.position.y, transform.position.z);
        }
    }
}
