using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class zoomManager : MonoBehaviour {
	//------------------------------------
	// Use this for initialization
	//Public:
	//public Queue zoomQueue;// tänkte först använda queue, men fick problem och fick förslaget att använda List istället som funkade, gör om koden att använda List
			public List<ZDB> zoomList;
	//public ZDB[] zoomArray;
	//Private: 
	
	//------------------------------------
	void Start () {
		//zoomQueue = new Queue();
		zoomList = new List<ZDB>();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void reciveZDB(ZDB newZoom){// add new ZDB to the queue
		//Debug.Log(newZoom.name);
		//zoomQueue.Enqueue(newZoom);
				zoomList.Add(newZoom);
		Activate ();
	}
	
	void DeacitivateCurrent(){ // remove deactivated ZDB from the queue
		if(zoomList.Count != 0){
			//zoomQueue.Dequeue();
			zoomList.RemoveAt(zoomList.Count - 1);// remove last element in list
		}
	}
	
	/*void ShiftFocus(){// set focus to the current ZDB (set camera etc)
			
	}*/
	
	void Activate(){// activate current ZDB
		if(zoomList.Count == 1){ // we only need to say we are in a puzzel the firt time we enter one, not when we go deeper into the puzzel/zoom
			Debug.Log("You entered the zoom");
			GameObject.Find("MasterMind").SendMessage("SetIsPuzzle", true);				// Let the input manager know we are in a puzzel.
		}
		//ZDB temp = zoomQueue.Peek() as ZDB;// use the latest zoom
				ZDB temp = zoomList[zoomList.Count - 1];
		
		CameraChangePos messenger 	= new CameraChangePos();			/* Change the camera position and let it know we are in puzzel mode 	*/
		messenger.pos 				= temp.active_pos;						/* Uses a struct with a position and a bool as a data messenger			*/
		messenger.isPuzzle 			= true;								/*																		*/
		
		Camera.main.SendMessage("SetPos", messenger);					// the new camera position is set.
		GameObject.Find(temp.name).GetComponent<GUI_Parent>().Activate(true);	// activates the GUI-components of the current zoom.
	}
	
	void Deactivate(){// deactivate current ZDB
		if(zoomList.Count != 0){// make sure we have a zoom to deactivate first
			//ZDB temp = zoomQueue.Peek() as ZDB;// use the latest zoom
					ZDB temp = zoomList[zoomList.Count - 1];
			
			GameObject.Find(temp.name).GetComponent<GUI_Parent>().Activate(false);	// activates the GUI-components of the current zoom.
			if(zoomList.Count == 1){
				Debug.Log("You left the zoom");
				GameObject.Find("MasterMind").SendMessage("SetIsPuzzle", false);				// Let the input manager know we are in a puzzel.
			}
				
			CameraChangePos messenger 	= new CameraChangePos();			/* Change the camera position and let it know we are in puzzel mode 	*/
			messenger.pos 				= temp.deactive_pos;				/*  Uses a struct with a position and a bool as a data messenger		*/
			messenger.isPuzzle 			= (zoomList.Count == 1 ? false : true);							/*																		*/
			
			Camera.main.SendMessage("SetPos", messenger);					// the new camera position is set.
			DeacitivateCurrent();											// remove the deactive zoom from the manager.
			if(zoomList.Count != 0){// if there is another 
				Activate ();	
			}
		}
		else{
			Debug.Log ("Can't deactivate the zoom because the is no zoom active (the queue is empty)");
		}
	}
}
