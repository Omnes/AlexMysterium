using UnityEngine;
using System.Collections;

public class Animationator : MonoBehaviour {
	
	public float Animations = 5; //Antal animationer på spriteSheeten
	public float Frames =14;
	public float FrametimeWalk = 0.05f;
	public float FrametimeStand = 0.1f;
	public float distanceDelta; //Hur mycket storleken på spriten ska ändras beroende av avståndet
	public float groundDepth;
	public float Size;
	Vector3 orgSpriteSize;
	float xOffset;
	float yOffset;
	float currentFrame;
	float clock;
	float Frametime;
	public bool walk;
	private Material playerMat;
	public float downOffset = 0.35f; 
	
	
	/*void Start () {
		

		
	}*/

	public void MastermindRunsThisStartFunction(){
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
		
		//if(walk){
			
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
	//	}
		
		float spriteSize = distanceDelta * transform.GetChild(0).position.z;

		transform.GetChild(0).localScale = new Vector3((2*(orgSpriteSize)*Size).x - (2*(orgSpriteSize)*Size).x*spriteSize, (2*(orgSpriteSize)*Size).y, (2*(orgSpriteSize)*Size).z - (2*(orgSpriteSize)*Size).z*spriteSize);
		transform.GetChild(0).localPosition = new Vector3(0, transform.GetChild(0).localScale.z*10/2 - downOffset, 0);
		
		//transform.localScale = spriteSize/(transform.position.z/distanceDelta); //Bildens storlek anpassas efter djupet(Z)
	}
	
	
	public void setWalkAnimation(Vector2 direction){
		
		//float degrees = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg; //omvandlar riktningskoordinaterna till antalet grader riktningen går i
		//Debug.Log (degrees);
		
		//yOffset = (degrees/(360/Animations))/Animations; 
		
		
		//världens fulaste jävla cplösning nedanför, ser dessutom skitfult ut

		Frametime = FrametimeWalk;

		if(direction.y < 1 && direction.y > -1){
			if(direction.x > 0){
				yOffset = 0f;
			}
			if(direction.x < 0){
				yOffset = 1f/Animations;
			}
		}else{
			if(direction.y < 0){
				
				yOffset = 1f/Animations*3;
			}
			if(direction.y > 0){
				
				yOffset = 1f/Animations*2;
			}
		}		

		
		playerMat.mainTextureOffset = new Vector2(xOffset,yOffset); //sätter nuvarande offsetY
		
		walk = true;
	}
	
	public void setStandAnimation(){

		//playerMat.mainTextureOffset = new Vector2(0,yOffset);	

		Frametime = FrametimeStand;
	
		yOffset = 0.8f;	
		
		walk = false;
	}
}
