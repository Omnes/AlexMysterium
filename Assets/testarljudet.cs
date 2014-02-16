using UnityEngine;
using System.Collections;

public class testarljudet : MonoBehaviour {
	
	private MessageWindow meswin;

	// Use this for initialization
	void Start () {
		
		meswin = GameObject.Find("MasterMind").GetComponent<MessageWindow>(); 
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Interact(){
		
		//Debug.Log ("Interracted");
		meswin.addQuest("1");
		//Debug.Log("ljud spelats");
	
	}
}
