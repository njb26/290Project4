	/*
	 *Code by Niklas Bauer, njb26 
	 * */
using UnityEngine;
using System.Collections;

public class CharMovementScript : MonoBehaviour {
	
	//float lerpAmount = 0f;
	//didn't want to make an enum because who needs it?
	//position for chars: 0 is "back", 1 is "middle", 2 is "front"
	
	
	// Use this for initialization
	void Start () {	
	
	}
	
	// Update is called once per frame
	void Update () {
		
		GameObject[] allies;
		allies = GameObject.FindGameObjectsWithTag("Player");
		//get object for each ally
		GameObject back = null; GameObject middle = null; GameObject front = null;
		foreach(GameObject obj in allies){
				if(obj.GetComponent<CharBehaviorScript>()){
					if(obj.GetComponent<CharBehaviorScript>().charPos == 0)
						back = obj;
					if(obj.GetComponent<CharBehaviorScript>().charPos == 1)
						middle = obj;
					if(obj.GetComponent<CharBehaviorScript>().charPos == 2)
						front = obj;
				}
		}
		Vector3 frontPos = front.transform.position;
		Vector3 newFrontPos = frontPos;
		Vector3 middlePos = middle.transform.position;
		bool move = true;
		
		//movement
		if (Input.GetKeyDown("left")){
			newFrontPos.x +=10;
			//checks to make sure ally not in space you want to move
			//if so, can't move there
			if(newFrontPos == middlePos)
				move = false;
			if(move){
				back.transform.position = middlePos;
				middle.transform.position = frontPos;
				front.transform.position  = newFrontPos;
			}
			move = true;
		}
		//lather, rinse, repeat
		if (Input.GetKeyDown("up")){
			newFrontPos.z -=10;
			if(newFrontPos == middlePos)
				move = false;
			if(move){
			back.transform.position = middlePos;
			middle.transform.position = frontPos;			
            front.transform.position  = newFrontPos;
			}
			move = true;
		}
		if (Input.GetKeyDown("down")){
			newFrontPos.z +=10;
			if(newFrontPos == middlePos)
				move = false;
			if(move){				
			back.transform.position = middlePos;
			middle.transform.position = frontPos;			
            front.transform.position  = newFrontPos;
			}
			move = true;
		}           	
		if (Input.GetKeyDown ("right")){
			newFrontPos.x -=10;
			if(newFrontPos == middlePos)
				move = false;
			if(move){
			back.transform.position = middlePos;
			middle.transform.position = frontPos;			
            front.transform.position  = newFrontPos;
			}
			move = true;
		}
	}
	
}
