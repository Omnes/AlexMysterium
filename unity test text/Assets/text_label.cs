using UnityEngine;
using System.Collections;

public class text_label : MonoBehaviour {
	
	public bool activate = false;
	public int guiDepth = 0;
	public Font f;
	public float xMin = 60f;	
	public float yMin = 60f;
	public float xMax = 400f;
	public float yMax = 20f;
	
	public TextAsset asset;
	
	// Use this for initialization
	void Start () {
		 print(asset.text);
		
		 if (!f) {
            Debug.LogError("No font found, assign one in the inspector.");
            return;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		if(activate)
		{
			GUI.color = Color.black;
			GUI.depth = guiDepth;
			GUI.skin.font = f;
        	GUI.Label(new Rect(xMin, yMin, xMax, yMax), asset.text);
		}
    }
}
