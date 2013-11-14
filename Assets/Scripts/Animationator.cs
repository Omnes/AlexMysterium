using UnityEngine;
using System.Collections;

public class Animationator : MonoBehaviour {
	
	public float Animations; //Antal animationer på spriteSheeten
	public float Frames;
	public float Frametime;
	public float distanceDelta; //Hur mycket storleken på spriten ska ändras beroende av avståndet
	public float groundDepth;
	Vector3 orgSpriteSize;
	float xOffset;
	float yOffset;
	float currentFrame;
	float clock;
	bool walk;
	private Material playerMat;
	
	
	void Start () {
		
		orgSpriteSize = transform.GetChild(0).localScale;//Storleken på objektet
		playerMat = transform.GetChild(0).renderer.material;
		playerMat.mainTextureScale = new Vector2(1/Frames,1/Animations); //Skalar av höjden(Y) till en frame
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
			
				playerMat.mainTextureOffset = new Vector2(xOffset,yOffset); //Sätt nästa frame
			
				clock = Time.time; //Starta om klockan
			}
		}
		
		float spriteSize = distanceDelta * transform.GetChild(0).position.z;
		
		transform.GetChild(0).localScale = new Vector3(orgSpriteSize.x - orgSpriteSize.x*spriteSize, orgSpriteSize.y, orgSpriteSize.z - orgSpriteSize.z*spriteSize);
		
		//transform.localScale = spriteSize/(transform.position.z/distanceDelta); //Bildens storlek anpassas efter djupet(Z)
	}
	
	
	public void setWalkAnimation(Vector2 direction){
		
		//float degrees = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg; //omvandlar riktningskoordinaterna till antalet grader riktningen går i
		//Debug.Log (degrees);
		
		//yOffset = (degrees/(360/Animations))/Animations; 
		
		
		//världens fulaste jävla cplösning nedanför, ser dessutom skitfult ut
		if(direction.y < 0.5 && direction.y > -0.5){
			if(direction.x > 0){
				yOffset = 0.25f;
			}
			if(direction.x < 0){
				yOffset = 0.75f;
			}
		}else{
			if(direction.y < 0){
				
				yOffset = 0.5f;
			}
			if(direction.y > 0){
				
				yOffset = 0;
			}
		}		

		
		playerMat.mainTextureOffset = new Vector2(xOffset,yOffset); //sätter nuvarande offsetY
		
		walk = true;
	}
	
	public void setStandAnimation(){
	
		playerMat.mainTextureOffset = new Vector2(0,yOffset);	
		
		walk = false;
	}
}
