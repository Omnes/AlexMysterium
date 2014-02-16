using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;

//koden är skit.

public class Pathfinding : MonoBehaviour {
	
	
	public Texture2D pixelMap;
	public Transform walkmesh;
	public int diagonalcost = 14;
	public int straightcost = 10;
	
	public Material debugMat1;
	public GameObject debugNodePrefab;
	public bool debugMode = false;
	
	public Node[,] map;
	public List<Node> path;
	public List<Node> mapclosed;
	
	
	private int height;
	private int width;
	
	//private List<GameObject> debugcubes;
	
	void Start () {
		
		height = pixelMap.height;
		width = pixelMap.width;
		path = new List<Node>();
		mapclosed = new List<Node>();
		
		initMap();
		if(debugMode) createDebugNodes();
	}
	
	struct Vector2Int{
		int _x;
		int _y;
		public Vector2Int(int x,int y){
			this._x = x;
			this._y = y;
		}
		public Vector2Int(float x,float y){
			this._x = Mathf.RoundToInt(x);
			this._y = Mathf.RoundToInt(y);
		}
		public int x{
			get{return _x;}
		}
		public int y{
			get{return _y;}
		}
	}
	
	
	//debug funktion som skapar en kub på varje node position och byter färge på de som är closed
	public void createDebugNodes(){
		GameObject empty = new GameObject();
		GameObject cubeparent = Instantiate(empty,Vector3.zero,Quaternion.identity) as GameObject;
		cubeparent.name = "debuggrid";
		for(int y = 0; y < height; y++)
		for(int x = 0; x < width; x++){
			GameObject cube = Instantiate(debugNodePrefab,gridposToWorld(map[x,y].pos),Quaternion.identity) as GameObject; 
			cube.transform.parent = cubeparent.transform;
			cube.tag = "debugCubePathfinding";
			pathfindingDebugInfoholder pdi = cube.AddComponent("pathfindingDebugInfoholder") as pathfindingDebugInfoholder;
			pdi.place = new Vector2(x,y);
			pdi.closed = map[x,y].Closed;
			pdi.updColor();
			//debugcubes.Add(cube);
			//if(map[x,y].Closed || map[x,y].Used){
			//	cube.renderer.material = debugMat1;
			//}
		}
		
	}
	
	void Update(){
		//press o to save
		if(Input.GetKeyDown(KeyCode.O) && debugMode){
			Texture2D tex = new Texture2D(width,height);
			GameObject[] debugcubes = GameObject.FindGameObjectsWithTag("debugCubePathfinding");
			foreach (GameObject cube in debugcubes){
				pathfindingDebugInfoholder pdi = cube.GetComponent<pathfindingDebugInfoholder>();
				float f = System.Convert.ToInt32(!pdi.closed);
				tex.SetPixel((int)pdi.place.x,(int)pdi.place.y,new Color(0f,f,0f));
			}
			byte[] bytes = tex.EncodeToPNG();
			File.WriteAllBytes(Application.dataPath + "/../pathfindingdebugimage.png", bytes);
		}
	}
	
	//initerar alla nodes och läser av pixelmappen
	private void initMap(){
		map = new Node[width, height];
		mapclosed.Clear();
		
		for(int y = 0; y < height; y++)
		for(int x = 0; x < width; x++){
			Color col = pixelMap.GetPixel(x,y);
			//float w = 1 + 1-col.grayscale;
			// w sätter 'weight' för varje node / kan användas för att få den att helst undvika vissa nodes om den kan
			int w = 1;
			bool c = col.g < 0.1;
			map[x,y] = new Node{posx = x, posy = y,Closed = c,weigth = w,Used = false, parent = null};
			//if(c){
			//	mapclosed.Add(map[x,y]);
			//}
		}
		////Debug.Log("map initiated");
	}
	
	void cleanmap(){
		for(int y = 0; y < height; y++)
		for(int x = 0; x < width; x++){
			map[x,y].clean();
		}
	} 
	bool compareVec3(Vector3 v1,Vector3 v2){
		return Vector3.Distance(v1,v2) < 0.6;
		
	}
	
	//hittar en path och gör om den till worldkordinater
	public List<Vector3> findpath(Vector3 start, Vector3 end){
		//om vi redan är där avbryt tidigare
		if(Vector2.Distance(worldposToGridpos(start),worldposToGridpos(end)) < 0.2){
			return new List<Vector3>();
		}
		
		findPath2d(worldposToGridpos(start),worldposToGridpos(end));
		////Debug.Log ("end in: " + end);
		////Debug.Log ("end grid in: " + worldposToGridpos(end));
		
		List<Vector3> vec3path = new List<Vector3>();
		vec3path.Clear();
		foreach(Node node in path){
			////Debug.Log("x = " + node.x + " y = " + node.y);
			vec3path.Add(gridposToWorld(node.pos));
			
		}
		////Debug.Log ("end grid out: " + path[0].pos);
		////Debug.Log ("end out: " + vec3path[0]);
		vec3path.Reverse();
		if(vec3path.Count > 0)
			vec3path.RemoveAt(0);
		path.Clear();
		return vec3path;
		
	}
	
	//A* implementationen
	private void findPath2d(Vector2 startv2,Vector2 endv2){
		Vector2Int start = new Vector2Int(Mathf.Clamp(startv2.x,0,width-1),Mathf.Clamp(startv2.y,0,height-1));
		Vector2Int end = new Vector2Int(Mathf.Clamp(endv2.x,0,width-1),Mathf.Clamp(endv2.y,0,height-1));
		////Debug.Log("input end " + endv2.x + " " + endv2.y);
		cleanmap();
		////Debug.Log("Start x = " + (int)start.x + " y = " + (int)start.y);
		////Debug.Log("End x = " + (int)end.x + " y = " + (int)end.y);
		Node startNode = map[start.x,start.y];
		Node endNode = map[end.x,end.y];
		
		//bryt ur om slutnoden inte går att nå
		if(endNode.Closed){
			////Debug.Log("could not find path. end node closed");
			return;
		}
		
		PriorityQueue<wnPair> openNodes = new PriorityQueue<wnPair>();
		//openNodes.Clear();
		//List<Node> closedNodes = new List<Node>(mapclosed);
		openNodes.Enqueue(new wnPair(startNode,0));
		Node cur = startNode;
		cur.Used = true;
		//closedNodes.Add(cur);
		
		
		
		//int tries = 0;
		
		while(openNodes.Count() > 0 && cur != endNode && cur != null){
			
			//så den inte fastnar i ett försök att hitta till en path till en plats som inte går att nå 
			//tries++;
			//if(tries > 5000){
			//	//Debug.Log("failed");
			//	return;
			//}
			//if(cur == null || cur == endNode)
			//	break;
			
			cur = openNodes.Dequeue().node;
			cur.Used = true;
			
			for(int i = Mathf.Max(cur.x-1, 0); i < Mathf.Min(cur.x+2, width); i++){
				for(int j = Mathf.Max(cur.y-1, 0); j < Mathf.Min(cur.y+2, height); j++){
					Node node = map[i,j];
					
					if(!node.Closed && !node.Used){
						//if(!closedNodes.Contains(n)){
						int sv = ((i == cur.x || j == cur.y) ? straightcost : diagonalcost); // kolla om det är diagonalt eller rakt till noden
						int weigth = cur.G + sv * node.weigth;
						
						if(!node.open){
							node.open = true;
							node.G = weigth;
							node.H = straightcost * (Mathf.Abs(i - end.x) + Mathf.Abs(j - end.y));
							node.parent = cur;
							int w = node.G + node.H;
							
							openNodes.Enqueue(new wnPair(node,w));
						}
					}
				}
			}
		}
		
		
		//går baklänges igenom alla parents
		while(cur != null){
			path.Add(cur);
			////Debug.Log("x = " + cur.x + " y = " + cur.y);
			Node curp = cur.parent;
			cur.parent = null;
			cur = curp;
		}
		////Debug.Log("steps taken: " + path.Count);
		
		
		
	}
	
	
	public Vector2 worldposToGridpos(Vector3 worldposition){
		Vector3 floorSize = 10*walkmesh.localScale;
		floorSize.y = 0;
		Vector3 localOrginOffset = new Vector3(floorSize.x/2f,0,floorSize.z/2f);
		localOrginOffset.y = 0;
		Vector3 floorOrgin = walkmesh.position - localOrginOffset;
		
		Vector3 nodeArea = new Vector3(floorSize.x/width,0,floorSize.z/height);
		
		//Vector3 floorPos = worldposition + localOrginOffset;
		Vector3 floorPos = worldposition + localOrginOffset + walkmesh.position;
		Vector2 gridpos = new Vector2(Mathf.Floor(floorPos.x/nodeArea.x+0.5f),Mathf.Floor(floorPos.z/nodeArea.z+0.5f));
		////Debug.Log("in: wp " + worldposition + " gp " + gridpos);
		return gridpos;
	}
	
	
	public Vector3 gridposToWorld(Vector2 gridposition){
		Vector3 floorSize = 10*walkmesh.localScale;
		floorSize.y = 0;
		Vector3 localOrginOffset = floorSize/2;
		localOrginOffset.y = 0;
		Vector3 floorOrgin = walkmesh.position - localOrginOffset;
		
		Vector3 nodeArea = new Vector3(floorSize.x/width,0,floorSize.z/height);
		
		Vector3 worldPos = new Vector3(nodeArea.x*gridposition.x,0,nodeArea.z*gridposition.y);
		worldPos += floorOrgin;
		////Debug.Log("out: wp " + worldPos + " gp " + gridposition);
		return worldPos;
	}
	
	
	//Node klassen
	public class Node{
		public bool Closed;
		public bool Used;
		public bool open = false;
		public int H;
		public int G;
		public Node parent;
		public int posx;
		public int posy;
		public int weigth;
		public int x{
			get{return posx;}
		}
		public int y{
			get{return posy;}
		}
		public Vector2 pos{
			get{return new Vector2(posx,posy);}
		}
		
		public void clean(){
			this.parent = null;
			this.Used = false;
			this.open = false;
			this.H = 0;
			this.G = 0;
		}
		
	}
	
	public class wnPair : IComparable<wnPair>{
		public Node node;
		public int weight;
		
		public wnPair(Node n,int w){
			this.node = n;
			this.weight = w;
		}
		
		public int CompareTo(wnPair other)
		{
			if (this.weight < other.weight) return -1;
			else if (this.weight > other.weight) return 1;
			else return 0;
		}
	}
	
	public class PriorityQueue<T> where T : IComparable<T>
	{
		private List<T> data;
		
		public PriorityQueue()
		{
			this.data = new List<T>();
		}
		
		public void Enqueue(T item)
		{
			data.Add(item);
			int ci = data.Count - 1; // child index; start at end
			while (ci > 0)
			{
				int pi = (ci - 1) / 2; // parent index
				if (data[ci].CompareTo(data[pi]) >= 0) break; // child item is larger than (or equal) parent so we're done
				T tmp = data[ci]; data[ci] = data[pi]; data[pi] = tmp;
				ci = pi;
			}
		}
		
		public T Dequeue()
		{
			// assumes pq is not empty; up to calling code
			int li = data.Count - 1; // last index (before removal)
			T frontItem = data[0];   // fetch the front
			data[0] = data[li];
			data.RemoveAt(li);
			
			--li; // last index (after removal)
			int pi = 0; // parent index. start at front of pq
			while (true)
			{
				int ci = pi * 2 + 1; // left child index of parent
				if (ci > li) break;  // no children so done
				int rc = ci + 1;     // right child
				if (rc <= li && data[rc].CompareTo(data[ci]) < 0) // if there is a rc (ci + 1), and it is smaller than left child, use the rc instead
					ci = rc;
				if (data[pi].CompareTo(data[ci]) <= 0) break; // parent is smaller than (or equal to) smallest child so done
				T tmp = data[pi]; data[pi] = data[ci]; data[ci] = tmp; // swap parent and child
				pi = ci;
			}
			return frontItem;
		}
		
		public T Peek()
		{
			T frontItem = data[0];
			return frontItem;
		}
		
		public int Count()
		{
			return data.Count;
		}
		
		public override string ToString()
		{
			string s = "";
			for (int i = 0; i < data.Count; ++i)
				s += data[i].ToString() + " ";
			s += "count = " + data.Count;
			return s;
		}
		
		public bool IsConsistent()
		{
			// is the heap property true for all data?
			if (data.Count == 0) return true;
			int li = data.Count - 1; // last index
			for (int pi = 0; pi < data.Count; ++pi) // each parent index
			{
				int lci = 2 * pi + 1; // left child index
				int rci = 2 * pi + 2; // right child index
				
				if (lci <= li && data[pi].CompareTo(data[lci]) > 0) return false; // if lc exists and it's greater than parent then bad.
				if (rci <= li && data[pi].CompareTo(data[rci]) > 0) return false; // check the right child too.
			}
			return true; // passed all checks
		} // IsConsistent
	} // PriorityQueue
	
} 







/*
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

//koden är skit.

public class Pathfinding : MonoBehaviour {
	
	
	public Texture2D pixelMap;
	public Transform walkmesh;
	public int diagonalcost = 140000;
	public int straightcost = 100000;
	
	public Material debugMat1;
	public GameObject debugNodePrefab;
	public bool debugMode = false;
	
	public Node[,] map;
	public List<Node> path;
	public List<Node> mapclosed;

	
	private int height;
	private int width;
	
	//private List<GameObject> debugcubes;
	
	void Start () {
		
		height = pixelMap.height;
		width = pixelMap.width;
		path = new List<Node>();
		mapclosed = new List<Node>();
		
		initMap();
		if(debugMode) createDebugNodes();
	}

	struct Vector2Int{
		int _x;
		int _y;
		public Vector2Int(int x,int y){
			this._x = x;
			this._y = y;
		}
		public Vector2Int(float x,float y){
			this._x = Mathf.RoundToInt(x);
			this._y = Mathf.RoundToInt(y);
		}
		public int x{
			get{return _x;}
		}
		public int y{
			get{return _y;}
		}
	}
	
	
	//debug funktion som skapar en kub på varje node position och byter färge på de som är closed
	public void createDebugNodes(){
		GameObject empty = new GameObject();
		GameObject cubeparent = Instantiate(empty,Vector3.zero,Quaternion.identity) as GameObject;
		cubeparent.name = "debuggrid";
		for(int y = 0; y < height; y++)
			for(int x = 0; x < width; x++){
			 	GameObject cube = Instantiate(debugNodePrefab,gridposToWorld(map[x,y].pos),Quaternion.identity) as GameObject; 
				cube.transform.parent = cubeparent.transform;
			 	cube.tag = "debugCubePathfinding";
				pathfindingDebugInfoholder pdi = cube.AddComponent("pathfindingDebugInfoholder") as pathfindingDebugInfoholder;
				pdi.place = new Vector2(x,y);
				pdi.closed = map[x,y].Closed;
				pdi.updColor();
			    //debugcubes.Add(cube);
				//if(map[x,y].Closed || map[x,y].Used){
				//	cube.renderer.material = debugMat1;
				//}
		}
		
	}
	
	void Update(){
		//press o to save
		if(Input.GetKeyDown(KeyCode.O) && debugMode){
			Texture2D tex = new Texture2D(width,height);
			GameObject[] debugcubes = GameObject.FindGameObjectsWithTag("debugCubePathfinding");
			foreach (GameObject cube in debugcubes){
				pathfindingDebugInfoholder pdi = cube.GetComponent<pathfindingDebugInfoholder>();
				float f = System.Convert.ToInt32(!pdi.closed);
				tex.SetPixel((int)pdi.place.x,(int)pdi.place.y,new Color(0f,f,0f));
			}
			byte[] bytes = tex.EncodeToPNG();
			File.WriteAllBytes(Application.dataPath + "/../pathfindingdebugimage.png", bytes);
		}
	}
	
	//initerar alla nodes och läser av pixelmappen
	private void initMap(){
		map = new Node[width, height];
		mapclosed.Clear();
		
		for(int y = 0; y < height; y++)
			for(int x = 0; x < width; x++){
				Color col = pixelMap.GetPixel(x,y);
				//float w = 1 + 1-col.grayscale;
				// w sätter 'weight' för varje node / kan användas för att få den att helst undvika vissa nodes om den kan
				int w = 1;
				bool c = col.g < 0.1;
				map[x,y] = new Node{posx = x, posy = y,Closed = c,weigth = w,Used = false, parent = null};
				//if(c){
				//	mapclosed.Add(map[x,y]);
				//}
		}
		////Debug.Log("map initiated");
	}
	
	void cleanmap(){
		for(int y = 0; y < height; y++)
			for(int x = 0; x < width; x++){
			map[x,y].clean();
		}
	} 
	bool compareVec3(Vector3 v1,Vector3 v2){
		return Vector3.Distance(v1,v2) < 0.6;
		
	}

	//hittar en path och gör om den till worldkordinater
	public List<Vector3> findpath(Vector3 start, Vector3 end){
		//om vi redan är där avbryt tidigare
		if(Vector2.Distance(worldposToGridpos(start),worldposToGridpos(end)) < 0.2){
			return new List<Vector3>();
		}
		
		findPath2d(worldposToGridpos(start),worldposToGridpos(end));
		////Debug.Log ("end in: " + end);
		////Debug.Log ("end grid in: " + worldposToGridpos(end));
		
		List<Vector3> vec3path = new List<Vector3>();
		vec3path.Clear();
		foreach(Node node in path){
			////Debug.Log("x = " + node.x + " y = " + node.y);
			vec3path.Add(gridposToWorld(node.pos));
			
		}
		////Debug.Log ("end grid out: " + path[0].pos);
		////Debug.Log ("end out: " + vec3path[0]);
		vec3path.Reverse();
		if(vec3path.Count > 0)
			vec3path.RemoveAt(0);
		path.Clear();
		return vec3path;
		
	}
	
	//A* implementationen
	private void findPath2d(Vector2 startv2,Vector2 endv2){
		Vector2Int start = new Vector2Int(Mathf.Clamp(startv2.x,0,width-1),Mathf.Clamp(startv2.y,0,height-1));
		Vector2Int end = new Vector2Int(Mathf.Clamp(endv2.x,0,width-1),Mathf.Clamp(endv2.y,0,height-1));
		////Debug.Log("input end " + endv2.x + " " + endv2.y);
		cleanmap();
		////Debug.Log("Start x = " + (int)start.x + " y = " + (int)start.y);
		////Debug.Log("End x = " + (int)end.x + " y = " + (int)end.y);
		Node startNode = map[start.x,start.y];
		Node endNode = map[end.x,end.y];
		
		//bryt ur om slutnoden inte går att nå
		if(endNode.Closed){
			////Debug.Log("could not find path. end node closed");
			return;
		}
		
		SortedList<int, Node> openNodes = new SortedList<int, Node>();
		openNodes.Clear();
		//List<Node> closedNodes = new List<Node>(mapclosed);
		openNodes.Add(0,startNode);
		Node cur = startNode;
		cur.Used = true;
		//closedNodes.Add(cur);
		

		
		//int tries = 0;
		
		while(openNodes.Count > 0 && cur != endNode && cur != null){
			
			//så den inte fastnar i ett försök att hitta till en path till en plats som inte går att nå 
			//tries++;
			//if(tries > 5000){
			//	//Debug.Log("failed");
			//	return;
			//}
			//if(cur == null || cur == endNode)
			//	break;
			
			cur = openNodes.FirstOrDefault().Value;
			openNodes.RemoveAt(0);
			cur.Used = true;
			
			for(int i = Mathf.Max(cur.x-1, 0); i < Mathf.Min(cur.x+2, width); i++){
				for(int j = Mathf.Max(cur.y-1, 0); j < Mathf.Min(cur.y+2, height); j++){
					Node node = map[i,j];
						
					if(!node.Closed && !node.Used){
						//if(!closedNodes.Contains(n)){
						int sv = ((i == cur.x || j == cur.y) ? straightcost : diagonalcost); // kolla om det är diagonalt eller rakt till noden
						int weigth = cur.G + sv * node.weigth + (int)(Random.value * 10);
						
						if(!openNodes.ContainsValue(node)){
							node.G = weigth;
							node.H = straightcost * (Mathf.Abs(i - end.x) + Mathf.Abs(j - end.y));
							node.parent = cur;
							int w = node.G + node.H;
							while(openNodes.ContainsKey(w)){
								w += 1;
							}
							openNodes.Add(w,node);
						}else{
							if(node.G > weigth){ //FIXA SÅ DEN BYTER PARENT NÄR DEN HITTAR TILL EN POSITION MED HÖGRE VÄRDE
								node.G = weigth;
								node.parent = cur; //fick spelet att hänga sig när 2 nodes blev parents till varandra
								openNodes.RemoveAt(openNodes.IndexOfValue(node));
								int w = node.G + node.H;
								while(openNodes.ContainsKey(w)){
									w += 1;
								}
								openNodes.Add(w,node);
							}
						}
					}
				}
			}
			
			//cur = openNodes.FirstOrDefault().Value;
			//openNodes.RemoveAt(0);
			//cur.Used = true;
			//closedNodes.Add(cur);
			

		}
		
		////Debug.Log("Path found! cells in open: " + openNodes.Count + " value on last node: " + cur.G + cur.H);
		////Debug.Log("output end " + cur.x + " " + cur.y);
		//går baklänges igenom alla parents
		while(cur != null){
			path.Add(cur);
			////Debug.Log("x = " + cur.x + " y = " + cur.y);
			Node curp = cur.parent;
			cur.parent = null;
			cur = curp;
		}
		////Debug.Log("steps taken: " + path.Count);
		
		
		
	}
	/*
	//tar en vector3 med en världs kordinat och gör om den till nodegrid kordinater
	public Vector2 worldposToGridpos(Vector3 worldposition){
		Vector3 offset = 10 * walkmesh.localScale / 2;
		Vector3 mapscale =  new Vector3(offset.x*4/(float)pixelMap.width,0,offset.z*4/(float)pixelMap.height);
		
		Vector2 gridpos = new Vector2(Mathf.Floor(worldposition.x + offset.x)*mapscale.x,Mathf.Floor(worldposition.z + offset.z)*mapscale.z) * 2;
		gridpos += new Vector2(walkmesh.position.x,walkmesh.position.y);
		////Debug.Log("WtG -> " + " x " + worldposition.x + " y " + worldposition.z + " x -> " + gridpos.x + " y " + gridpos.y);
		return gridpos;
	}*/
	/*
	//tar en vector2 med en nodegrid kordinat och gör om den till världs kordinater
	public Vector3 gridposToWorld(Vector2 gridposition){
		Vector3 offset = 10 * walkmesh.localScale;
		Vector3 mapscale =  new Vector3(offset.x/(float)pixelMap.width,0,offset.z/(float)pixelMap.height)*2;
		offset =  new Vector3(offset.x,0,offset.z);
		
		float midpointoffset = 0.5f;

		Vector3 worldpos = new Vector3((gridposition.x - offset.x + midpointoffset) ,0 ,(gridposition.y - offset.z + midpointoffset))/2 ;
		worldpos += walkmesh.position;
		worldpos = new Vector3(worldpos.x*mapscale.x ,0,worldpos.z*mapscale.z);
		////Debug.Log("GtW -> " + " x " + gridposition.x + " y " + gridposition.y + " x -> " + worldpos.x + " y " + worldpos.z);
		return worldpos;
	}
	*/
	/*
	public Vector2 worldposToGridpos1(Vector3 worldposition){
		Vector3 offset = 10 * walkmesh.localScale / 2;
		Vector3 mapscale =  new Vector3(offset.x*4/width,0,offset.z*4/height);
		
		Vector2 gridpos = new Vector2(Mathf.Floor(worldposition.x + offset.x)*mapscale.x,Mathf.Floor(worldposition.z + offset.z)*mapscale.z) * 2;
		gridpos += new Vector2(walkmesh.position.x,walkmesh.position.y);
		//Debug.Log("wp " + worldposition + " gp " + gridpos);
		return gridpos;
	}*//*
	
	public Vector2 worldposToGridpos(Vector3 worldposition){
		Vector3 floorSize = 10*walkmesh.localScale;
		floorSize.y = 0;
		Vector3 localOrginOffset = new Vector3(floorSize.x/2f,0,floorSize.z/2f);
		localOrginOffset.y = 0;
		Vector3 floorOrgin = walkmesh.position - localOrginOffset;
		
		Vector3 nodeArea = new Vector3(floorSize.x/width,0,floorSize.z/height);
		
		//Vector3 floorPos = worldposition + localOrginOffset;
		Vector3 floorPos = worldposition + localOrginOffset + walkmesh.position;
		Vector2 gridpos = new Vector2(Mathf.Floor(floorPos.x/nodeArea.x+0.5f),Mathf.Floor(floorPos.z/nodeArea.z+0.5f));
		////Debug.Log("in: wp " + worldposition + " gp " + gridpos);
		return gridpos;
	}
	
	
	public Vector3 gridposToWorld(Vector2 gridposition){
		Vector3 floorSize = 10*walkmesh.localScale;
		floorSize.y = 0;
		Vector3 localOrginOffset = floorSize/2;
		localOrginOffset.y = 0;
		Vector3 floorOrgin = walkmesh.position - localOrginOffset;
		
		Vector3 nodeArea = new Vector3(floorSize.x/width,0,floorSize.z/height);
		
		Vector3 worldPos = new Vector3(nodeArea.x*gridposition.x,0,nodeArea.z*gridposition.y);
		worldPos += floorOrgin;
		////Debug.Log("out: wp " + worldPos + " gp " + gridposition);
		return worldPos;
	}
	
	
	//Node klassen
	public class Node{
		public bool Closed;
		public bool Used;
		public int H;
		public int G;
		public Node parent;
		public int posx;
		public int posy;
		public int weigth;
		public int x{
			get{return posx;}
		}
		public int y{
			get{return posy;}
		}
		public Vector2 pos{
			get{return new Vector2(posx,posy);}
		}

		public void clean(){
			this.parent = null;
			this.Used = false;
			this.H = 0;
			this.G = 0;
		}
		
	}
}


*/