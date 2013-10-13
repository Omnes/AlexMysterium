using UnityEngine;
using System.Collections;

public class GUI_Text_Label : GUI_Element {
	
	//public bool activate = false;
	public int guiDepth = 0;
	//public Font f;
	public float xMin = 60f;	
	public float yMin = 60f;
	public float xMax = 400f;
	public float yMax = 20f;
	
	public TextAsset asset;
	
	public GUIStyle gui_style;
	// Use this for initialization
	public override void Start () {
		enabled = false;
		print(asset.text);
		
		 /*if (!f) {
            Debug.LogError("No font found, assign one in the inspector.");
            return;
        }*/
	}
	
	// Update is called once per frame
	public override void Update () {
	
	}
	
	public override void OnGUI() {
		if(enabled)
		{
			//GUI.color = Color.black;
			GUI.depth = guiDepth;
			//GUI.skin.font = f;
        	GUI.Label(new Rect(xMin, yMin, xMax, yMax), asset.text, gui_style);
		}
    }
	
	public override void Activate(bool x){
		enabled = x;
	}
}
