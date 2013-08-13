using UnityEngine;
using System.Collections;

public class PlayerMovement_KanTasbort : MonoBehaviour {
	
	public Vector3 inputMovement;
	public float movementSpeed;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		FindInput();
		
	}
	
	//Hittar playerinput och gör movement
	void FindInput(){
		
		inputMovement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f) * movementSpeed * Time.deltaTime;
		
		rigidbody.AddForce(inputMovement);
		
	}
}
