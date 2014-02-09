using UnityEngine;
using System.Collections;
using UnityEditor;

public class HedvigPassingBy : MonoBehaviour {

	public Transform prefab;
	private Transform instance;
	public Vector3 spawnposition;
	public float walkingspeed;
	public float pathlength; 
	bool instantiated = false;
	float alpha;
	Hedviganimation hedvigani; 
	Traveltothepast past; 

	// Use this for initialization
	void Start () {
	 
		//alpha = 0.01f;
		playAnimation();
		//renderer.material.SetTexture("_MainTex",playerMat.GetTexture("_MainTex"));
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
		//	alpha += 0.01f;
			//prefab.transform.GetChild(0).renderer.material.color = new Color(renderer.material.color.r,renderer.material.color.g,renderer.material.color.b,alpha += 0.01f);
		}
		//	}
	}


	public void playAnimation(){

		Debug.Log ("Hallå ja");
		instance = Instantiate(prefab,spawnposition,prefab.rotation) as Transform;
		instantiated = true;
		Debug.Log ("Instansiated ghost");
	}

}
