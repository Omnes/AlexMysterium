using UnityEngine;
using System.Collections;

public class Interact_Door : MonoBehaviour {
	
	public string nextLevelName;
	public string spawnpointName;
	public string key = "Item_1";
	public bool locked = true;

	// Use this for initialization
	void Start () {
	
	}
	
	public void Interact(){
		//sätt vilken spawnpoint som ska användas på nästa level
		GameObject masterMind = GameObject.Find("MasterMind");
		Inventory inv = masterMind.GetComponent<Inventory>();
		
		if(locked){
			if(inv.useItem(key,true)){
				//Debug.Log("The Door is open!");
				locked = false;
				//Application.LoadLevel(nextLevelName);
			}else{
				//Debug.Log("The Door is locked :(");
				//need to change this if we got more locked doors
				gameObject.GetComponent<DoorLocked>().playDoorLocked_corridor1();
			}
		}
		
		if(!locked){
			masterMind.SendMessage("setSpawnpoint",spawnpointName);
			masterMind.SendMessage("LoadLevel",nextLevelName,SendMessageOptions.RequireReceiver);
		}
		
	}

}
