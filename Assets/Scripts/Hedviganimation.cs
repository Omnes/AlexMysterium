using UnityEngine;
using System.Collections;

public class Hedviganimation : MonoBehaviour {

	public float Animations = 2; //Antal animationer på spriteSheeten
	public float Frames = 7;
	public float Frametime = 0.05f;
	public float alphadelta = 0.01f;
	Vector3 direction = new Vector3(1,0,0);
	Material playerMat;
	float xOffset = 0;
	float yOffset = 0;
	float currentFrame;
	float clock;
	Color color;
	float currentalpha;
	bool startfade = false;

	// Use this for initialization
	void Start () {

		playerMat = transform.renderer.material;
		playerMat.mainTextureScale = new Vector2(1/Frames,1/Animations); //Skalar av höjden(Y) till en frame
		currentFrame = 0; 
		clock = Time.time; 
		setAnimation(direction);
		color = playerMat.color;
		playerMat.color = new Color(color.r, color.g, color.b, currentalpha);
	}
	
	// Update is called once per frame
	void Update () {
	
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

		if(!startfade){
			currentalpha += alphadelta;
		}

		if(startfade){
			currentalpha -= alphadelta;
		}

		playerMat.color = new Color(color.r, color.g, color.b, currentalpha);
	}

	public void setAnimation(Vector2 direction){
		
		if(direction.y < 1 && direction.y > -1){
			if(direction.x > 0){
				yOffset = 1f/Animations*1;
			}
			if(direction.x < 0){
				yOffset = 0;
			}
		}else{
			if(direction.y < 0){
				yOffset = 1f/Animations*2;
			}
			if(direction.y > 0){
				
				yOffset = 1f/Animations*3;
			}
		}	
		playerMat.mainTextureOffset = new Vector2(xOffset,yOffset); //sätter nuvarande offsetY
	}

	public void startfadingaway(){

		startfade = true;
	}
}
