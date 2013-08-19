using UnityEngine;
using System.Collections;

public class Animation : MonoBehaviour {
	
	public float Animations; //Antal animationer på spriteSheeten
	public float Frames;
	public float Frametime;
	public float distanceDelta; //Hur mycket storleken på spriten ska ändras beroende av avståndet
	Vector3 spriteSize;
	float xOffset;
	float yOffset;
	float currentFrame;
	float clock;
	bool walk;
	
	
	void Start () {
		
		spriteSize = transform.localScale;//Storleken på objektet
		renderer.material.mainTextureScale = new Vector2(1/Frames,1/Animations); //Skalar av höjden(Y) till en frame
		currentFrame = 0; 
		clock = Time.time; //Startar klockan
		xOffset = 0;
		yOffset = 0;
		walk = false;
	}
	
	
	void Update () {
		
		if(walk){
			
			if(Time.time - clock > Frametime){ //När frametimen passerat
			
				if(currentFrame < Frames - 1){
				
					currentFrame+=1;
				}
			
				else{
			
					currentFrame=0; 	
				}
			
				xOffset = currentFrame/Frames;
			
				renderer.material.mainTextureOffset = new Vector2(xOffset,yOffset); //Sätt nästa frame
			
				clock = Time.time; //Starta om klockan
			}
		}
		
		transform.localScale = spriteSize/(transform.position.z/distanceDelta); //Bildens storlek anpassas efter djupet(Z)
	}
	
	
	public void setWalkAnimation(Vector2 direction){
		
		float degrees = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg; //omvandlar riktningskoordinaterna till antalet grader riktningen går i
		
		yOffset = (degrees/(360/Animations))/Animations; 
		
		renderer.material.mainTextureOffset = new Vector2(xOffset,yOffset); //sätter nuvarande offsetY
		
		walk = true;
	}
	
	public void setStandAnimation(){
	
		renderer.material.mainTextureOffset = new Vector2(0,yOffset);	
		
		walk = false;
	}
}
