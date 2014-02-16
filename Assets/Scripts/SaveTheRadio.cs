using UnityEngine;
using System.Collections;

public class SaveTheRadio : MonoBehaviour {
	public ulong offset = 0;
	public Radio radioRef;

	void Awaken(){
		//radioRef = GameObject.Find ("Radio").GetComponent<Radio>();
	}
	// Use this for initialization
	void Start () {
		radioRef = GameObject.Find ("Radio").GetComponent<Radio>();

		if(radioRef == null){
			this.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnLevelWasLoaded(){
		if(radioRef == null){
			radioRef = GameObject.Find ("Radio").GetComponent<Radio>();
			if(radioRef == null){
				this.enabled = false;
			}
		}


		saveOffset();
	}

	public void saveOffset(){
		if(radioRef != null){
			offset = (ulong) radioRef.sendOffset();
		}
	}

	public void OnDestroy(){
		if(radioRef != null){
			radioRef.receiveOffset(offset);
		}
	}
}
