using UnityEngine;
using System.Collections;

public class ComputerSound_script : MonoBehaviour {

	public GameObject mastermind;
	public bool isDone = false;
	public AudioClip passSound;
	public AudioSource audioS;

	public void playSound(){
		mastermind = GameObject.FindGameObjectWithTag("Mastermind");
		bool password = mastermind.GetComponent<ItemUseStates>().password;
		if(!password && !isDone){
			audioS.clip = passSound;
			audioS.Play ();
			isDone = true;
		}
	}
}
