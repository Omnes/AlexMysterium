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
		
		GameObject spawn = GameObject.Find(spawnpointName);
		Vector3 spawnPosition = spawn.transform.position;
			
		Transform player = (Transform)Instantiate(playerPrefab,spawnPosition,Quaternion.identity);
		Camera.main.GetComponent<InputManager>().SetPlayer(player);
		player.GetComponent<Pathfinding>().walkmesh = GameObject.Find("floor").transform;
	}
	
	void setSpawnpoint(string spawnName){
		spawnpointName = spawnName;
	}
	
}
