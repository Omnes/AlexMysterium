using UnityEngine;
using System.Collections;

public class StromAvbrott : MonoBehaviour {

	public bool powerON = true;
	public bool avbrott = false;

	void Start(){
		setOn();
	}

	void setAvbrott(bool power){
		avbrott = power;
		if(avbrott && powerON){
			powerON = false;
		}
		setOn();
	}

	void togglePowerON(){
		if(!avbrott){
			powerON = !powerON;
		}else{
			powerON = false;
		}
		setOn();

	}

	void setOn(){
		GameObject[] withtag = GameObject.FindGameObjectsWithTag("DarkLayer");
		foreach(GameObject g in withtag){
			g.renderer.enabled =!powerON;
		}
	}
}
