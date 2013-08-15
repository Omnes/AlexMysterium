using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	
	public Transform player;
	private MovementManager ptrMovementManager; 	//reference to pathfinding
	private bool WorldInput = true;			//True if outside puzzle, false if in puzzle
	//private Vector3 ObjectPosition;
	
	// Use this for initialization
	void Start () {
		ptrMovementManager = player.GetComponent<MovementManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
		
		//if in puzzle WorldInput == false
		if(WorldInput){
			FindInput ();
		}
		
	}
	
	void FindInput(){
		
		//mousebutton left pressed
		if(Input.GetMouseButton(0)){
			
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)){
				
				//Debug.Log(hit.transform.name);
				
				//Sends input to pathfinding
				if(hit.transform.tag == "Floor"){
					
					//Debug.Log("Floor");
					Debug.Log("input: "+hit.transform.position);
					//transform eller position ? 
					ptrMovementManager.pathfindToPosition(hit.point);
					
				}
				//Puts object in Inventory
				if(hit.transform.tag == "Item"){
					Debug.Log("Item");
					StopCoroutine("PickUpObject");
					StartCoroutine(PickUpObject(hit.transform));
					
				}
				
				//Sends waypoint to pathfinding, and also gameobject
				if(hit.transform.tag == "Interactive"){
					
					//Debug.Log("Interactive");
					StopCoroutine("InteractObject");
					StartCoroutine(InteractObject(hit.transform));
					
				}
				
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
				target.SendMessage("Interact");
				break;
			}
			
			yield return new WaitForSeconds(.1f);
		}
		
	}
	
}
