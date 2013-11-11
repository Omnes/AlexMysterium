using UnityEngine;
using System.Collections;

public class GUI_Graphic_Label : GUI_Element {

	//public bool activate = false;
	public Texture aTexture;
	public bool useScreenWidth = true;
	public bool useScreenHeight = true;
	public int guiDepth = 0;
	public float XPos = 0f;
	public float YPos = 0f;
	public float Width = 0f;
	public float Height = 0f;
	
	public GUIStyle gui_style;
	// Use this for initialization
	public override void Start () {
		enabled = false;	
		
		if(useScreenHeight)
		{
			Width = Screen.width;
			Debug.LogError("useScreenHeight: " + Screen.width);
		}
		if(useScreenWidth)
		{
			Height = Screen.height;
			Debug.LogError("Screen.width: " + Screen.height);
		}
		
		//GUI.depth = guiDepth;
	}
	
	// Update is called once per frame
	public override void Update () {	
	
	}
	
  	public override void OnGUI() {
		if(enabled)
		{
	        if (!aTexture) {
	            Debug.LogError("Assign a Texture in the inspector.");
	            return;
	        }// rect(x,y,w,h)
			
			GUI.depth = guiDepth;
			
	        GUI.DrawTexture(new Rect(XPos, YPos, Width, Height), aTexture);//, gui_style);//, ScaleMode.ScaleToFit, true);
		}
    }
	
	public override void Activate(bool x){
		enabled = x;
	}
}




























