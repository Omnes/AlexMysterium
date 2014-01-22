using UnityEngine;
using System.Collections;

public class Sound_WalkingManager : MonoBehaviour {

	public AudioClip[] audioList;// minst 2 ljud
	public AudioSource speaker;
	public Animationator AnimationatorRef; // reference aqquired from animationator
	
	public int currentIndex = 0;
	// Use this for initialization
	void Start () {
		AnimationatorRef = GameObject.FindWithTag("Player").GetComponent<Animationator>();
		speaker = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(AnimationatorRef.walk){
			if(!speaker.isPlaying){
				nextSound();
			}
		}
	}
	
	void nextSound(){ // pick another random sound, not the same as the last one.
		int index = 0;
		do{
			index = Random.Range(0, audioList.Length);
		}while(index == currentIndex);
		currentIndex = index;
		speaker.clip = audioList[currentIndex];
		speaker.Play();
	}
}
