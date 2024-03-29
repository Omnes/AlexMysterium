	using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Puzzel_Manager : MonoBehaviour {

	public bool enabled 					= false;
	public bool Completed					= false;
	
	public List<Puzzel_piece> Ppiece_list;

	public List<Puzzel_slot> Pslot_list;

	
	public float buttonDelay = 0.3f;
	private float buttonDelayCounter;
	
	public Puzzel_piece tempPiece; 
	//private Vector3 tempPos;
	// sounds
	
	
	// Use this for initialization
	void Start () {
		//--------------------------------------------------------------------------------------------------------
		//	Fill the list of all pieces for this puzzel
		Ppiece_list = new List<Puzzel_piece>();
		
		// collect all the gui-elements from the current GameObject.
		Component[] pieceComp = GetComponentsInChildren<Puzzel_piece>(true);
		foreach(Component comp in pieceComp){
				addPieceElement(comp as Puzzel_piece); // if they are a subclass they get added in as a base-class
		}
		//--------------------------------------------------------------------------------------------------------
		//	Fill the list of all slots for this puzzel
		Pslot_list = new List<Puzzel_slot>();
		
		// collect all the gui-elements from the current GameObject.
		Component[] slotComp = GetComponentsInChildren<Puzzel_slot>(true);
		foreach(Component comp in slotComp){
				addSlotElement(comp as Puzzel_slot); // if they are a subclass they get added in as a base-class
		}
		//--------------------------------------------------------------------------------------------------------
	}
	
	// Update is called once per frame
	void Update () {
		if(enabled){// should be ENABLED
			//on collision enter
				////Debug.Log ("mouse " + Input.GetMouseButtonDown(0));
			//--------------------------------------------------------------------------------------------------------------------------------------------------
			if(Input.GetMouseButton(0) && tempPiece == null){// if we don't have a piece, try to find one
				//reset mousecounter
				//buttonDelayCounter = Time.time;
				
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
					if(hit.transform.tag == "PusselBit" && tempPiece == null){
						// pressed a pussel piece
						tempPiece 			= hit.transform.gameObject.GetComponent<Puzzel_piece>();
						tempPiece.holding 	= true;

					}
				}
			}
			//-----------------------------------------------------------------------------------------------------------------------------------------------------
			if(Input.GetMouseButton(0) && tempPiece != null){// We are holding a piece
				if(tempPiece.correct){
					////Debug.Log (Input.mousePosition.x + " " + Input.mousePosition.y + " " + Input.mousePosition.z);
					Vector3 newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
						newPos.z = -2;
						tempPiece.transform.position = newPos;
							
				}
			}
			//-----------------------------------------------------------------------------------------------------------------------------------------------------
			if(!Input.GetMouseButton(0) && tempPiece != null){// we dropped the piece
				tempPiece.holding = false;	
				tempPiece = null;
			}
			//-----------------------------------------------------------------------------------------------------------------------------------------------------
			if(CheckIfCorrect() && !Completed){
				Completed = true;
				//Debug.Log("SUCCESS YOU COMPLETED THE PUZZLE!!!");

			}
		}
	}
	
	public void Active(bool CurrentState){
		enabled = CurrentState;	
	}
	
	void addPieceElement(Puzzel_piece x){ //add a element to the list
		Ppiece_list.Add(x);	
	}
	
	void addSlotElement(Puzzel_slot x){ //add a element to the list
		Pslot_list.Add(x);	
	}

	public bool CheckIfCorrect(){
		bool isCorrect = true;
		
		foreach(Puzzel_slot current_slot in Pslot_list){
			if(!current_slot.Correct){
				isCorrect = false;// if all pieces are in ther correct slot then you will never set the value to false.
			}
		}
		
		return isCorrect;
	}
}
