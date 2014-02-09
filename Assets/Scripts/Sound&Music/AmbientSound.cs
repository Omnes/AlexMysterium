using UnityEngine;
using System.Collections;

public class AmbientSound : MonoBehaviour {

	public AudioClip[] audioList;// minst 2 ljud
	public AudioSource speaker;
	
	public int currentIndex = 0;
	public int panRange;
	
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
		speaker.pan = panRange;
		speaker.Play();
	}
	
	private void randomPan(){
			panRange = Random.Range(-1,1);
	}
	
	private void randomDelay(){
		delay = Random.Range (minDelay, maxDelay);
		if(delay < 0){
			delay = -delay;
		}
	}
}
