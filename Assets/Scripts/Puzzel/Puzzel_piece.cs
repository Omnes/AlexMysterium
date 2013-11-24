using UnityEngine;
using System.Collections;

public class Puzzel_piece : MonoBehaviour {
	
	
	
	public Vector3 	position;
	public Vector2	size = new Vector2(64,64);
	//public float 	radius;
	public int 		Piece_KeyValue;
	
	public bool	holding = false;	// being moved by the user
	public bool correct = true;		// is in correct position
	
	private bool notUsed = true;
	
	// Use this for initialization
	void Start () {
		position = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(notUsed){
			if(transform.position != position && correct){
				transform.position = position;	
			}
		}
		/*
		if(movable){
		  	if(Input.GetAxis("Mouse X")<0){
    			//Code for action on mouse moving left
   			 	Debug.Log("Mouse moved left");
   			 }
    		else if(Input.GetAxis("Mouse X")>0){
   		 		//Code for action on mouse moving right
   				 Debug.Log("Mouse moved right");
    		}
			if(Input.GetAxis("Mouse Y")<0){
    			//Code for action on mouse moving left
   			 	Debug.Log("Mouse moved left");
   			 }
    		else if(Input.GetAxis("Mouse Y")>0){
   		 		//Code for action on mouse moving right
   				 Debug.Log("Mouse moved right");
    		}
		}
		*/
	}
	
	public void Switch_MoveState(){
		correct = !correct;	
	}
	
	void OnTriggerEnter(Collider other){
		if(other.tag == "PusselSlot"){
			notUsed = false;
		}
	}
	
	void OnTriggerExit(Collider other){
		if(other.tag == "PusselSlot"){
			notUsed = true;
		}
	}
}

/*public struct Piece{
	
	public int 			Piece_KeyValue;
	private bool 		movable;
	
	public Texture2D 	sprite;
	public int 			id;
	
	public Piece(string name,Texture2D sprite,int id){
		this.name = name;
		this.sprite = sprite;
		this.id = id;
	}
	
}*/
