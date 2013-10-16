using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour {
	
	
	private List<Item> inventoryList;
	private int startItem = 0;
	public int displayCount = 3;
	
	private bool showInventory = false;
	private bool drawInventory = false;
	public float offset = 0;
	
	private Vector2 spriteSize = new Vector2(50,50);
	private Vector2 startPosition = new Vector2(Screen.width,Screen.height);
	
	private Item holdingItem;
	private Item nullItem;
	private Rect inventoryArea;
	private int holdingItemNr = -1;
	
	private int ItemPickupCounter = 0;
	
	void Start () {
		inventoryList = new List<Item>();
		nullItem = new Item("null",null,-1);
		holdingItem = nullItem; 
	}
	
	public void AddItem (GameObject itemPickup) {
		itemPickup.SetActive(false);
		Item item = new Item(itemPickup.name,(Texture2D)itemPickup.renderer.material.mainTexture,ItemPickupCounter);
		inventoryList.Add(item);
		ItemPickupCounter++;
	}
	
	public void RemoveItem(Item item){
		inventoryList.Remove(item);
	}
	
	public List<Item> GetInventoryList(){
		return inventoryList;
	}
	
	void OnGUI(){
		
		if(offset > 0){
			for(int i = startItem; i < inventoryList.Count && i < startItem+displayCount; i++){
				if(i != holdingItemNr){
					Rect pos = new Rect(startPosition.x-spriteSize.x*(i-startItem+1)+spriteSize.x*inventoryList.Count-offset,startPosition.y-spriteSize.y,spriteSize.x,spriteSize.y);
					GUI.DrawTexture(pos,inventoryList[i].sprite);
				}
			}
		}
		if(holdingItem.id != nullItem.id){
			GUI.DrawTexture(new Rect(Input.mousePosition.x-spriteSize.x/2,Screen.height-Input.mousePosition.y-spriteSize.y/2,spriteSize.x,spriteSize.y),holdingItem.sprite);
		}

	}
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			if(startItem > 0){
				startItem--;
			}
		}
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			if(startItem < inventoryList.Count && inventoryList.Count - startItem >= displayCount)
				startItem++;
		}
		
		inventoryArea = new Rect (startPosition.x,startPosition.y,-spriteSize.x*inventoryList.Count,-spriteSize.y);
		
		Vector2 mousePos = Input.mousePosition;
		
		//if(mousePos.y < spriteSize.y){
		if(mousePos.y < spriteSize.y){
			showInventory = true;
		}else{
			showInventory = false;
		}
		
		if(holdingItem.id != nullItem.id){
			showInventory = true;
		}
		
		if(showInventory && offset < spriteSize.x*inventoryList.Count){
			offset += (spriteSize.x*inventoryList.Count-offset)/2+0.01f;
		}
		
		if(!showInventory && offset > 0){
			offset = (offset)/2-0.01f;
		}
		
		
		
		if(showInventory){
			if(Input.GetMouseButtonDown(0)){
				if(holdingItem.id == nullItem.id){
					if(Screen.width-mousePos.x < inventoryList.Count*spriteSize.x && mousePos.y < spriteSize.y){
						//plocka upp
						holdingItemNr = startItem + (int)Mathf.Floor((Screen.width-mousePos.x)/spriteSize.x);
						holdingItem = inventoryList[holdingItemNr];
						Debug.Log("plockade upp: " + holdingItem.name);
					}
				}
			}
			if(Input.GetMouseButtonUp(0)){
				if(holdingItem.id != nullItem.id){
					//släpp itemet
					
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;
					if(Physics.Raycast(ray, out hit)){
						if(hit.transform.tag == "Combinable"){
							GetComponent<InputManager>().UseItemOnTarget(hit.transform,holdingItem);
							
						}
					}	
					
					Debug.Log("släpper: " + holdingItem.name);
					holdingItem = nullItem;
					holdingItemNr = -1;
					//ta reda på vad vi släppte objektet över och skicka ett meddelande till det
				}
			}
		}
		
	}
}




public struct Item{
	
	public string 		name;
	public Texture2D 	sprite;
	public int 			id;
	
	public Item(string name,Texture2D sprite,int id){
		this.name = name;
		this.sprite = sprite;
		this.id = id;
	}
	/*
	public static Item operator ==(Item i1, Item i2){
    	return i1.name == i2.name;
    }
	public static Item operator !=(Item i1, Item i2){
    	return i1.name != i2.name;
    }
	*/
	
}