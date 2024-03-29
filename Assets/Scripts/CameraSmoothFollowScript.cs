using UnityEngine;
using System.Collections;

public class CameraSmoothFollowScript : MonoBehaviour {
	
	//public
	//Should be prefab and should initaiate new player each scene ***************************
	public GameObject Player;
	public float CameraDistZ;
	public float CameraDistY;
	
	//private
	private Vector3 playerMovement;
	private Vector3 ContactDir;
	private bool isColliding = false;
	private Vector3 cameraCenter;
	private Vector3 cameraCenterToPlayer;
	public bool isFollowing = true;
	
	
	// Use this for initialization
	void Start () {
		//startposition

		GameObject cmObj = GameObject.FindGameObjectWithTag("CameraSpawn");
		if(cmObj != null){
			transform.position = new Vector3(cmObj.transform.position.x, CameraDistY, CameraDistZ);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(isFollowing){
			SmoothFollow();
		}
		
	}
	
	void OnCollisionEnter(Collision other) {
		
		//collision vector.
		ContactDir = other.transform.position - transform.position;
		//is the camera colliding ?
		isColliding = true;
		//disable collider
		collider.enabled = false;
		//set velocity to zero
		transform.rigidbody.velocity = new Vector3(0f,0f,0f);
	}
	
	void SmoothFollow(){
		
		//follow player
		if(isColliding == false){
			playerMovement = new Vector3(Player.transform.position.x, CameraDistY, CameraDistZ);
			Vector3 dirVec = playerMovement - transform.position;
			//pro programming skillz
			if(dirVec.magnitude > 0.001){
				transform.position += dirVec.normalized * 0.1f * dirVec.magnitude;
			}
		}
		
		//Center of camera
		cameraCenter = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
		
		//vector between center and player
		cameraCenterToPlayer = cameraCenter - Player.transform.position;
	
		//if collision vector is same as vector(center to player) then player is moving away from the collision
		if(cameraCenterToPlayer.x > 0 && ContactDir.x > 0){
			isColliding = false;
			collider.enabled = true;
		}else if(cameraCenterToPlayer.x < 0 && ContactDir.x < 0){
			isColliding = false;
			collider.enabled = true;
		};
		
		
	}
	
	/*void SetPos(Vector3 pos, bool puzzle){
		
		transform.position = pos;
		
		//if puzzle = true then turn off smoothfollow
		if(puzzle){
			isFollowing = false;
		}else{
			isFollowing = true;
		}
		
	}*/
	
	void SetPos(CameraChangePos x){
		
		transform.position = x.pos;
		
		//if puzzle = true then turn off smoothfollow
		if(x.isPuzzle){
			isFollowing = false;
		}else{
			isFollowing = false;
		}
		
	}
	
	void exitPuzzle(){
		isFollowing = false;
		
	}
	
	
	
}
