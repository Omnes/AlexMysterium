using UnityEngine;
using System.Collections;

public class ActivateCardReader : MonoBehaviour {
	public Transform trans;
	
	void ActivateStuff(){
		trans.GetComponent<Cardreader>().enabled = true;
	}

	void DeactivateStuff(){
		trans.GetComponent<Cardreader>().enabled = false;
	}
}
