using UnityEngine;
using System.Collections;

public class LevelInitation : MonoBehaviour {
	
	public Transform playerPrefab;
	
	public string spawnpointName = "defaultSpawn";
	
	public Texture2D blackTexture;
	private float alphaFadeValue = 0.0f;
	public bool initiateFade = false;
	private string nextLevel;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(transform.gameObject);
		OnLevelWasLoaded(); //kan ställa till saker sen!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! DANGERZONE
	}
	
	void Update(){
		if(initiateFade){
			FadeIn();
		}else{			//it might be expensive to always diminish the fadevalue...
			FadeOut();
		}
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
	
	//spawnpoint in new level
	void setSpawnpoint(string spawnName){
		spawnpointName = spawnName;
	}

	void LoadLevel(string nextLevelName){
		initiateFade = true;
		//can we always be sure that this is the next level and not the current?
		nextLevel = nextLevelName;
	}
	
	void FadeIn(){
		alphaFadeValue = Mathf.Clamp01(alphaFadeValue + (Time.deltaTime * 2));
		if(alphaFadeValue > 0.99){
			Application.LoadLevel(nextLevel);
			initiateFade = false;
		}
	}
	
	void FadeOut(){
		if(alphaFadeValue > 0.01f){
			alphaFadeValue = Mathf.Clamp01(alphaFadeValue - (Time.deltaTime * 2));
		}
	}
	
	//create new color every GUI tick might be expensive..
	void OnGUI(){
		GUI.color = new Color(0,0,0,alphaFadeValue);
		GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), blackTexture);
	}
}
