using UnityEngine;
using System.Collections;

public class ItemIconHolder : MonoBehaviour {

	public Texture2D itemIcon;

	void Start(){
		Inventory inv = GameObject.Find("MasterMind").GetComponent<Inventory>();
		if(inv.checkIfHaveBeenPickedUp(gameObject.name)){
			//Debug.Log("this item is already pickedup. Removing...");
			Destroy (gameObject);
			
		}
	}
}
