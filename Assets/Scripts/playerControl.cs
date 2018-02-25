using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    public float speed = 10f;
    public float jumpPower = 1f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float strafe = Input.GetAxis("Horizontal") * speed;
        translation = translation*Time.deltaTime;
        strafe = strafe*Time.deltaTime;

        transform.Translate(strafe, 0, translation);
        /* a way with forces that doesnt need to be used but hey check it out
        var horiz = Input.GetAxis("Horizontal");
        var vert = Input.GetAxis("Vertical");
        rb.AddForce(new Vector3(horiz * speed, 0, vert * speed));
        */
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetButtonDown("Jump") && transform.position.y < 2)
        {
            rb.AddForce(new Vector3(0, jumpPower, 0));
        }
    }
}