﻿// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour {
	/// This is a modified javascript conversion of the Standard Assets MouseLook script.
	/// Also added is functionallity of using a key to look up, down, left and right in addition to the Mouse.
	/// Everything is on by default. You will want to turn off/on stuff depending on what you're doing.

	/// You can also modify the script to use the KeyLook functions to control an object's rotation.
	/// Try using MouseXandY on an object. Actually it works as is but you'll want to clean it up ;)

	/// As of this version the key and mouse fight if used at the same time (ie up key and down mouse jitters).

	/// Minimum and Maximum values can be used to constrain the possible rotation

	/// To make an FPS style character:
	/// - Create a capsule.
	/// - Add a rigid body to the capsule
	/// - Add the MouseLookPlus script to the capsule.
	///   -> Set the script's Axis to MouseX in the inspector. (You want to only turn character but not tilt it)
	/// - Add FPSWalker script to the capsule

	/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
	/// - Add the MouseLookPlus script to the camera.
	///   -> Set the script's Axis to MouseY in the inspector. (You want the camera to tilt up and down like a head. The character already turns.)


	enum Axes {MouseXandY, MouseX, MouseY}
	Axes Axis = Axes.MouseXandY;

	public float strafespeedX = 30;
	public float strafespeedZ = 30;

	public float sensitivityX = 15.0f;
	public float sensitivityY = 15.0f;

	public float minimumX= -360.0f;
	public float maximumX= 360.0f;

	public float minimumY= -60.0f;
	public float maximumY= 60.0f;

	public float rotationX = 0.0f;
	public float rotationY = 0.0f;

	public float lookSpeed = 1.0f;

	public Camera cameraReference;


	void  Update (){
		// Lock the cursor
		//Screen.lockCursor = true;

		// Move toward and away from the camera
	//	if (Input.GetAxis("Vertical"))
	//	{
			float translationZ= Input.GetAxis("Vertical");
			transform.localPosition += cameraReference.transform.localRotation * new Vector3(0,0,translationZ/strafespeedX);
	//	}

		// Strafe the camera
	//	if (Input.GetAxis("Horizontal"))
	//	{
			float translationX= Input.GetAxis("Horizontal");
			transform.localPosition += cameraReference.transform.localRotation * new Vector3(translationX/strafespeedZ,0,0);
	//	}       

		if (Axis == Axes.MouseXandY)
		{
			// Read the mouse input axis
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

			// Call our Adjust to 360 degrees and clamp function
			Adjust360andClamp();

			// Most likely you wouldn't do this here unless you're controlling an object's rotation.
			// Call our look left and right function.
			KeyLookAround();

			// Call our look up and down function.
			KeyLookUp();
		}
		else if (Axis == Axes.MouseX)
		{
			// Read the mouse input axis
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;

			// Call our Adjust to 360 degrees and clamp function
			Adjust360andClamp();

			// if you're doing a standard X on object Y on camera control, you'll probably want to
			// delete the key control in MouseX. Also, take the transform out of the if statement.
			// Call our look left and right function.
			KeyLookAround();

			// Call our look up and down function.
			KeyLookUp();
		}
		else
		{
			// Read the mouse input axis
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

			// Call our Adjust to 360 degrees and clamp function
			Adjust360andClamp();

			// Call our look left and right function.
			KeyLookAround();

			// Call our look up and down function.
			KeyLookUp();
		}
	}

	void  KeyLookAround (){
		//      If you're not using it, you can delete this whole function.
		//      Just be sure to delete where it's called in Update.

		// Call our Adjust to 360 degrees and clamp function
		Adjust360andClamp();

		// Transform our X angle
		transform.localRotation = Quaternion.AngleAxis (rotationX, Vector3.up);
	}

	void  KeyLookUp (){
		// Adjust for 360 degrees and clamp
		Adjust360andClamp();

		// Transform our Y angle, multiply so we don't loose our X transform
		transform.localRotation *= Quaternion.AngleAxis (rotationY, Vector3.left);
	}

	void  Adjust360andClamp (){
		//      This prevents your rotation angle from going beyond 360 degrees and also
		//      clamps the angle to the min and max values set in the Inspector.

		// During in-editor play, the Inspector won't show your angle properly due to
		// dealing with floating points. Uncomment this Debug line to see the angle in the console.

		// Debug.Log (rotationX);

		// Don't let our X go beyond 360 degrees + or -
		if (rotationX < -360)
		{
			rotationX += 360;
		}
		else if (rotationX > 360)
		{
			rotationX -= 360;
		}   

		// Don't let our Y go beyond 360 degrees + or -
		if (rotationY < -360)
		{
			rotationY += 360;
		}
		else if (rotationY > 360)
		{
			rotationY -= 360;
		}

		// Clamp our angles to the min and max set in the Inspector
		rotationX = Mathf.Clamp (rotationX, minimumX, maximumX);
		rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
	}

	void  Start (){
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
		{
			GetComponent<Rigidbody>().freezeRotation = true;
		}
	}
}