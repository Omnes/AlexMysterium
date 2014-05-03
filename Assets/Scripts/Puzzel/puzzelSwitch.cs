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

	public GameObject m_masterMind;
	private bool m_switch = false;

	void Start () {
		m_masterMind = GameObject.Find("MasterMind");

		managerRef = transform.parent.GetComponent<Puzzel_Manager>();
		switchOn.SetActive(false);
		light_on.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		//fråga sean
		if(!m_switch){
			if(managerRef.CheckIfCorrect() && leverStatus){
				m_switch = true;
				light_on.SetActive(true);
				light_off.SetActive(false);
				m_masterMind.GetComponent<Inventory>().removeItems("item_propp1",1);
				m_masterMind.GetComponent<Inventory>().removeItems("item_propp2",1);
				m_masterMind.GetComponent<Inventory>().removeItems("item_propp3",1);
				m_masterMind.GetComponent<Inventory>().removeItems("item_propp4",1);
				m_masterMind.GetComponent<Inventory>().removeItems("item_propp5",1);
				m_masterMind.SendMessage("setAvbrott",false);
			}else{
				light_off.SetActive(true);
				light_on.SetActive(false);
			}
		}
	}

	void Interact(){
		//fråga sean
		if(!m_switch){
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
}
