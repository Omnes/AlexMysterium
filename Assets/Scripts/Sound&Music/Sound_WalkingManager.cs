using UnityEngine;
using System.Collections;

public class Sound_WalkingManager : MonoBehaviour {

	public AudioClip[] audioList;// minst 2 ljud
	public AudioSource speaker;
	public Animationator AnimationatorRef; // reference aqquired from animationator
	public float lastStepTime = 0f;
	public float stepDelay = 0.7f;
	public float time;
	
	public int currentIndex = 0;
	// Use this for initialization
	void Start () {
		AnimationatorRef = GetComponent<Animationator>();
		speaker = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(AnimationatorRef.walk){
			time += Time.deltaTime;
			if(time > lastStepTime + stepDelay){
				nextSound();
				lastStepTime = time;
			}
		}else{
			lastStepTime = time;
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
