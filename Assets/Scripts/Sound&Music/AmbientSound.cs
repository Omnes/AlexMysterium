using UnityEngine;
using System.Collections;

public class AmbientSound : MonoBehaviour {

	public AudioClip[] audioList;// minst 2 ljud
	public AudioSource speaker;
	
	public int currentIndex = 0;
	public float pan;
	public float maxPan = 1.0f;
	public float minPan = -1.0f;
	
	public int minDelay;
	public int maxDelay;
	public float delay;
	// Use this for initialization
	void Start () {
		speaker = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!speaker.isPlaying){
			if(delay <= 0){
				nextSound();
				randomDelay();
			}
			delay -= Time.deltaTime;
		}
	}
	
	private void nextSound(){ // pick another random sound, not the same as the last one.
		int index = 0;
		randomPan();// randomize the pan
		do{
			index = Random.Range(0, audioList.Length);
		}while(index == currentIndex);
		currentIndex = index;
		speaker.clip = audioList[currentIndex];
		speaker.pan = pan;
		speaker.Play();
	}
	
	private void randomPan(){
		pan = Random.Range(minPan,maxPan);
	}
	
	private void randomDelay(){
		delay = Random.Range (minDelay, maxDelay);
		if(delay < 0){
			delay = -delay;
		}
	}
}
