using UnityEngine;
using System.Collections;

public class Radio : MonoBehaviour {
	
	public AudioClip mainClip;
	public AudioClip powerfailureClip;
	private AudioSource speaker;
	public bool enabled = false;
	public bool avbrott = false;

	void Start () {
		speaker = gameObject.GetComponent<AudioSource>();
		speaker.clip = mainClip;
	}

	void Interact(){
		if(!avbrott){
			enabled = !enabled;
			if(enabled){
				if(!speaker.isPlaying){
					speaker.Play();
				}
			}else{
				speaker.Pause();
			}
		}
	}

	void setPower(bool powerOff){
		avbrott = powerOff;
		if(powerOff){
			if(enabled){
				speaker.clip = powerfailureClip;
				speaker.Play();
				speaker.loop = false;
			}
		}else{
			if(enabled){
				speaker.clip = mainClip;
				speaker.Play();
				speaker.loop = true;
			}
		}
	}

	//eh, this is a bit odd but okey, should revert this
	void setPowerOut(bool power){
		Debug.Log("Radio: powerOut!");
		setPower(true);
	}

}