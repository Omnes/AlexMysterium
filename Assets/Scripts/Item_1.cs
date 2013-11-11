using UnityEngine;
using System.Collections;

public class Item_1 : MonoBehaviour {
	
	private Texture2D onTouchMaterial;
	
	// Use this for initialization
	void Start () {
	
		onTouchMaterial = (Texture2D) renderer.material.GetTexture("_BumpMap");
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseEnter(){
		
		Cursor.SetCursor(onTouchMaterial, Vector2.zero, CursorMode.Auto);
		
	}
	
	void OnMouseExit(){
		
		//Cursor.SetCursor(null, Vector2(0f,0f), CursorMode.Auto);
		
	}
}
