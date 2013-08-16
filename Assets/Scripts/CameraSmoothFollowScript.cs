using UnityEngine;
using System.Collections;

public class CameraSmoothFollowScript : MonoBehaviour {
	
	//Should be prefab and should initaiate new player each scene ***************************
	public GameObject Player;
	public float CameraHeightZ;
	public float CameraHeightY;
	private Vector3 playerMovement;
	private Vector3 ContactDir;
	private bool isColliding = false;
	private Vector3 cameraCenter;
	private Vector3 cameraCenterToPlayer;
	
	
	// Use this for initialization
	void Start () {
		//startposition
		playerMovement = new Vector3(Player.transform.position.x, CameraHeightY, CameraHeightZ);
		transform.position = playerMovement;
	}
	
	// Update is called once per frame
	void Update () {
		
		SmoothFollow();
		
	}
	
	void OnCollisionEnter(Collision other) {
		
		//collision vector.
		ContactDir = other.transform.position - transform.position;
		//is the camera colliding ?
		isColliding = true;
		
	}
	
	void SmoothFollow(){
		
		//follow player
		if(isColliding == false){
			playerMovement = new Vector3(Player.transform.position.x, CameraHeightY, CameraHeightZ);
			transform.position = playerMovement;
		}
		
		//Center of camera
		cameraCenter = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
		
		//vector between center and player
		cameraCenterToPlayer = cameraCenter - Player.transform.position;
	
		//if collision vector is same as vector(center to player) then player is moving away from the collision
		if(cameraCenterToPlayer.x > 0 && ContactDir.x > 0){
			isColliding = false;
		}else if(cameraCenterToPlayer.x < 0 && ContactDir.x < 0){
			isColliding = false;
		};
		
		
	}
	
}
