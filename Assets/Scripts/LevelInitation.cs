using UnityEngine;
using System.Collections;

public class LevelInitation : MonoBehaviour {
	
	public Transform playerPrefab;
	
	public string spawnpointName = "defaultSpawn";
	

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(transform.gameObject);

		OnLevelWasLoaded(); //kan ställa till saker sen!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! DANGERZONE
	}
	
	void OnLevelWasLoaded(){
		
		Debug.Log ("Level Initiation worked!");
		
		GameObject spawn = GameObject.Find(spawnpointName);
		Vector3 spawnPosition = spawn.transform.position;
			
		Transform player = (Transform)Instantiate(playerPrefab,spawnPosition,Quaternion.identity);
		GetComponent<InputManager>().SetPlayer(player);
		Camera.main.GetComponent<CameraSmoothFollowScript>().Player = player.gameObject;
		
		Transform floor = GameObject.Find("floor").transform;
		Debug.Log(floor);

		player.GetComponent<Pathfinding>().walkmesh = floor;
	}
	
	void setSpawnpoint(string spawnName){
		spawnpointName = spawnName;
	}
	
}
