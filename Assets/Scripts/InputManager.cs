using UnityEngine;
using System.Collections;


public class InputManager : MonoBehaviour {
	
	//public
	//public Transform playerTransform;
	public float buttonDelay = 0.3f;
	
	//private
	//public Transform player;
	private MovementManager ptrMovementManager; 	//reference to pathfinding
	public bool isPuzzle = false;					//True if outside puzzle, false if in puzzle
	private float buttonDelayCounter;
	private Inventory ptrInventory;
	
	// Use this for initialization
	void Start (){
		//get MovementmanagerScript for pathfinding
		//ptrMovementManager = playerTransform.GetComponent<MovementManager>();
		
		//Start button delay counter
		buttonDelayCounter = Time.time;
		ptrInventory = GameObject.Find("MasterMind").GetComponent<Inventory>();
	}
	
	public void SetPlayer (Transform player) {
		ptrMovementManager = player.GetComponent<MovementManager>();
	}
	
	// Update is called once per frame
	void Update () {

		//if in puzzle then WorldInput == false
		if(buttonDelayCounter + buttonDelay < Time.time && !ptrInventory.showInventory){
			FindInput ();
		}
		
	}
	
	void stopPreviousCoroutine(){
		StopCoroutine("PickUpObject");
		StopCoroutine("InteractObject");
		StopCoroutine("UseItemOn");
	}
	
	
	void FindInput(){
		
		//mousebutton left pressed
		if(Input.GetMouseButtonDown(0)){

			//reset mousecounter
			buttonDelayCounter = Time.time;
			
			//Input for where the user is pointing
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit[] hits;
			hits = Physics.RaycastAll(ray);
			int i = 0;
			while(i < hits.Length){
				RaycastHit hit = hits[i];
				i++;
				
				//---------------------------------------------------------
				//Puts object in Inventory
				if(hit.transform.tag == "Item"){
					
					//if in puzzle
					//without pathfinding
					if(isPuzzle){
						
						ptrInventory.AddItem(hit.transform.gameObject);
						//hit.transform.SendMessage("Item");
						
					}else{//with pathfinding
					
						//stop previous coroutine
						stopPreviousCoroutine();
						//start new coroutine
						StartCoroutine("PickUpObject", (hit.transform));
					}
					return;
					
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
						stopPreviousCoroutine();
						//start new coroutine
						StartCoroutine("InteractObject", (hit.transform));
						
					}
					return;
					
				}
				
				//Sends input to pathfinding
				if(hit.transform.tag == "Floor"){
					//move to position
					ptrMovementManager.pathfindToPosition(hit.point);
					
					
				}

				
			}
			
		}
		
	}
	
	struct TargetAndItem{
		public Transform target;
		public Item item; 
		
		public TargetAndItem(Transform target,Item item){
			this.target = target;
			this.item = item;
		}
		
	}
	
	
	public void UseItemOnTarget(Transform target,Item item){
		
		if(isPuzzle){
			target.SendMessage("UseItem",item,SendMessageOptions.DontRequireReceiver);
		
		}else{
			stopPreviousCoroutine();
			StartCoroutine("UseItemOn", new TargetAndItem(target,item));
		}
		
	}
	
	
	
	//pickup object
	IEnumerator PickUpObject(Transform target){
		
		Vector3 targetPosition = ptrMovementManager.pathfindToObject(target);
		while(true){
			if(ptrMovementManager.isAtPosition(targetPosition)){
				
				ptrInventory.AddItem(target.gameObject);
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
	
	
	
	IEnumerator UseItemOn(TargetAndItem tai){
		
		Vector3 targetPosition = ptrMovementManager.pathfindToObject(tai.target);
		while(true){
			if(ptrMovementManager.isAtPosition(targetPosition)){
				tai.target.SendMessage("UseItem",tai.item,SendMessageOptions.DontRequireReceiver);
				break;
			}
			
			yield return new WaitForSeconds(.1f);
		}
		
	}
	
	void SetIsPuzzle(bool isPuzzleTrueOrFalse){
		
		isPuzzle = isPuzzleTrueOrFalse;
		
	}
	
}
