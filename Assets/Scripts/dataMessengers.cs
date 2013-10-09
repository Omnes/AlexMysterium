using UnityEngine;
using System.Collections;

public class dataMessengers : MonoBehaviour {
	
	/*
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	*/
}

public struct CameraChangePos{
		public Vector3 	pos;
		public bool 	isPuzzle; 
}

// create a zoom data block with all the information needed for 1 zoom
public struct ZDB{
	// public Transform 	deactive_pos; 	// Position of the zoom_interact
	// public Transform 	active_pos; 	// Position of the zoom
	public Vector3 		deactive_pos; 		// last camera pos
	public Vector3 		active_pos;			// new camera pos
	public string 		name; 				// Name of the zoom
}
