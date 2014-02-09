using UnityEngine;
using System.Collections;

public class ActivationSound : MonoBehaviour {
	
	// Innehålla flera ljud. Länkad lista eller vector? = Array.
	// Möjligen göra så det aktiveras om man klickar på det istället, kolla om den blir träffad av en raycast.
	
	// Bool: Köra ljued bara första gången den aktiveras eller varje gång.
	// Om det finns flera ljud, köra dem i ordning eller alla samtidigt?
	//public	bool playInOrder 	= true;	// play all sounds at once or one at a time?
	public 	bool playedOnce	 	= false;// if there is only one sound, then this ensuers this is atleast played once and not stopped before that.
	public 	bool playOnce 		= true;
	private bool played 		= false;
	private bool playing 		= false;
	
	public int currentIndex = 0;
	
	public AudioClip[] audioList;
	public AudioSource speaker;
	
	public 	float[] 	delays;
	private float 		currentDelay 	= 0;
	private int 		delayIndex 		= 0;
	public 	float[] 	pans;
	private int			panIndex		= 0;
	
	void Awaken(){
		delays = new float[audioList.Length];
		pans = new float[audioList.Length];
	}
	
	// Use this for initialization
	void Start () {
		speaker = gameObject.GetComponent<AudioSource>();
		nextDelay();
	}
	
	// Update is called once per frame
	void Update () {
		if(playing){
			if(!speaker.isPlaying){
				if(currentDelay <= 0){
					nextSound();
					nextDelay();
				}
				currentDelay -= Time.deltaTime;
			}
		}
	}
	
	void Interact(){
		if(!playOnce || !played && !playing){// om båda är sanna ska man inte köra igen och man ska inte köra igen om man redan håller på och kör.
			playing = true;
			played 	= true;
		}
	}
	
	private void nextSound(){ // pick another random sound, not the same as the last one.
		if(audioList.Length > 0){
			speaker.clip = audioList[currentIndex];
			nextPan();
			//currentIndex++;
			//currentIndex %= audioList.Length;
			
			if(currentIndex == 0 && playedOnce){// have iterated over the list and is back to the beginning;
				//speaker.clip = audioList[currentIndex];
				playing 	= false;
				playedOnce 	= false;
			}else{
				speaker.Play();
				
				currentIndex++;
				if(currentIndex == audioList.Length){
					currentIndex = 0;	
				}
				
				playedOnce 	= true;
			}
		}else{
			Debug.Log("Audiolist has no soundObjects");	
		}
	}
	
	private void nextDelay(){// gets a new delay from the delay list, the reason a temporary storage space is used is because then this value can be changed without editing the delay-list
		currentDelay = delays[delayIndex];
		delayIndex++;
		if(delayIndex == delays.Length){// this is used instead of modulus since that can cause errors with diffrent size arrays, atleast that is what I rmember from testing it.
				delayIndex = 0;	
			}
	}
	
	private void nextPan(){// works basically the same way as delay except that no temp-space is needed and pan can be assigned to the speaker directly.
		speaker.pan = pans[panIndex];
		panIndex++;
		if(panIndex == pans.Length){
				panIndex = 0;
		}
	}
	
}
