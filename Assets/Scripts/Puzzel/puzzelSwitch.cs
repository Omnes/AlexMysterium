using UnityEngine;
using System.Collections;

public class puzzelSwitch : MonoBehaviour {

	// Use this for initialization

	public GameObject light_on;
	public GameObject light_off;
	public GameObject switchOn;
	public GameObject switchOff;

	private Puzzel_Manager managerRef;
	private bool onOff = false;
	private bool leverStatus = false;
	//private bool interaction = false;

	void Start () {
		managerRef = transform.parent.GetComponent<Puzzel_Manager>();
		switchOn.SetActive(false);
		light_on.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(managerRef.CheckIfCorrect() && leverStatus){
			light_on.SetActive(true);
			light_off.SetActive(false);
		}else{
			light_off.SetActive(true);
			light_on.SetActive(false);
		}
	}

	void Interact(){
		if(managerRef != null){
			onOff = !onOff;
			if(onOff){
				leverStatus = true;
				switchOn.SetActive(true);
				switchOff.SetActive(false);
			}else{
				leverStatus = false;
				switchOff.SetActive(true);
				switchOn.SetActive(false);
			}
		}
	}
}
