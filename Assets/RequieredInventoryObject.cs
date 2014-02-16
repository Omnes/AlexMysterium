using UnityEngine;
using System.Collections;

public class RequieredInventoryObject : MonoBehaviour {
	public string reqItem;

	Inventory inv;
	int counter = 0;
	// Use this for initialization
	void Start () {
		inv = GameObject.Find ("MasterMind").GetComponent<Inventory>();
	}
	
	// Update is called once per frame
	void Update () {
		counter++;
		if(counter % 30 == 0){
			bool active = inv.checkItemSupply(reqItem,1);
			transform.GetChild(0).renderer.enabled = active;
			gameObject.collider.enabled = active;
		}
	}
}
