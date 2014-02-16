using UnityEngine;
using System.Collections;

public class DroppskapZoom_voice : MonoBehaviour {

	public AudioClip fusesClue_voice;
	public AudioClip number_voice;
	public AudioSource audioS;

	public float count;
	public bool m_active = false;

	public AudioClip blownFuses_sound;
	public bool visited = false;

	public float firstClue = 500f;
	public bool firstClueBool = true;
	public float secondClue = 1000f;
	public bool secondClueBool = true;

	// Update is called once per frame
	void Update () {
		if(m_active){
			if(!visited){
				audioS.clip = blownFuses_sound;
				audioS.Play();
				visited = true;
			}

			count++;
			if(count > firstClue && firstClueBool){
				audioS.clip = fusesClue_voice;
				audioS.Play();
				firstClueBool = false;
			}
			if(count > secondClue && secondClueBool){
				audioS.clip = number_voice;
				audioS.Play();
				secondClueBool = false;
			}
		}
	}

	void ActivateStuff(){
		m_active = true;
	}
	
	void DeactivateStuff(){
		m_active = false;
	}
}
