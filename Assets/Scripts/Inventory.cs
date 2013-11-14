using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour {
	
	
	public List<Item> inventoryList;
	private int startItem = 0;
	public int displayCount = 5;
	
	public bool showInventory = false;
	private bool drawInventory = false;
	private float offset = 0;
	
	public Vector2 spriteSize = new Vector2(64,64);
	private Vector2 startPosition = new Vector2(Screen.width,Screen.height);
	public Vector2 paddingFromEdge;
	
	private Item holdingItem;
	private Item nullItem;
	private Rect inventoryArea;
	private int holdingItemNr = -1;
	
	private int ItemPickupCounter = 0;
	
	//seans holding item icon thingy
	public Texture2D cursorImage;
	Mouse_Animation mo_anim;
	
	
	
	void Start () {
		inventoryList = new List<Item>();
		nullItem = new Item("null",null,-1);
		holdingItem = nullItem;
		
		//seans holding item icon thingy
		mo_anim = GetComponent<Mouse_Animation>();
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
	
	Item findItemByName(List<Item> list,string name){
		foreach(Item i in list){
			if(i.name == name){
				return i;
			}
		}
		return new Item("null",null,-1);
	}
	
	public bool useItem(string name,bool consume){
		Item i = findItemByName(inventoryList,name);
		if(i.name == null) return false;
		if(consume) inventoryList.Remove(i);
		return true;
		
	}
	
	public List<Item> GetInventoryList(){
		return inventoryList;
	}
	
	public void DoGUI(){
		
		if(offset > 0){
			for(int i = startItem; i < inventoryList.Count && i < startItem+displayCount; i++){
				if(i != holdingItemNr){
					Rect pos = new Rect(startPosition.x-spriteSize.x*(i-startItem+1)+spriteSize.x*inventoryList.Count-offset - paddingFromEdge.x,startPosition.y-spriteSize.y - paddingFromEdge.y,spriteSize.x,spriteSize.y);
					GUI.DrawTexture(pos,inventoryList[i].sprite);
				}
			}
		}
		if(holdingItem.id != nullItem.id){
			GUI.DrawTexture(new Rect(Input.mousePosition.x-spriteSize.x/2,Screen.height-Input.mousePosition.y-spriteSize.y/2,spriteSize.x,spriteSize.y),holdingItem.sprite);
			
			//seans holding item icon thingy
			mo_anim.isDrawing = false;
			GUI.DrawTexture(new Rect(Input.mousePosition.x - 3, (Screen.height - Input.mousePosition.y) - 3, 32, 32), cursorImage, ScaleMode.ScaleAndCrop);
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
		/*
		if(mousePos.y < spriteSize.y){
			showInventory = true;
		}else{
			showInventory = false;
		}
		*/
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
					if(Screen.width-mousePos.x - paddingFromEdge.x < inventoryList.Count*spriteSize.x + paddingFromEdge.x && mousePos.y < spriteSize.y + paddingFromEdge.y){
						//plocka upp
						holdingItemNr = startItem + (int)Mathf.Floor((Screen.width-mousePos.x  - paddingFromEdge.x)/spriteSize.x);
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
					mo_anim.isDrawing = true;
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
