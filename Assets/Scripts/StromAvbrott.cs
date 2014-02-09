using UnityEngine;
using System.Collections;

public class StromAvbrott : MonoBehaviour {

	public bool powerON = true;
	public bool avbrott = false;
	public Transform[] reactiveObjects;

	public bool asdhjabsdkja = true;

	void Start(){
		setOn();
	}

	void Update(){
		if(Input.GetKey(KeyCode.J)){
			//fråga sean
			if(asdhjabsdkja){
				powerOut();
				Debug.Log("powerout!");
				asdhjabsdkja = false;
			}

		}
	}

	void OnEnable(){

	}

	void OnLevelWasLoaded(){
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

	void powerOut(){
		for(int i  = 0; i < reactiveObjects.Length; i++){
			reactiveObjects[i].SendMessage("setPowerOut", false);
		}
		avbrott = true;
		togglePowerON();
	}
}
