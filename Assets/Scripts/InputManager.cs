using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	
	public MovementManager ptrMovementManager; 	//reference to pathfinding
	private bool WorldInput = true;			//True if outside puzzle, false if in puzzle
	//private Vector3 ObjectPosition;
	
	// Use this for initialization
	void Start () {
	
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
				
				//Sends input to pathfinding
				if(hit.transform.tag == "Floor"){
					
					Debug.Log("Floor");
					//transform eller position ? 
					//ptrMovementManager.PathfindToPosition(hit.transform.position);
					
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
					hit.transform.gameObject.SendMessage("Interact");
					//
					//ptrMovementManager.findobjectandactiveate();
					
					
				}
				
			}
			
		}
		
	}
	//pickup object
	IEnumerator PickUpObject(Transform target){
		
		Vector3 targetPosition = ptrMovementManager.PathfindToObject(target);
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
		
		Vector3 targetPosition = ptrMovementManager.PathfindToObject(target);
		while(true){
			if(ptrMovementManager.isAtPosition(targetPosition)){
				target.SendMessage("Interact");
				break;
			}
			
			yield return new WaitForSeconds(.1f);
		}
		
	}
	
}
