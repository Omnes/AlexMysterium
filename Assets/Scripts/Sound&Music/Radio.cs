using UnityEngine;
using System.Collections;

public class Radio : MonoBehaviour {
	
	public AudioClip mainClip;
	public AudioClip powerfailureClip;
	public float powerOutVolume = 0.2f;
	private AudioSource speaker;
	public bool enabled = false;
	public bool avbrott = false;
	public ulong offset = 0;

	void Start () {
		speaker = gameObject.GetComponent<AudioSource>();
		speaker.clip = mainClip;
		if(enabled){
			speaker.Play(offset);
		}
	}

	void Interact(){
		if(!avbrott){
			enabled = !enabled;
			GameObject.Find ("MasterMind").GetComponent<ItemUseStates>().radio = enabled;
			if(enabled){
				if(!speaker.isPlaying){
					speaker.Play(offset);
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
				speaker.volume = powerOutVolume;
				speaker.Play();
				speaker.loop = false;
			}
		}else{
			if(enabled){
				speaker.clip = mainClip;
				speaker.volume = 1.0f;
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

	public ulong sendOffset(){
		offset = (ulong) speaker.clip.samples - (ulong) audio.timeSamples;
		return offset;
	}

	public void receiveOffset(ulong x){
		offset = x;
	}

}