using UnityEngine;
using System.Collections;

public class Gui_Control : MonoBehaviour {

	public Texture2D gui_texture;
	
	Inventory inventory;
	
	private Rect guiArea;
	
	private Vector2 startPosition = new Vector2(Screen.width,Screen.height);
	Rect inventoryArea;
	// Use this for initialization
	void Start () {
		startPosition = new Vector2(Screen.width,Screen.height);
		inventory = GetComponent<Inventory>();
		float w = Screen.width / 3;
		float h = Screen.height / 5;
		//guiArea = new Rect(Screen.width-w,Screen.height-w,w,w);
		float guiImageSize = 256;
		guiArea = new Rect(Screen.width-guiImageSize,Screen.height-guiImageSize,guiImageSize,guiImageSize);
		//ersätt med en knapp eller något som matchar guiknappen vi ska ha
		inventoryArea = new Rect (startPosition.x-inventory.spriteSize.x, startPosition.y-inventory.spriteSize.y, inventory.spriteSize.x,inventory.spriteSize.y);
		//float ia = w/4;
		//inventoryArea = new Rect (Screen.width - ia - ia/10, Screen.height - ia - ia/10,ia,ia);
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
			if(mousePos.y < (inventoryArea.y - inventoryArea.height)){
				inventory.showInventory = false;
			}
		}
	}
	
	void OnGUI(){
		
		inventory.DoGUI();
		GUI.DrawTexture(guiArea,gui_texture);
		//GUI.Box(inventoryArea,"DONT FORGET TO REMOVE");
		
	}
}
