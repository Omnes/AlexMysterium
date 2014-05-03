using UnityEngine;
using System.Collections;

public class Radio : MonoBehaviour {
	
	//public AudioClip mainClip;
	//public AudioClip powerfailureClip;
	//public float powerOutVolume = 0.2f;

	public AudioSource speaker;
	public AudioSource powerOutRadio;

	public bool enabled = false;
	public bool avbrott = false;
	public static int offset = 0;

	void Start () {
		//speaker = gameObject.GetComponent<AudioSource>();
		//speaker.clip = mainClip;
		if(enabled){
			//Debug.Log (offset);
			bool t =  GameObject.Find ("MasterMind").GetComponent<ItemUseStates>().powerout;
			if(!t){
				speaker.timeSamples = offset;
				speaker.Play();
			}
		}
	}

	void Update(){
		offset = speaker.timeSamples;
	}

	void Interact(){
		if(!avbrott){
			enabled = !enabled;
			GameObject.Find ("MasterMind").GetComponent<ItemUseStates>().radio = enabled;
			if(enabled){
				if(!speaker.isPlaying){
					speaker.timeSamples = offset;
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
				powerOutRadio.Play();
				speaker.Stop();
				enabled = false;
			}
		}else{
			if(enabled){
				//speaker.volume = 1.0f;
				speaker.timeSamples = offset;
				speaker.Play();
				speaker.loop = true;
			}
		}
	}

	//eh, this is a bit odd but okey, should revert this
	void setPowerOut(bool power){
		//Debug.Log("Radio: powerOut!");
		setPower(true);
	}

	public int sendOffset(){
		offset =  speaker.clip.samples -  speaker.timeSamples;
		return offset;
	}

	public void receiveOffset(int x){
		offset = x;
	}

}