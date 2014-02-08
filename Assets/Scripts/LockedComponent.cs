﻿using UnityEngine;
using System.Collections;

public class LockedComponent : MonoBehaviour {

	public MonoBehaviour component;
	public bool locked = true;
	public string key = "key";
	public bool consumeKey = true;

	// Use this for initialization
	void Start () {
		setlock(locked);
	
	}

	void UseItem(Item item){
		Debug.Log(item.name);
		if(item.name == key){
			GameObject.Find("MasterMind").GetComponent<Inventory>().useItem(key,consumeKey);
			setlock(false);
		}
	}

	void setlock(bool state){
		locked = state;
		component.enabled = !locked;
	}

}