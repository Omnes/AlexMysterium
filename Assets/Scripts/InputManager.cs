using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	
	//public
	//public Transform playerTransform;
	public float buttonDelay = 0.3f;
	
	//private
	//public Transform player;
	private MovementManager ptrMovementManager; 	//reference to pathfinding
	private bool isPuzzle = false;					//True if outside puzzle, false if in puzzle
	private float buttonDelayCounter;
	
	// Use this for initialization
	void Start (){
		//get MovementmanagerScript for pathfinding
		//ptrMovementManager = playerTransform.GetComponent<MovementManager>();
		
		//Start button delay counter
		buttonDelayCounter = Time.time;
	}
	
	public void SetPlayer (Transform player) {
		ptrMovementManager = player.GetComponent<MovementManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
		//if in puzzle then WorldInput == false
		if(buttonDelayCounter + buttonDelay < Time.time){
			FindInput ();
		}
		
	}
	
	void FindInput(){
		
		//mousebutton left pressed
		if(Input.GetMouseButtonDown(0)){
			
			//reset mousecounter
			buttonDelayCounter = Time.time;
			
			//Input for where the user is pointing
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)){
				
				//Sends input to pathfinding
				if(hit.transform.tag == "Floor"){
					//move to position
					ptrMovementManager.pathfindToPosition(hit.point);
					
				}
				//---------------------------------------------------------
				//Puts object in Inventory
				if(hit.transform.tag == "Item"){
					
					//if in puzzle
					//without pathfinding
					if(isPuzzle){
						
						hit.transform.SendMessage("Item");
						
					}else{//with pathfinding
					
						//stop previous coroutine
						StopCoroutine("PickUpObject");
						//start new coroutine
						StartCoroutine("PickUpObject", (hit.transform));
					}
					
				}
				//----------------------------------------------------------
				//Sends waypoint to pathfinding, and also gameobject
				if(hit.transform.tag == "Interactive"){
					
					//if in puzzle
					//without pathfinding
					if(isPuzzle){
						
						hit.transform.SendMessage("Interact");
						
					}else{//with pathfinding
						
						//stop previous coroutine
						StopCoroutine("InteractObject");
						//start new coroutine
						StartCoroutine("InteractObject", (hit.transform));
						
					}
					
				}
				//----------------------------------------------------------
				if(hit.transform.tag == "Zoom_Interact"){
					
					//if in puzzle
					//without pathfinding
					if(isPuzzle){
						
						hit.transform.SendMessage("Activate");
						
					}else{//with pathfinding
						
						//stop previous coroutine
						StopCoroutine("InteractObject");
						//start new coroutine
						StartCoroutine("InteractObject", (hit.transform));
						
					}
					
				}
				//---------------------------------------------------------
				
			}
			
		}
		
	}
	//pickup object
	IEnumerator PickUpObject(Transform target){
		
		Vector3 targetPosition = ptrMovementManager.pathfindToObject(target);
		while(true){
			if(ptrMovementManager.isAtPosition(targetPosition)){
				
				//doinventoryfunctionstuff
				target.SendMessage("Interact");
				break;
			}
			
			yield return new WaitForSeconds(.1f);
		}
		
	}
	
	//activate object
	IEnumerator InteractObject(Transform target){
		
		Vector3 targetPosition = ptrMovementManager.pathfindToObject(target);
		while(true){
			if(ptrMovementManager.isAtPosition(targetPosition)){
				target.SendMessage("Activate");
				break;
			}
			
			yield return new WaitForSeconds(.1f);
		}
		
	}
	
	void SetIsPuzzle(bool isPuzzleTrueOrFalse){
		
		isPuzzle = isPuzzleTrueOrFalse;
		
	}
	
}
