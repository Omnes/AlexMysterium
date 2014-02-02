using UnityEngine;
using System.Collections;
using UnityEditor;

public class HedvigPassingBy : MonoBehaviour {

	public Transform prefab;
	public Vector3 spawnposition;
	public float walkingspeed;
	public float pathlength;
	Vector3 direction = new Vector3(1,0,0);

	// Use this for initialization
	void Start () {

		Animationator hedvigani = gameObject.GetComponent<Animationator>();
		hedvigani.Animations = 2;
		hedvigani.Frames = 7;
	}
	
	// Update is called once per frame
	void Update () {
	
		prefab.position += walkingspeed;

		if(prefab.position.x - spawnposition.x > pathlength){

			PrefabUtility.DisconnectPrefabInstance(prefab);
		}
	}

	void playAnimation(){

		Instantiate(prefab,spawnposition,prefab.rotation);
		hedvigani.setWalkAnimation(direction);
	}
}
