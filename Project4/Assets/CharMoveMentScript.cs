using UnityEngine;
using System.Collections;

public class CharMovementScript : MonoBehaviour {
	
	float lerpAmount = 0f;
	//didn't want to make an enum because who needs it?
	//position for chars: 0 is "back", 1 is "middle", 2 is "front"
	public int charPos;
	
	// Use this for initialization
	void Start () {	
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;	
		//movement
		if (Input.GetKeyDown("left")){
			pos.x +=10;
            transform.position  = Vector3.Lerp(transform.position, pos, lerpAmount); 
		}           			
		if (Input.GetKeyDown("up")){
			pos.z -=10;
            transform.position  = Vector3.Lerp(transform.position, pos, lerpAmount); 
		}
		if (Input.GetKeyDown("down")){
			pos.z +=10;
            transform.position  = Vector3.Lerp(transform.position, pos, lerpAmount); 
		}           	
		if (Input.GetKeyDown ("right")){
			pos.x -=10;
            transform.position  = Vector3.Lerp(transform.position, pos, lerpAmount); 
		}
		
		//character swap
		/*if(Input.GetKeyDown("q")){
			int partner = findQPartner();
			//find all players
			GameObject.FindGameObjectsWithTag("Player");
			//find one with correct CharPos (int partner has the value you're looking for)
			//get their position
			//your position = their position
		switch(charPos){
		case 0:			
			break;
		case 1:
			break;
		case 2:
			break;
		}
		}*/
		
		lerpAmount += Time.deltaTime;
	}
	
/*	int findQPartner(){
		switch(charPos){
		case 0:
			return 1;
			break;
		case 1:
			return 0;
			break;
		case 2:
			return 2;
			break;
		}
		print("Error in QPartner");
		return -1;
	}*/
	
	void charMoveLeft(){
		switch(charPos){
		case 0:			
			break;
		case 1:
			break;
		case 2:
			break;
		}
		
	
	}
	
}
