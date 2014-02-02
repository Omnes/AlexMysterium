using UnityEngine;
using System.Collections;

public class Janitary_Button : MonoBehaviour {

	void Interact(){
		GameObject.Find("MasterMind").SendMessage("togglePowerON");
	}
}
