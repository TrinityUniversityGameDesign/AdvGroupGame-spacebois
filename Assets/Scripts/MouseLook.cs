using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour {

    private Vector2 mLook;
    private Vector2 smoothV;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    public GameObject character;

	// Use this for initialization
	void Start () {
        character = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        var mDelt = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        mDelt = Vector2.Scale(mDelt, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, mDelt.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, mDelt.y, 1f / smoothing);
        mLook += smoothV;

        transform.localRotation = Quaternion.AngleAxis(-mLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mLook.x, character.transform.up);
    }
}
