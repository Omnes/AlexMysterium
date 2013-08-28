using UnityEngine;
using System.Collections;

public class graphic_label : MonoBehaviour {

	public bool activate = false;
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
	void Start () {
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
	void Update () {	
	
	}
	
    void OnGUI() {
		if(activate)
		{
	        if (!aTexture) {
	            Debug.LogError("Assign a Texture in the inspector.");
	            return;
	        }// rect(x,y,w,h)
			
			GUI.depth = guiDepth;
			
	        GUI.DrawTexture(new Rect(XPos, YPos, Width, Height), aTexture);//, gui_style);//, ScaleMode.ScaleToFit, true);
		}
    }
}




























