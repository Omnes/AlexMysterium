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
			gameObject.SetActive(inv.checkItemSupply(reqItem,1));
		}
	}
}
