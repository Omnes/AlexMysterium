using UnityEngine;
using System.Collections;

public class PhotoInteract : MonoBehaviour {

	private bool played = false;

	void Interact(){
		if(!played){
			gameObject.audio.Play();
			played = true;
		}
	}
}
