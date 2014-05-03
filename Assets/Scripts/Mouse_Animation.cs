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

	//new texure for highlight. sean 2014-05-03
	public Texture2D highlightCursor;
	
	public Vector2 mouseDisp = new Vector2(3.0f,3.0f);
	
	private float currentTime;
	public float maxTime = 0.05f;
	
	public bool isDrawing = true;
	public bool isInInventory = false;
	
	
	// Use this for initialization
	void Start () {
		currentTime = Time.time;
		
		Screen.showCursor = false;
		//Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(isDrawing){
			if(currentTime + maxTime < Time.time){
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				
				if(Physics.Raycast(ray, out hit)){
					if(hit.transform.gameObject.layer == 8 && !isInInventory){//8 == Door Layer
						cursorImage = doorCursor;
					}else if(hit.transform.gameObject.layer == 9  && !isInInventory){//9 == Interactive Layer
						cursorImage = interactiveCursor;
					}else if(hit.transform.gameObject.layer == 12  && !isInInventory){//9 == Interactive Layer
						cursorImage = highlightCursor;
					}else{
						cursorImage = standardCursor;
					}
				}
				currentTime = Time.time;
			}
		}
		
	}
	
	public void OnGUI()
	{
		if(isDrawing){
			GUI.DrawTexture(new Rect(Input.mousePosition.x - mouseDisp.x, (Screen.height - Input.mousePosition.y) - mouseDisp.y, cursorWidth, cursorHeight), cursorImage, ScaleMode.ScaleAndCrop);
		}
	}
}
