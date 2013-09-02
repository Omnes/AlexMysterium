using UnityEngine;
using System.Collections;

public class Interact_Door : MonoBehaviour {
	
	public string nextLevelName;
	public string spawnpointName;

	// Use this for initialization
	void Start () {
	
	}
	
	public void Interact(){
		//sätt vilken spawnpoint som ska användas på nästa level
		GameObject masterMind = GameObject.Find("MasterMind");
		masterMind.SendMessage("setSpawnpoint",spawnpointName);
		
		Application.LoadLevel(nextLevelName);
		
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
