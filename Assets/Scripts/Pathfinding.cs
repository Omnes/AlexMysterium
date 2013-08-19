using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Robin

public class Pathfinding : MonoBehaviour {
	
	
	public Texture2D pixelMap;
	public Transform walkmesh;
	
	
	public Material debugMat1;
	public GameObject debugNodePrefab;
	public bool debug = false;
	
	public Node[,] map;
	public List<Node> path;
	public List<Node> mapclosed;
	
	private int height;
	private int width;
	
	void Start () {
		
		height = pixelMap.height;
		width = pixelMap.width;
		path = new List<Node>();
		mapclosed = new List<Node>();
		
		initMap();
		if(debug) drawNodes();
	}
	
	public void drawNodes(){
		for(int y = 0; y < height; y++)
			for(int x = 0; x < width; x++){
				GameObject cube = (GameObject)Instantiate(debugNodePrefab,gridposToWorld(map[x,y].pos),Quaternion.identity);
				if(map[x,y].Closed){
					cube.renderer.material = debugMat1;
				}
		}
		
	}
	
	private void initMap(){
		map = new Node[width, height];
		mapclosed.Clear();
		
		for(int y = 0; y < height; y++)
			for(int x = 0; x < width; x++){
				Color col = pixelMap.GetPixel(x,y);
				//float w = 1 + 1-col.grayscale;
				float w = 1;
				bool c = col.g < 0.1;
				map[x,y] = new Node{pos = new Vector2(x,y),Closed = c,weigth = w, parent = null};
				if(c){
					mapclosed.Add(map[x,y]);
				}
		}
		Debug.Log("map initiated");
	}
	
	public List<Vector3> findpath(Vector3 start, Vector3 end){
		findPath2d(worldposToGridpos(start),worldposToGridpos(end));
		
		List<Vector3> vec3path = new List<Vector3>();
		vec3path.Clear();
		foreach(Node node in path){
			//Debug.Log("x = " + node.x + " y = " + node.y);
			vec3path.Add(gridposToWorld(node.pos));
			
		}
		vec3path.Reverse();
		vec3path.RemoveAt(0);
		path.Clear();
		return vec3path;
		
	}
	
	private void findPath2d(Vector2 start,Vector2 end){
		initMap();
		Debug.Log("Start x = " + (int)start.x + " y = " + (int)start.y);
		Debug.Log("End x = " + (int)end.x + " y = " + (int)end.y);
		Node startNode = map[(int)start.x,(int)start.y];
		Node endNode = map[(int)end.x,(int)end.y];
		
		if(endNode.Closed){
			Debug.Log("could not find path. end node closed");
			return;
		}
		
		SortedList<float, Node> openNodes = new SortedList<float, Node>();
		List<Node> closedNodes = new List<Node>(mapclosed);
		
		Node cur = startNode;
		cur.Closed = true;
		//closedNodes.Add(cur);
		

		
		int tries = 0;
		
		while(true){
			
			tries++;
			if(tries > 5000){
				Debug.Log("failed");
				return;
			}
			
			for(int i = Mathf.Max(cur.x-1, 0); i < Mathf.Min(cur.x+2, width); i++)
				for(int j = Mathf.Max(cur.y-1, 0); j < Mathf.Min(cur.y+2, height); j++){
					Node n = map[i,j];
					
					if(!n.Closed){
					//if(!closedNodes.Contains(n)){
						int sv = (i == cur.x || j == cur.y ? 10 : 14);
						if(!openNodes.ContainsValue(n)){
							n.G = cur.G + (int)(sv*n.weigth)+ (float)(Random.value*0.1); //lägg till *1.4 för diagonal
							n.H = 10 * (int)(Mathf.Abs(i - end.x) + Mathf.Abs(j - end.y));
							n.parent = cur;
							openNodes.Add(n.G + n.H,n);
						}else{
							if(n.G > cur.G + (int)(sv*n.weigth)+ 0.1){
								n.G = cur.G + (int)(sv*n.weigth) + (float)(Random.value*0.1);
								//n.parent = cur;
								openNodes.RemoveAt(openNodes.IndexOfValue(n));
								openNodes.Add(n.G + n.H,n);
							}
						}
					}
			}
			
			cur = openNodes.FirstOrDefault().Value;
			openNodes.RemoveAt(0);
			cur.Closed = true;
			//closedNodes.Add(cur);
			if(cur == null || cur == endNode)
				break;

		}
		
		Debug.Log("Path found! cells in open: " + openNodes.Count + " value on last node: " + cur.G + cur.H);
		
		while(cur != null){
			path.Add(cur);
			//Debug.Log("x = " + cur.x + " y = " + cur.y);
			Node curp = cur.parent;
			cur.parent = null;
			cur = curp;
		}
		Debug.Log("steps taken: " + path.Count);
		
		
		
	}
	
	public Vector2 worldposToGridpos(Vector3 worldposition){
		Vector3 offset = 10 * walkmesh.localScale / 2;
		
		Vector2 gridpos = new Vector2(Mathf.Floor(worldposition.x + offset.x),Mathf.Floor(worldposition.z + offset.z)) * 2;
		gridpos += new Vector2(walkmesh.position.x,walkmesh.position.y);
		//Debug.Log("WtG -> " + " x " + worldposition.x + " y " + worldposition.z + " x -> " + gridpos.x + " y " + gridpos.y);
		return gridpos;
	}
	
	public Vector3 gridposToWorld(Vector2 gridposition){
		Vector3 offset = 10 * walkmesh.localScale;
		float midpointoffset = 0.5f;

		Vector3 worldpos = new Vector3(gridposition.x - offset.x + midpointoffset ,0 ,gridposition.y - offset.z + midpointoffset) / 2;
		worldpos += walkmesh.position;
		//Debug.Log("GtW -> " + " x " + gridposition.x + " y " + gridposition.y + " x -> " + worldpos.x + " y " + worldpos.z);
		return worldpos;
	}
	
	
	
	public class Node{
		public bool Closed;
		public float H;
		public float G;
		public Node parent;
		public Vector2 pos;
		public float weigth;
		public int x{
			get{return (int)pos.x;}
		}
		public int y{
			get{return (int)pos.y;}
		}
		
	}
}


