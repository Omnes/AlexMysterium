using UnityEngine;
using System.Collections;

public class ActivateCardReader : MonoBehaviour {
	public Transform trans;
	public string keycard;
	
	void ActivateStuff(){
		trans.GetComponent<Cardreader>().enabled = true;
	}

	void DeactivateStuff(){
		trans.GetComponent<Cardreader>().enabled = false;
	}

	void UseItem(Item item){
		if(item.name == keycard){
			trans.GetComponent<Cardreader>().carddrawn = true;
		}
	}
}
