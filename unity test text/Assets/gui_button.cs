using UnityEngine;
using System.Collections;

public class gui_button : MonoBehaviour {
	
	public bool activate = false;
	public Texture aTexture;
	public Texture onHoverTexture;
	public int guiDepth = 0;
	public float XPos = 0f;
	public float YPos = 0f;
	public float Width = 0f;
	public float Height = 0f;
	
	public GUIStyle gui_style;
	// Use this for initialization
	void Start () {
	
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
			
			GUI.skin.button.onHover.textColor = Color.cyan;
			
	        if (GUI.Button(new Rect(XPos, YPos, Width, Height), aTexture, gui_style))
			{//, ScaleMode.ScaleToFit, true);
				Debug.LogError("You pressed the button, VICTORY!!!!");
			}
		}
    }
}
