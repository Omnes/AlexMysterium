using UnityEngine;
using System.Collections;

public class StromAvbrott : MonoBehaviour {

	public bool powerON = false;
	public bool avbrott = false;
	private Transform[] reactiveObjects;

	public bool asdhjabsdkja = true;
	private bool played = false;

	public AudioSource audioS;
	MessageWindow quest;

	void Start(){
		quest = GameObject.Find ("MasterMind").GetComponent<MessageWindow>();
		powerON = GetComponent<ItemUseStates>().button;
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
		if(!avbrott){

			quest.finishedSubQuest("1e");
		}
		GetComponent<ItemUseStates>().powerout = avbrott;
		setOn();
	}

	void togglePowerON(){
		if(!avbrott){
			powerON = !powerON;
			GetComponent<ItemUseStates>().button = !powerON;
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
		GameObject[] withlighttag = GameObject.FindGameObjectsWithTag("LightLayer");
		foreach(GameObject g in withlighttag){
			g.renderer.enabled =powerON;
		}
	}

	void powerOut(){

		audioS.Play();
		quest.addSubQuest("1e");

		reactiveObjects = GameObject.Find("currentSceneInfo").GetComponent<GetReactiveObjects>().reactiveObj_array;

		for(int i  = 0; i < reactiveObjects.Length; i++){
			reactiveObjects[i].SendMessage("setPowerOut", false);
		}
		avbrott = true;
		GetComponent<ItemUseStates>().powerout = avbrott;
		powerON = false;
		setOn();
		
	}
}
