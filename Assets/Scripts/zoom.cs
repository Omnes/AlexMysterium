using UnityEngine;
using System.Collections;

public class zoom : MonoBehaviour {
	
	
	public Vector3 cameraPos;
	public bool objectPositioning = true; // set to true if you want to use the object in the scene for camera positioning, or false if you want to do it the hardcore way and try and input diffrent vector3 positions
	public Transform childObject;
	
	// Use this for initialization
	void Start () {
		/*if(objectPositioning)
		{
			cameraPos = childObject.position;
		}*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public Vector3 retrieveCameraPos(){
		if(objectPositioning)
		{
			cameraPos = childObject.position;
		}
		return 	cameraPos;
	}
}
