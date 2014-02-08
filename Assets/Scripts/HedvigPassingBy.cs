using UnityEngine;
using System.Collections;
using UnityEditor;

public class HedvigPassingBy : MonoBehaviour {

	public Transform prefab;
	public Vector3 spawnposition;
	public float walkingspeed;
	public float pathlength;
	Vector3 direction = new Vector3(1,0,0);
	bool instantiated = false;
	float alpha;
	// Use this for initialization
	void Start () {
	
		alpha = 0.01f;
		Animationator hedvigani = gameObject.GetComponent<Animationator>();
		hedvigani.Animations = 2;
		hedvigani.Frames = 7;
		hedvigani.setWalkAnimation(direction);
		playAnimation();
		prefab.transform.GetChild(0).renderer.material.color = new Color(renderer.material.color.r,renderer.material.color.g,renderer.material.color.b,alpha);
		
	}
	
	// Update is called once per frame
	void Update () {

		if(instantiated){


			alpha += 0.01f;
			//prefab.transform.GetChild(0).renderer.material.color = new Color(renderer.material.color.r,renderer.material.color.g,renderer.material.color.b,alpha += 0.1);
		}

		float temppos = prefab.transform.position.x + walkingspeed;
		prefab.transform.position = new Vector3(temppos, prefab.transform.position.y, prefab.transform.position.z);

		if(Mathf.Abs(spawnposition.x - prefab.transform.position.x)> pathlength){

			Destroy(prefab.transform.gameObject); //Vet inte om detta duger
			//PrefabUtility.DisconnectPrefabInstance(prefab);
		}
	}

	void playAnimation(){

		Instantiate(prefab,spawnposition,prefab.rotation);
		instantiated = true;
	}
}
