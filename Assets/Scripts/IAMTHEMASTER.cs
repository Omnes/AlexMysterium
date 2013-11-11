using UnityEngine;
using System.Collections;

public class IAMTHEMASTER : MonoBehaviour {
	
	//public AudioClip MASTERSOUNDMIND;
	//public AudioSource MASTERSOUNDMIND;
	//private bool visiblePlaying = false;
	//private bool mousePlaying = false;
	
	/*void OnBecameVisible(){
		AudioSource.PlayClipAtPoint(MASTERSOUNDMIND,Vector3.zero);
		
	}*/
	
	/*void Start()
    {
        audio.Stop();
    }*/
	
	void OnMouseEnter(){
		if(!audio.isPlaying)
		{
			//AudioSource.PlayClipAtPoint(MASTERSOUNDMIND,Vector3.zero);
			//audio.clip = AudioSource.PlayClipAtPoint(MASTERSOUNDMIND,Camera.main.transform.position);
			audio.Play();
		}
		
	}
	
	void OnMouseExit(){
		if(audio.isPlaying)
		{
			audio.Stop();
		}
	}
	
}
