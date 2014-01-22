using UnityEngine;
using System.Collections;

public class Gui_Control : MonoBehaviour {

	public Texture2D gui_texture;
	
	Inventory inventory;
	
	private Vector2 startPosition = new Vector2(Screen.width,Screen.height);
	Rect inventoryArea;
	// Use this for initialization
	void Start () {
		inventory = GetComponent<Inventory>();
		//ersätt med en knapp eller något som matchar guiknappen vi ska ha
		inventoryArea = new Rect (startPosition.x-inventory.spriteSize.x,startPosition.y-inventory.spriteSize.y,inventory.spriteSize.x,inventory.spriteSize.y);
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector2 mousePos = Input.mousePosition;
		mousePos.y = Screen.height-mousePos.y; 
		if(!inventory.showInventory){
			if(inventoryArea.Contains(mousePos) && inventory.GetInventoryList().Count > 0){
				inventory.showInventory = true;
			}
		}else{
			if(mousePos.y < Screen.height-(inventory.spriteSize.y+inventory.paddingFromEdge.y)){
				inventory.showInventory = false;
			}
		}
	}
	
	void OnGUI(){
		Rect screenSize = new Rect(0,0,Screen.width,Screen.height);
		GUI.DrawTexture(screenSize,gui_texture);
		//GUI.Box(inventoryArea,"!");
		inventory.DoGUI();
		
	}
}
