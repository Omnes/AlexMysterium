using UnityEngine;
using System.Collections;

public class GUI_Return_Button : GUI_Button {

	public override void Start () {
		enabled = false;
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
	        }
			// rect(x,y,w,h)
			
			GUI.depth = guiDepth;
			
			//GUI.skin.button.onHover.textColor = Color.cyan;
			
	        if (GUI.Button(new Rect(XPos, YPos, Width, Height),aTexture , gui_style))
			{//, ScaleMode.ScaleToFit, true);
				Camera.main.SendMessage("Deactivate");
				Debug.Log("You pressed the button, VICTORY!!!!");
				Debug.Log("THE BUTTON SUB_CLASS!!!!");
				//use a queue
				
				
				//Camera.main.SendMessage("SetIsPuzzle", false);
				//GameObject.Find(name).GetComponent<GUI_Parent>().Activate(false);
				//Camera.main.SendMessage("exitPuzzle");
				
			}
			
			GUI.depth = guiDepth+1;
			
			GUI.Label(new Rect(XPos, YPos, Width, Height), "a button has appeared!" , gui_style);
		}
    }
	
	public override void Activate(bool x){
		enabled = x;
	}
}