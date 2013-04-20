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
		Vector3 middlePos = middle.transform.position;
		//movement
		if (Input.GetKeyDown("left")){
			back.transform.position = middlePos;
			middle.transform.position = frontPos;
			frontPos.x +=10;
            front.transform.position  = frontPos;
		}           			
		if (Input.GetKeyDown("up")){
			back.transform.position = middlePos;
			middle.transform.position = frontPos;
			frontPos.z -=10;
            front.transform.position  = frontPos;
		}
		if (Input.GetKeyDown("down")){
			back.transform.position = middlePos;
			middle.transform.position = frontPos;
			frontPos.z +=10;
            front.transform.position  = frontPos;
		}           	
		if (Input.GetKeyDown ("right")){
			back.transform.position = middlePos;
			middle.transform.position = frontPos;
			frontPos.x -=10;
            front.transform.position  = frontPos; 
		}
	}
	
}
