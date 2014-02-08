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

		cam_ref = Camera.main.gameObject;

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
//robin did this		Puzzel_cam.GetComponent<AudioListener>().enabled = false;
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

		zoomInfo = Create_ZDB();
		send_ZDB();
		Debug.Log("RAWR!");

	}
		
	void Interact(){
		Activate();

	}
	
	void Deactivate()
	{
		GameObject.Find("MasterMind").SendMessage("Deactivate");

	}
	
	ZDB Create_ZDB()//  creates a ZoomDataBlock
	{
		ZDB zoom = new ZDB();
		zoom.deactive_pos	= Camera.main.transform.position;
		zoom.active_pos		= childObject.transform.position;
		zoom.name 			= name;
		zoom.zoom_cam 		= Puzzel_cam;
		zoom.uses_puzzel	= uses_puzzel;
		if(current_Manager != null){
			zoom.current_manager= current_Manager;
		}else{
			Debug.LogWarning("puzzel_Manager not used");
		}
		
		return zoom;
	}
	
	void send_ZDB(){
		//Camera.main.SendMessage("reciveZDB", zoomInfo);
		GameObject.Find("MasterMind").SendMessage("reciveZDB", zoomInfo);
	}
}
