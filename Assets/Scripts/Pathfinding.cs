using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

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
	
	//private List<GameObject> debugcubes;
	
	void Start () {
		
		height = pixelMap.height;
		width = pixelMap.width;
		path = new List<Node>();
		mapclosed = new List<Node>();
		
		initMap();
		if(debug) createDebugNodes();
	}
	
	
	//debug funktion som skapar en kub på varje node position och byter färge på de som är closed
	public void createDebugNodes(){
		for(int y = 0; y < height; y++)
			for(int x = 0; x < width; x++){
				GameObject cube = Instantiate(debugNodePrefab,gridposToWorld(map[x,y].pos),Quaternion.identity) as GameObject;
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
		if(Input.GetKeyDown(KeyCode.O) && debug){
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
				float w = 1;
				bool c = col.g < 0.1;
				map[x,y] = new Node{pos = new Vector2(x,y),Closed = c,weigth = w,Used = false, parent = null};
				//if(c){
				//	mapclosed.Add(map[x,y]);
				//}
		}
		//Debug.Log("map initiated");
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
		
		List<Vector3> vec3path = new List<Vector3>();
		vec3path.Clear();
		foreach(Node node in path){
			//Debug.Log("x = " + node.x + " y = " + node.y);
			vec3path.Add(gridposToWorld(node.pos));
			
		}
		vec3path.Reverse();
		if(vec3path.Count > 0)
			vec3path.RemoveAt(0);
		path.Clear();
		return vec3path;
		
	}
	
	//A* implementationen
	private void findPath2d(Vector2 start,Vector2 end){
		start = new Vector2(Mathf.Clamp(start.x,0,width),Mathf.Clamp(start.y,0,height));
		end = new Vector2(Mathf.Clamp(end.x,0,width),Mathf.Clamp(end.y,0,height));
		cleanmap();
		//Debug.Log("Start x = " + (int)start.x + " y = " + (int)start.y);
		//Debug.Log("End x = " + (int)end.x + " y = " + (int)end.y);
		Node startNode = map[(int)start.x,(int)start.y];
		Node endNode = map[(int)end.x,(int)end.y];
		
		//bryt ur om slutnoden inte går att nå
		if(endNode.Closed){
			Debug.Log("could not find path. end node closed");
			return;
		}
		
		SortedList<float, Node> openNodes = new SortedList<float, Node>();
		//List<Node> closedNodes = new List<Node>(mapclosed);
		
		Node cur = startNode;
		cur.Used = true;
		//closedNodes.Add(cur);
		

		
		int tries = 0;
		
		while(true){
			
			//så den inte fastnar i ett försök att hitta till en path till en plats som inte går att nå 
			tries++;
			if(tries > 5000){
				Debug.Log("failed");
				return;
			}
			
			for(int i = Mathf.Max(cur.x-1, 0); i < Mathf.Min(cur.x+2, width); i++)
				for(int j = Mathf.Max(cur.y-1, 0); j < Mathf.Min(cur.y+2, height); j++){
					Node n = map[i,j];
					
					if(!n.Closed && !n.Used){
					//if(!closedNodes.Contains(n)){
						int sv = (i == cur.x || j == cur.y ? 10 : 14);
						if(!openNodes.ContainsValue(n)){
							n.G = cur.G + (int)(sv*n.weigth)+ (float)(Random.value*0.1); //lägg till *1.4 för diagonal
							n.H = 10 * (int)(Mathf.Abs(i - end.x) + Mathf.Abs(j - end.y));
							n.parent = cur;
							openNodes.Add(n.G + n.H,n);
						}else{
							if(n.G > cur.G + (int)(sv*n.weigth)+ 0.1){ //FIXA SÅ DEN BYTER PARENT NÄR DEN HITTAR TILL EN POSITION MED HÖGRE VÄRDE
								n.G = cur.G + (int)(sv*n.weigth) + (float)(Random.value*0.1);
								//n.parent = cur; //fick spelet att hänga sig när 2 nodes blev parents till varandra
								openNodes.RemoveAt(openNodes.IndexOfValue(n));
								openNodes.Add(n.G + n.H,n);
							}
						}
					}
			}
			
			cur = openNodes.FirstOrDefault().Value;
			openNodes.RemoveAt(0);
			cur.Used = true;
			//closedNodes.Add(cur);
			if(cur == null || cur == endNode)
				break;

		}
		
		//Debug.Log("Path found! cells in open: " + openNodes.Count + " value on last node: " + cur.G + cur.H);
		
		//går baklänges igenom alla parents
		while(cur != null){
			path.Add(cur);
			//Debug.Log("x = " + cur.x + " y = " + cur.y);
			Node curp = cur.parent;
			cur.parent = null;
			cur = curp;
		}
		//Debug.Log("steps taken: " + path.Count);
		
		
		
	}
	
	//tar en vector3 med en världs kordinat och gör om den till nodegrid kordinater
	public Vector2 worldposToGridpos(Vector3 worldposition){
		Vector3 offset = 10 * walkmesh.localScale / 2;
		Vector3 mapscale =  new Vector3(offset.x*4/(float)pixelMap.width,0,offset.z*4/(float)pixelMap.height);
		
		Vector2 gridpos = new Vector2(Mathf.Floor(worldposition.x + offset.x)*mapscale.x,Mathf.Floor(worldposition.z + offset.z)*mapscale.z) * 2;
		gridpos += new Vector2(walkmesh.position.x,walkmesh.position.y);
		//Debug.Log("WtG -> " + " x " + worldposition.x + " y " + worldposition.z + " x -> " + gridpos.x + " y " + gridpos.y);
		return gridpos;
	}
	
	//tar en vector2 med en nodegrid kordinat och gör om den till världs kordinater
	public Vector3 gridposToWorld(Vector2 gridposition){
		Vector3 offset = 10 * walkmesh.localScale;
		Vector3 mapscale =  new Vector3(offset.x/(float)pixelMap.width,0,offset.z/(float)pixelMap.height)*2;
		offset =  new Vector3(offset.x,0,offset.z);
		

		float midpointoffset = 0.5f;

		Vector3 worldpos = new Vector3((gridposition.x - offset.x + midpointoffset) ,0 ,(gridposition.y - offset.z + midpointoffset))/2 ;
		worldpos += walkmesh.position;
		worldpos = new Vector3(worldpos.x*mapscale.x ,0,worldpos.z*mapscale.z);
		//Debug.Log("GtW -> " + " x " + gridposition.x + " y " + gridposition.y + " x -> " + worldpos.x + " y " + worldpos.z);
		return worldpos;
	}
	
	
	//Node klassen
	public class Node{
		public bool Closed;
		public bool Used;
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
		public void clean(){
			this.parent = null;
			this.Used = false;
			this.H = 0;
			this.G = 0;
		}
		
	}
}


