using UnityEngine;
using System.Collections;

public class Traveltothepast : MonoBehaviour {

	public Texture2D white;
	public Texture2D pastBackground;
	public float fadeTime;
	bool fade = false;
	float changeCount = 1;
	Color alpha;
	bool donefading = false;
	float tick;
	float fadedelta;


	void Start(){

		alpha =  GameObject.Find ("Background").renderer.material.color;
		fadedelta = (1/fadeTime)*0.1f;
		transform.renderer.material.color = new Color(1,1,1,0);

	}

	void Update () {

		if(fade == true) {

			if(Time.time - tick >= 0.1f){
				Debug.Log ("fade = true");
				changeCount = changeCount - fadedelta;
				GameObject.Find("Background").renderer.material.color = new Color(alpha.r,alpha.g,alpha.b,changeCount); 

				if(changeCount <= 0) {
					fade = false;
					GameObject.Find ("Background").renderer.material.mainTexture = pastBackground;
					alpha = GameObject.Find ("Background").renderer.material.color;
					GameObject.Find ("Background").renderer.material.color = new Color(alpha.r,alpha.g,alpha.b,1);
					//GameObject.Find ("Background").renderer.material.mainTexture = white;
					//gameObject.Find("Background").renderer.material.color = new Color(1f,1f,1f,1);
				}

				tick = Time.time;
			}
		}
	}


	void Interact(){
	
		Debug.Log ("Interractade");
		fade = true;
		tick = Time.time;
	}
}
