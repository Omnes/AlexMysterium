using UnityEngine;
using System.Collections;

public class Flashlight_Movement : MonoBehaviour {
	private bool gotFlashlight  = false;
	private ItemUseStates ius;

	// Use this for initialization
	void Start () {
		ius = GameObject.Find("MasterMind").GetComponent<ItemUseStates>();
		Vector2 normPos = new Vector2(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height);
		renderer.material.SetVector("_MousePos", new Vector4(-1f,-1f,0.0f,0.0f));
	}
	
	// Update is called once per frame
	void Update () {
		if(!gotFlashlight){
			gotFlashlight = ius.flashlight;
		}else{

			Vector2 normPos = new Vector2(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height);
			renderer.material.SetVector("_MousePos", new Vector4(normPos.x,normPos.y,0.0f,0.0f));
		}
	}
}
