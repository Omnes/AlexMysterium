using UnityEngine;
using System.Collections;
using UnityEditor;

public class HedvigPassingBy : MonoBehaviour {

	public Transform prefab;
	private Transform instance;
	public Vector3 spawnposition;
	public float walkingspeed;
	public float pathlength; 
	public AudioClip fadesound;		//Kanske får flytta till traveltothepast senar
	public AudioSource soundsource;
	bool instantiated = false;
	float alpha;
	Hedviganimation hedvigani; 
	Traveltothepast past; 


	// Use this for initialization
	void Start () {
	 
		soundsource = gameObject.GetComponent<AudioSource>();
	//	ghostsound = gameObject.GetComponent<AudioSource>();	
		playAnimation();
		hedvigani = instance.GetComponent<Hedviganimation>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(instantiated){

			float temppos = instance.position.x + walkingspeed;
			instance.position = new Vector3(temppos, instance.position.y, instance.position.z);
			
			//Debug.Log ("ska ha flyttat sig framåt här");
			
			if(Mathf.Abs(spawnposition.x - instance.position.x)> pathlength){
				Destroy(instance.gameObject); //Vet inte om detta duger
				//PrefabUtility.DisconnectPrefabInstance(prefab);
				instantiated = false;
				past = gameObject.GetComponent<Traveltothepast>();
				past.timeTraveltoPast();
			}

			if(Mathf.Abs(spawnposition.x - instance.position.x)> pathlength - 3){

				hedvigani.startfadingaway();
			}

			if(hedvigani.gone){

				soundsource.clip = fadesound;
			} 
		}
	}


	public void playAnimation(){

		Debug.Log ("Hallå ja");
		instance = Instantiate(prefab,spawnposition,prefab.rotation) as Transform;
		instantiated = true;
		soundsource.Play();
		//audio.clip = ghostsound;
		Debug.Log("Nu spelas ljudet");
		//audio.timeSamples = 5000;
	//	audio.Play();
		Debug.Log ("Instansiated ghost");
	}

}
