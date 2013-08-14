using UnityEngine;
using System.Collections;

public class CameraSmoothFollowScript : MonoBehaviour {
	
	//Should be prefab and should initaiate new player each scene ***************************
	public GameObject Player;
	public float CameraHeightZ;
	private Vector3 playerMovement;
	private Vector3 ContactDir;
	private bool isColliding = false;
	private Vector3 ScreenMiddle;
	private Vector3 ScreenMiddleToPlayer;
	
	
	// Use this for initialization
	void Start () {
	
		playerMovement = new Vector3(Player.transform.position.x, 0.0f, CameraHeightZ);
		transform.position = playerMovement;
		
		ScreenMiddle = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);

		
	}
	
	// Update is called once per frame
	void Update () {
		
		SmoothFollow();
		
	}
	
	void OnCollisionEnter(Collision other) {
		
		//ContactDir = transform.position - other.transform.position;
		
		//ContactDir = other.contacts[0].normal;
		
		//Debug.Log("Collide");
		
		isColliding = true;
		
		
	}
	
	void OnCollisionStay(){
		
		
	}
	
	void SmoothFollow(){
		
		if(isColliding == false){
			playerMovement = new Vector3(Player.transform.position.x, 0.0f, CameraHeightZ);
			transform.position = playerMovement;
		}
		
	
		ScreenMiddleToPlayer = ScreenMiddle - Player.transform.position;
		
		Debug.Log(ScreenMiddleToPlayer);
		
		if(ScreenMiddleToPlayer.x > 0 && ContactDir.x > 0){
			
			isColliding = false;
			
		};
		
		
	}
	
}
