using UnityEngine;
using System.Collections;

public class Flashlight_Movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 normPos = new Vector2(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height);
		renderer.material.SetVector("_MousePos", new Vector4(1-normPos.x,1-normPos.y,0.0f,0.0f));
	}
}
