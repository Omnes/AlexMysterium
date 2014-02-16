using UnityEngine;
using System.Collections;

public class Traveltothepast : MonoBehaviour {

	public Texture2D pastBackground;
	public float fadeTime;
	public string objecttag;
	public string darklayertag;
	public string pastobjecttag;
	public Music_Master musicRef;
	public Transform blacklayertag;
	public Transform logotag;
	public float startlogofade;
//	public AudioSource fadesound;
	bool fade = false;
	bool fadetologo = false;
	float changeCount = 1;
	Color alpha;
	Color objectcolor;
	Color logocolor;
	bool donefading = false;
	float tick;
	float fadedelta;
	Gui_Control gui;


	void Start(){

		gui = GameObject.Find ("MasterMind").GetComponent<Gui_Control>();
		gui.drawGUI = false;
		Color blackcolor = blacklayertag.renderer.material.color;
		blacklayertag.renderer.material.color = new Color(blackcolor.r, blackcolor.g, blackcolor.b,0);
		//Debug.Log (blacklayertag.renderer.material.color.a);
		logocolor = logotag.renderer.material.color;
		logotag.renderer.material.color = new Color(logocolor.r,logocolor.g,logocolor.b,0);
	//	fadesound = gameObject.GetComponent<AudioSource>();	
		alpha =  GameObject.Find ("Background").renderer.material.color;
		objectcolor = GameObject.Find (pastobjecttag).renderer.material.color;
		GameObject.Find (pastobjecttag).renderer.material.color = new Color(objectcolor.r,objectcolor.g,objectcolor.b,0);
		fadedelta = (1/fadeTime)*0.1f;
		transform.renderer.material.color = new Color(1,1,1,0);

	}

	void Update () {

		if(fade) {

			if(Time.time - tick >= 0.1f){
				////Debug.Log ("fade = true");
				changeCount = changeCount - fadedelta;
				GameObject.Find(objecttag).renderer.material.color = new Color(alpha.r,alpha.g,alpha.b,changeCount);
				GameObject.Find(darklayertag).renderer.material.color = new Color(alpha.r,alpha.g,alpha.b,changeCount);
				GameObject.Find("Background").renderer.material.color = new Color(alpha.r,alpha.g,alpha.b,changeCount); 

				if(changeCount <= 0) {	
					//musicRef.Enable(true);//HÄR SKA MUSIKEN BÖRJA SPELAS IGEN!!!!!!!!!!!
					fade = false;
					GameObject.Find ("Background").renderer.material.mainTexture = pastBackground;
					GameObject.Find (pastobjecttag).renderer.material.color = new Color(objectcolor.r,objectcolor.g,objectcolor.b,1);
					alpha = GameObject.Find ("Background").renderer.material.color;
					GameObject.Find ("Background").renderer.material.color = new Color(alpha.r,alpha.g,alpha.b,1);
					fadetologo = true;
					changeCount = 0;
					//GameObject.Find ("Background").renderer.material.mainTexture = white;
					//gameObject.Find("Background").renderer.material.color = new Color(1f,1f,1f,1);
				}

				tick = Time.time;
			}
		}

		if(fadetologo){
			if(Time.time - tick >= startlogofade){
				if(Time.time - tick >= 0.1f){
					blacklayertag.renderer.material.color = new Color(0,0,0,changeCount);
					logotag.renderer.material.color = new Color(logocolor.r,logocolor.g,logocolor.b,changeCount);
					changeCount += 0.01f;
			}
		}
	}
}


	public void timeTraveltoPast(){
	
	//	fadesound.Play();
		//Debug.Log ("Interractade");
		fade = true;
		tick = Time.time;
	}
}
