using UnityEngine;
using System.Collections;

public class InitiateFirstScene : MonoBehaviour {

	public GameObject masterMind;
	public string firstLevel;

	// Use this for initialization
	void Start () {
		Application.LoadLevel(firstLevel);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
