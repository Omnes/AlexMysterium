using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUI_Parent : MonoBehaviour {
	
	//***********************************************************************************************************
	// GUI-PARENT works like a controll node for all the diffrent gui-scripts which has been assigned to it.
	// * It's able to activate and deactivate them in groups thus making activation of several gui-scripts easy.
	// To do this the gui-scripts have beend changed to use polymorphism with an abstract base class <gui-element> 
	//		and several diffrent child-classes that override the abstract functions present in gui-element
	
	
	//***********************************************************************************************************
	
	public List<GUI_Element> elements;
	// Use this for initialization
	void Start () {
	
		//create the list
		elements = new List<GUI_Element>();
		
		// collect all the gui-elements from the current GameObject.
		Component[] guiComp = gameObject.GetComponents(typeof(GUI_Element));
		foreach(Component comp in guiComp){
				addGuiElement(comp as GUI_Element); // if they are a subclass they get added in as a base-class
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Activate(bool currentState){// TypeOfElement var (to use) in ListOfElements
		foreach(GUI_Element current_element in elements){
			current_element.Activate(currentState);
		}
	}
	
	void addGuiElement(GUI_Element x){ //add a element to the list
		elements.Add(x);	
	}
}
  