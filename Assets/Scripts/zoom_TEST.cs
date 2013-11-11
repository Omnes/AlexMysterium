using UnityEngine;
using System.Collections;

/*
public struct CameraChangePos{
		public Vector3 pos;
		public bool isPuzzle; 
}*/

public class zoom_TEST : MonoBehaviour {
	
	public string name; 			// name of the linked zoom
	public Transform childObject;	// The camerapos of the zoom
	
	private Vector3 changePos;
	private ZDB zoomInfo;
	
	// Use this for initialization
	void Start () {
		//changePos			= GameObject.Find(name).GetComponent("zoom").retrieveCameraPos(); 					// hitta till rätt inzoomning så man kan hämta koordinaterna som kameran ska flyttas till. //1: find the object. 2: find the script on that object. 3:Access that script 4:???? 5: PROFIT
		
		changePos			= childObject.position;
			//changePos			= GameObject.Find(name).GetComponent<zoom>().retrieveCameraPos();
		//Component tempScript	    = temp.GetComponent("zoom"); 
		//changePos 					= tempScript.retrieveCameraPos();
		zoomInfo = Create_ZDB();
		
	}
	
	/*// Update is called once per frame
	void Update () {
	
	}
	*/
	
		
	void Activate()
	{
		send_ZDB();
		// Debug.Log("You entered the zoom");
		// Camera.main.SendMessage("SetIsPuzzle", true);				// Let the input manager know we are in a puzzel.
		
		// CameraChangePos messenger 	= new CameraChangePos();			/* Change the camera position and let it know we are in puzzel mode 	*/
		// messenger.pos 				= changePos;						/* Uses a struct with a position and a bool as a data messenger			*/
		// messenger.isPuzzle 			= true;								/*																		*/
		
		// Camera.main.SendMessage("SetPos", messenger);				// the new camera position is set.
		// GameObject.Find(name).GetComponent<GUI_Parent>().Activate(true);	// activates the GUI-components of the current zoom.
	}
		
	void Interact(){
		Activate();
		//Debug.Log("you interacted with the zoom-object!?!?!!!!");
		//Camera.main.SendMessage("SetIsPuzzle", true);				// Let the input manager know we are in a puzzel.
		
		//CameraChangePos messenger = new CameraChangePos();			/* Change the camera position and let it know we are in puzzel mode 	*/
		//messenger.pos = changePos;									/*  																	*/
		//messenger.isPuzzle = true;									/*																		*/
		
		//Camera.main.SendMessage("SetPos", messenger);				// the new camera position is set.
		//GameObject.Find(name).GetComponent<GUI_Parent>().Activate(true);	// activates the GUI-components of the current zoom.
	}
	
	void Deactivate()
	{
		Camera.main.SendMessage("Deactivate");
		// GameObject.Find(name).GetComponent<GUI_Parent>().Activate(false);	// activates the GUI-components of the current zoom.
		
		// Debug.Log("You left the zoom");
		// Camera.main.SendMessage("SetIsPuzzle", false);				// Let the input manager know we are in a puzzel.
		
		// CameraChangePos messenger 	= new CameraChangePos();			/* Change the camera position and let it know we are in puzzel mode 	*/
		// messenger.pos 				= changePos;						/*  Uses a struct with a position and a bool as a data messenger		*/
		// messenger.isPuzzle 			= false;							/*																		*/
		
		// Camera.main.SendMessage("SetPos", messenger);				// the new camera position is set.
	}
	
	ZDB Create_ZDB()//  creates a ZoomDataBlock
	{
		ZDB zoom = new ZDB();
		zoom.deactive_pos	= Camera.main.transform.position;
		zoom.active_pos		= childObject.position;
		zoom.name 			= name;
		
		return zoom;
	}
	
	void send_ZDB(){
		Camera.main.SendMessage("reciveZDB", zoomInfo);
	}
}
