using UnityEngine;
using System.Collections;

public abstract class GUI_Element : MonoBehaviour {
	// using polymorphism to use this base class as a way of activating several diffrent gui_scripts easily since I then can list then in a single array
	
	// Use this for initialization
	public abstract void Start ();
	
	// Update is called once per frame
	public abstract void Update ();
	
	//gui-function call
	public abstract void OnGUI();
	
	//activate gui
	public abstract void Activate(bool x);
}
