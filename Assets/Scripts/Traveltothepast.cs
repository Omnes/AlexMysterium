using UnityEngine;
using System.Collections;

public class Traveltothepast : MonoBehaviour {

	public Texture2D white;
	public Texture2D pastBackground;
	public float fadeTime;
	public string objecttag;
	public string darklayertag;
	public string pastobjecttag;
	public Music_Master musicRef;
//	public AudioSource fadesound;
	bool fade = false;
	float changeCount = 1;
	Color alpha;
	Color objectcolor;
	bool donefading = false;
	float tick;
	float fadedelta;


	void Start(){

	//	fadesound = gameObject.GetComponent<AudioSource>();	
		alpha =  GameObject.Find ("Background").renderer.material.color;
		objectcolor = GameObject.Find (pastobjecttag).renderer.material.color;
		GameObject.Find (pastobjecttag).renderer.material.color = new Color(objectcolor.r,objectcolor.g,objectcolor.b,0);
		fadedelta = (1/fadeTime)*0.1f;
		transform.renderer.material.color = new Color(1,1,1,0);

	}

	void Update () {

		if(fade == true) {

			if(Time.time - tick >= 0.1f){
				//Debug.Log ("fade = true");
				changeCount = changeCount - fadedelta;
				GameObject.Find(objecttag).renderer.material.color = new Color(alpha.r,alpha.g,alpha.b,changeCount);
				GameObject.Find(darklayertag).renderer.material.color = new Color(alpha.r,alpha.g,alpha.b,changeCount);
				GameObject.Find("Background").renderer.material.color = new Color(alpha.r,alpha.g,alpha.b,changeCount); 

				if(changeCount <= 0) {	
					musicRef.Enable(true);//HÄR SKA MUSIKEN BÖRJA SPELAS IGEN!!!!!!!!!!!
					fade = false;
					GameObject.Find ("Background").renderer.material.mainTexture = pastBackground;
					GameObject.Find (pastobjecttag).renderer.material.color = new Color(objectcolor.r,objectcolor.g,objectcolor.b,1);
					alpha = GameObject.Find ("Background").renderer.material.color;
					GameObject.Find ("Background").renderer.material.color = new Color(alpha.r,alpha.g,alpha.b,1);
					//GameObject.Find ("Background").renderer.material.mainTexture = white;
					//gameObject.Find("Background").renderer.material.color = new Color(1f,1f,1f,1);
				}

				tick = Time.time;
			}
		}
	}


	public void timeTraveltoPast(){
	
	//	fadesound.Play();
		Debug.Log ("Interractade");
		fade = true;
		tick = Time.time;
	}
}
