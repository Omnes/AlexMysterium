using UnityEngine;
using System.Collections;

public class LightSwitchSound : MonoBehaviour {

	private bool played = false;
	public AudioClip notWorking_sound;
	
	void Interact(){
		bool valid = GameObject.FindGameObjectWithTag("Mastermind").GetComponent<ItemUseStates>().powerout;
		if(!played && !valid){
			gameObject.audio.Play();
			played = true;
		}else{
			gameObject.audio.clip = notWorking_sound;
			audio.Play();
		}
	}
}
