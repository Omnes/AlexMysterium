using UnityEngine;
using System.Collections;
using UnityEditor;

public class HedvigPassingBy : MonoBehaviour {

	public Transform prefab;
	public Vector3 spawnposition;
	public float walkingspeed;
	public float pathlength;
	Vector3 direction = new Vector3(1,0,0);
	GameObject hedvig;
	// Use this for initialization
	void Start () {
	
		Animationator hedvigani = gameObject.GetComponent<Animationator>();
		hedvigani.Animations = 2;
		hedvigani.Frames = 7;
		hedvigani.setWalkAnimation(direction);
	}
	
	// Update is called once per frame
	void Update () {

		float temppos = hedvig.transform.position.x + walkingspeed;
		hedvig.transform.position = new Vector3(temppos, hedvig.transform.position.y, hedvig.transform.position.z);

		if(prefab.position.x - spawnposition.x > pathlength){

			PrefabUtility.DisconnectPrefabInstance(prefab);
		}
	}

	void playAnimation(){

		hedvig = (GameObject) Instantiate(prefab,spawnposition,prefab.rotation);
	}
}
