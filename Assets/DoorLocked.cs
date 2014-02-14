using UnityEngine;
using System.Collections;

public class DoorLocked : MonoBehaviour {

	public AudioSource m_audioSource;
	public AudioClip m_doorLocked_sound;

	public void playDoorLocked_corridor1(){
		m_audioSource.clip = m_doorLocked_sound;
		m_audioSource.Play();
	}
}
