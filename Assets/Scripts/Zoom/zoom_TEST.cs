using UnityEngine;
using System.Collections;

/*
public struct CameraChangePos{
		public Vector3 pos;
		public bool isPuzzle; 
}*/

public class zoom_TEST : MonoBehaviour {

	public GameObject ZoomCamPrefab;
	public string name; 			// name of the linked zoom
	public GameObject childObject;	// The camerapos of the zoom
	public GameObject Puzzel_cam;
	public string Puzzelmanager_Name;
	
	private Vector3 changePos;
	private ZDB zoomInfo;
	private GameObject cam_ref;
	private Puzzel_Manager current_Manager;
	private bool uses_puzzel = false;
	
	// Use this for initialization
	void Awake(){
		//changePos			= GameObject.Find(name).GetComponent("zoom").retrieveCameraPos(); 					// hitta till rätt inzoomning så man kan hämta koordinaterna som kameran ska flyttas till. //1: find the object. 2: find the script on that object. 3:Access that script 4:???? 5: PROFIT
		
		changePos			= childObject.transform.position;
		//changePos			= GameObject.Find(name).GetComponent<zoom>().retrieveCameraPos();
		//Component tempScript	    = temp.GetComponent("zoom"); 
		//changePos 					= tempScript.retrieveCameraPos();
		/*
		//-------------------------------------------------------
		cam_ref = Camera.main;
		//Create camera
		Camera temp_main = Camera.main;
		Puzzel_cam = Camera.Instantiate(
			temp_main, 				// clone of the original
			changePos,				// Position
			Quaternion.FromToRotation(new Vector3(0, 0, 0), new Vector3(0, 0, 1)) 		// Rotation
		) as Camera;
		Puzzel_cam.enabled = false;	// Activate if needed
		Puzzel_cam.GetComponent<AudioListener>().enabled = false;
		//--------------------------------------------------------
		*/
		//-------------------------------------------------------
		cam_ref = Camera.main.gameObject;
		//Create camera
		/*Puzzel_cam = GameObject.Instantiate(
			//temp_main, 				// clone of the original
			ZoomCamPrefab, 				// clone of the original
			changePos,				// Position
			Quaternion.FromToRotation(new Vector3(0, 0, 0), new Vector3(0, 0, 1)) 		// Rotation
			) as GameObject;*/
		//Puzzel_cam.gameObject.SetActive(false);	// Activate if needed
		//Puzzel_cam.position = changePos;
		//Puzzel_cam.GetComponent<AudioListener>().enabled = false;
		//--------------------------------------------------------
		//zoomInfo = Create_ZDB();
	}

	void Start () {

		Puzzel_cam = GameObject.Instantiate(
			//temp_main, 				// clone of the original
			ZoomCamPrefab, 				// clone of the original
			changePos,				// Position
			Quaternion.FromToRotation(new Vector3(0, 0, 0), new Vector3(0, 0, 1)) 		// Rotation
			) as GameObject;


		//ZoomCamPrefab.gameObject.SetActive(false);
		Debug.Log("hejseas ");
		Puzzel_cam.gameObject.SetActive(false);	// Activate if needed
		//Puzzel_cam.position = changePos;
		Puzzel_cam.GetComponent<AudioListener>().enabled = false;
		//--------------------------------------------------------

		if(Puzzelmanager_Name != ""){
			current_Manager = GameObject.Find(Puzzelmanager_Name).GetComponent<Puzzel_Manager>();
			uses_puzzel = true;
		}
		//zoomInfo = Create_ZDB();
		
	}
	
	/*// Update is called once per frame
	void Update () {
	
	}
	*/
	
		
	void Activate()
	{
		//zoomInfo = Create_ZDB();
		//send_ZDB();
		/*
		Puzzel_cam.enabled = true; 		// ! might need a more solid approch but for now this works !
		Puzzel_cam.GetComponent<AudioListener>().enabled = true;
		cam_ref.GetComponent<AudioListener>().enabled = false;
		cam_ref.enabled = false; // if it don't work call them by name.
		*/
		zoomInfo = Create_ZDB();
		send_ZDB();
		Debug.Log("RAWR!");
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
		GameObject.Find("MasterMind").SendMessage("Deactivate");
		/*
		cam_ref.enabled 	= true; // if it don't work call them by name.
		cam_ref.GetComponent<AudioListener>().enabled		= true;
		Puzzel_cam.GetComponent<AudioListener>().enabled	= false;
		Puzzel_cam.enabled 	= false;
		*/
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
		zoom.active_pos		= childObject.transform.position;
		zoom.name 			= name;
		zoom.zoom_cam 		= Puzzel_cam;
		zoom.uses_puzzel	= uses_puzzel;
		zoom.current_manager= current_Manager;
		
		return zoom;
	}
	
	void send_ZDB(){
		//Camera.main.SendMessage("reciveZDB", zoomInfo);
		GameObject.Find("MasterMind").SendMessage("reciveZDB", zoomInfo);
	}
}
