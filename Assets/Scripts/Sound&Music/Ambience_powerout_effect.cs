using UnityEngine;
using System.Collections;

public class Ambience_powerout_effect : MonoBehaviour {

	float poweroutVolumeBuff = 1.2f;
	float speakerVolume;
	AudioSource speaker;
	// Use this for initialization
	void Start () {
		speaker = gameObject.GetComponent<AudioSource>();
		speakerVolume = speaker.volume;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void setPowerOut(bool power){
		if(power){
			speakerVolume = speaker.volume;
			fadeIn();
		}else{
			fadeOut();
		}
	}

	void fadeIn(){
		speakerVolume = speaker.volume;
		float goalVolume = speakerVolume*poweroutVolumeBuff;
		while(speaker.volume < goalVolume){
			speaker.volume += 0.1f*Time.deltaTime;
		}
	}

	void fadeOut(){
		float goalVolume = speakerVolume;
		while(speaker.volume > goalVolume){
			speaker.volume -= 0.1f*Time.deltaTime;
		}
	}
}
