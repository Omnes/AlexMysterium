using UnityEngine;
using System.Collections;

public class SaveTheRadio : MonoBehaviour {
	public int offset = 0;
	public Radio radioRef = null;




	public void OnLevelWasLoaded(){
		if(radioRef == null){
			radioRef = GameObject.Find ("Radio").GetComponent<Radio>();
			if(radioRef == null){
				this.enabled = false;
			}
		}


		//saveOffset();
	}

	public void saveOffset(){
		if(radioRef != null){
			radioRef.receiveOffset(offset);
			
		}
	}

	public void OnDestroy(){
		if(radioRef != null){
			offset = radioRef.sendOffset();
		}
	}
}
