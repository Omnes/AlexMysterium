using UnityEngine;
using System.Collections;

public class LockFuseBoxBeforePowerout : MonoBehaviour {

	// Use this for initialization
	void Start () {
		collider.enabled = GameObject.Find("MasterMind").GetComponent<ItemUseStates>().powerout;
	}

}
