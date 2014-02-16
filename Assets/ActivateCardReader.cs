using UnityEngine;
using System.Collections;

public class ActivateCardReader : MonoBehaviour {
	public Transform trans;
	public string keycard;
	public ItemUseStates ius;
	bool hascard;

	void Start(){

		ius = GameObject.Find ("MasterMind").GetComponent<ItemUseStates>();
		hascard = ius.card;
	}
	
	void ActivateStuff(){
		if(!ius.powerout){
			trans.GetComponent<Cardreader>().enabled = true;
			if(!hascard){
				Debug.Log ("DOES NOT HAVE CARD");
				GameObject.Find ("MasterMind").GetComponent<MessageWindow>().addSubQuest("1a");
			}
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
