using UnityEngine;
using System.Collections;

public class Ambience_powerout_effect : MonoBehaviour {

	public float FadeInTo = 1.0f;
	public float FadeOutTo = 0.0f;
	public float increment = 0.01f;
	private float speakerVolume;
	private bool m_fadeIn = false;
	private bool m_fadeOut = false;
	//private float goalVolume;

	private AudioSource speaker;
	// Use this for initialization
	void Start () {
		speaker = gameObject.GetComponent<AudioSource>();
		speakerVolume = speaker.volume;
	}
	
	// Update is called once per frame
	void Update () {
		if(m_fadeIn && !m_fadeOut){
			speakerVolume = speaker.volume;
			fadeIn();
		}else if(!m_fadeIn && m_fadeOut){
			fadeOut();
		}
	}

	void setPowerOut(bool power){
		Debug.Log ("ambience-powerout");
		if(!power){
			speakerVolume = speaker.volume;
			m_fadeIn 	= true;
			m_fadeOut = false;
		}else{
			m_fadeIn 	= false;
			m_fadeOut = true;
		}
	}

	void fadeIn(){

		speaker.volume += increment*Time.deltaTime;
		if(speaker.volume > FadeInTo){
			m_fadeIn = false;
		}
	}

	void fadeOut(){

		speaker.volume -= increment*Time.deltaTime;

		if(speaker.volume < FadeOutTo){
			m_fadeOut = false;
		}
	}
}
