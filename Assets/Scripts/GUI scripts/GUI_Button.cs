using UnityEngine;
using System.Collections;

public class GUI_Button : GUI_Element {
	
	//****************************************
	// Göra om klassen så den använder texturen som kan sparas i gui-styles
	//
	//***************************************
	
	//public bool activate = false;
	public Texture aTexture;
	
	//public GUI_Button buttonInstance;
	//public Texture onHoverTexture;
	public int guiDepth = 0;
	public float XPos = 0f;
	public float YPos = 0f;
	public float Width = 0f;
	public float Height = 0f;
	
	public GUIStyle gui_style;
	// Use this for initialization
	public override void Start () {
		//enabled = false;
	}
	
	// Update is called once per frame
	public override void Update () {
	
	}
	
	public override void OnGUI() {
		/*if(enabled)
		{
	        if (!aTexture) {
	            //Debug.LogError("Assign a Texture in the inspector.");
	            return;
	        }
			// rect(x,y,w,h)
			
			GUI.depth = guiDepth;
			
			//GUI.skin.button.onHover.textColor = Color.cyan;
			
	        if (GUI.Button(new Rect(XPos, YPos, Width, Height),aTexture , gui_style))
			{//, ScaleMode.ScaleToFit, true);
				//Debug.LogError("You pressed the button, VICTORY!!!!");
				
			}
			
			GUI.depth = guiDepth+1;
			
			GUI.Label(new Rect(XPos, YPos, Width, Height), "a button has appeared!" , gui_style);
		}*/
    }
	
	public override void Activate(bool x){
		//enabled = x;
	}
}
