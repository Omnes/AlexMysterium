using UnityEngine;
using System.Collections;

public class Mouse_Animation : MonoBehaviour {
	
	private int cursorWidth = 32;
	private int cursorHeight = 36; //blev bra ?
	public Texture2D cursorImage;
	//array?
	public Texture2D cursor1;
	public Texture2D cursor2;
	public Texture2D standardCursor;
	
	private float currentTime;
	private float maxTime = 0.05f;
	
	
	// Use this for initialization
	void Start () {
		currentTime = Time.time;
		
		Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(currentTime + maxTime < Time.time){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if(Physics.Raycast(ray, out hit)){
				if(hit.transform.gameObject.layer == 8){//8 == Door Layer
					cursorImage = cursor1;
				}else if(hit.transform.gameObject.layer == 9){//9 == Interactive Layer
					cursorImage = cursor2;
				}else{
					cursorImage = standardCursor;
				}
			}
			currentTime = Time.time;
		}
			
		
	}
	
	void OnGUI()
	{

		GUI.DrawTexture(new Rect(Input.mousePosition.x - 3, (Screen.height - Input.mousePosition.y) - 3, cursorWidth, cursorHeight), cursorImage, ScaleMode.ScaleAndCrop);
		
	}
}
