using UnityEngine;
using System.Collections;

public class IAMTHEMASTER : MonoBehaviour {
	
	public AudioClip MASTERSOUNDMIND;
	
	void OnBecameVisible(){
		AudioSource.PlayClipAtPoint(MASTERSOUNDMIND,Vector3.zero);
		
	}
	void OnMouseEnter(){
		AudioSource.PlayClipAtPoint(MASTERSOUNDMIND,Vector3.zero);
		
	}
	
}
