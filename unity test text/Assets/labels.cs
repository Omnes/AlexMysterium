using UnityEngine;
using System.Collections;

public class labels : MonoBehaviour {

	public float xMin = 10f;	
	public float yMin = 10f;
	public float xMax = 400f;
	public float yMax = 20f;
	
	public TextAsset asset;
	
	// Use this for initialization
	void Start () {
		 print(asset.text);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
        GUI.Label(new Rect(xMin, yMin, xMax, yMax), asset.text);
    }
}
