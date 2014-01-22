using UnityEngine;
using System.Collections;

public class Radio : MonoBehaviour {
	
	public AudioClip audioList;
	public AudioSource speaker;
	
	public bool enabled = true;
	
	public 	float 	pan;
	
	void Awaken(){

	}
	
	// Use this for initialization
	void Start () {
		speaker = gameObject.GetComponent<AudioSource>();
	
		
	}
	
	// Update is called once per frame
	void Update () {
		if(enabled){
			if(!speaker.isPlaying){
				speaker.Play();
			}
		}
	}
	
	void Interact(){
		enabled = !enabled;
		if(enabled){
			
		}
	}
	
	private void restart(){ // pick another random sound, not the same as the last one.
		// starta upp radion igen, inte för långt ifrån där man avslutade.
	}
	
	/*private void nextPan(){
		speaker.pan = pans[panIndex];
		panIndex++;
		if(panIndex == pans.Length){
				panIndex = 0;	
		}
	}*/
	
}