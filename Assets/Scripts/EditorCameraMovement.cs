using UnityEngine;
using System.Collections;

// edited from: https://gamedev.stackexchange.com/questions/104693/how-to-use-input-getaxismouse-x-y-to-rotate-the-camera

public class EditorCameraMovement : MonoBehaviour
{
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public bool mouseControlActive = false;

    void Start(){
        if(Application.isEditor){
            mouseControlActive = true;
        }
    }

    void Update()
    {
        if (mouseControlActive)
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }
}