using UnityEngine;
using System.Collections;

public class Droppskap_voice : MonoBehaviour {

	public AudioSource audioS;
	public AudioClip blownFuses_sound;

	public void Interact(){
		audioS.clip = blownFuses_sound;
		audioS.Play();
		//gameObject.GetComponent<zoom_TEST>().
	}
}