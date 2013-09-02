using UnityEngine;
using System.Collections;

/*
public struct CameraChangePos{
		public Vector3 pos;
		public bool isPuzzle; 
}*/

public class zoom_TEST : MonoBehaviour {
	
	public string name;
	private Vector3 changePos;
	// Use this for initialization
	void Start () {
		//changePos			= GameObject.Find(name).GetComponent("zoom").retrieveCameraPos(); 					// hitta till rätt inzoomning så man kan hämta koordinaterna som kameran ska flyttas till. //1: find the object. 2: find the script on that object. 3:Access that script 4:???? 5: PROFIT
		changePos			= GameObject.Find(name).GetComponent<zoom>().retrieveCameraPos();
		//Component tempScript	    = temp.GetComponent("zoom"); 
		//changePos 					= tempScript.retrieveCameraPos();
		
	}
	
	/*// Update is called once per frame
	void Update () {
	
	}
	*/
	void Interact(){
		Debug.Log("you interacted with the zoom-object!?!?!!!!");
		Camera.main.SendMessage("SetIsPuzzle", true);
		
		CameraChangePos messenger = new CameraChangePos();
		messenger.pos = changePos;
		messenger.isPuzzle = true;
		
		Camera.main.SendMessage("SetPos", messenger);
		GameObject.Find(name).GetComponent<GUI_Parent>().Activate(true);
	}
}
