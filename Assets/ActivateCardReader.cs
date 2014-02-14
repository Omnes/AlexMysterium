using UnityEngine;
using System.Collections;

public class ActivateCardReader : MonoBehaviour {
	public Transform trans;
	public string keycard;
	public ItemUseStates ius;

	void Start(){
		ius = GameObject.Find ("MasterMind").GetComponent<ItemUseStates>();
	}
	
	void ActivateStuff(){
		if(!ius.powerout){
			trans.GetComponent<Cardreader>().enabled = true;
		}

	}

	void DeactivateStuff(){
		trans.GetComponent<Cardreader>().enabled = false;
	}
	void Interact(){

	}

	void UseItem(Item item){
		if(item.name == keycard){
			trans.SendMessage("drawCard");
		}
	}
}
