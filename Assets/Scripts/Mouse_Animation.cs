using UnityEngine;
using System.Collections;

public class Mouse_Animation : MonoBehaviour {
	
	public int cursorWidth = 32;
	public int cursorHeight = 32; //blev bra ?
	public Texture2D cursorImage;
	//array?
	public Texture2D doorCursor;
	public Texture2D interactiveCursor;
	public Texture2D standardCursor;
	
	private float currentTime;
	private float maxTime = 0.05f;
	
	public bool isDrawing = true;
	
	
	// Use this for initialization
	void Start () {
		currentTime = Time.time;
		
		Screen.showCursor = false;
		//Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(currentTime + maxTime < Time.time){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if(Physics.Raycast(ray, out hit)){
				if(hit.transform.gameObject.layer == 8){//8 == Door Layer
					cursorImage = doorCursor;
				}else if(hit.transform.gameObject.layer == 9){//9 == Interactive Layer
					cursorImage = interactiveCursor;
				}else{
					cursorImage = standardCursor;
				}
			}
			currentTime = Time.time;
		}
			
		
	}
	
	public void OnGUI()
	{
		if(isDrawing){
			GUI.DrawTexture(new Rect(Input.mousePosition.x - 3, (Screen.height - Input.mousePosition.y) - 3, cursorWidth, cursorHeight), cursorImage, ScaleMode.ScaleAndCrop);
		}
	}
}
