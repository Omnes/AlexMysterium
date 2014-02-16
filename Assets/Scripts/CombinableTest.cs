using UnityEngine;
using System.Collections;

public class CombinableTest : MonoBehaviour {

	// Use this for initialization
	void UseItem(Item item){
		if(item.name == "Key"){
			transform.position += Vector3.up*3;
			//Debug.Log ("Used item " + item.name);
			GameObject.Find ("MasterMind").GetComponent<Inventory>().RemoveItem(item);
		}else{
			//Debug.Log (item.name + " is the wrong item!");
		}
	}
}
