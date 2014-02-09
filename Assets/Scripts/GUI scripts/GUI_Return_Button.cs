using UnityEngine;
using System.Collections;

public class GUI_Return_Button : GUI_Button {


	float lastActivateTime = 0;
	public float cooldown = 1;

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
			
	        if (GUI.Button(new Rect(XPos, YPos, Width, Height),aTexture , gui_style) && lastActivateTime + cooldown < Time.time)
			{//, ScaleMode.ScaleToFit, true);
				lastActivateTime = Time.time;
				GameObject.Find("MasterMind").SendMessage("Deactivate");
				Debug.Log("You pressed the button, VICTORY!!!!");
				//use a queue
				
				
				//Camera.main.SendMessage("SetIsPuzzle", false);
				//GameObject.Find(name).GetComponent<GUI_Parent>().Activate(false);
				//Camera.main.SendMessage("exitPuzzle");
				
			}
			
		}
    }

	public void DeactivateStuff(){
		enabled = false;
	}
	
	public override void Activate(bool x){
		enabled = x;
	}
}
