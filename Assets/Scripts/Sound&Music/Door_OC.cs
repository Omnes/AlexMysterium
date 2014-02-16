using UnityEngine;
using System.Collections;

public class Door_OC : MonoBehaviour {


	public AudioClip openSound;
	public AudioClip closeSound;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnLevelWasLoaded(){
		audio.clip = closeSound;
		audio.Play();
		//Debug.Log ("DoorWas closed");
	}

	void Interact(){
		audio.clip = openSound;
		audio.Play();
	}
}
