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

		changePos			= childObject.transform.position;

		cam_ref = Camera.main.gameObject;

	}

	void Start () {

		Puzzel_cam = GameObject.Instantiate(
			ZoomCamPrefab, 				// clone of the original
			changePos,				// Position
			Quaternion.FromToRotation(new Vector3(0, 0, 0), new Vector3(0, 0, 1)) 		// Rotation
			) as GameObject;

		Debug.Log("hejseas ");
		Puzzel_cam.gameObject.SetActive(false);	// Activate if needed

		if(Puzzelmanager_Name != ""){
			current_Manager = GameObject.Find(Puzzelmanager_Name).GetComponent<Puzzel_Manager>();
			uses_puzzel = true;
		}

	}

	void Activate(){
		zoomInfo = Create_ZDB();
		send_ZDB();
		childObject.transform.parent.SendMessage("ActivateStuff",SendMessageOptions.DontRequireReceiver);
		Debug.Log("RAWR!");

	}
		
	void Interact(){
		if(!enabled) return;
		Activate();
	}
	
	void Deactivate()
	{
		childObject.transform.parent.SendMessage("DeactivateStuff",SendMessageOptions.DontRequireReceiver);
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
