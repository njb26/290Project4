using UnityEngine;
using System.Collections;

public class CharSwapScript : MonoBehaviour {
	float lerpAmount = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//find all players
		GameObject[] allies;
		allies = GameObject.FindGameObjectsWithTag("Player");
						
		//swap right two partners
		if(Input.GetKeyDown("w")){
			GameObject middle, right = null;
			//find which one is in the middle and the right, assign to correct GameObject
			foreach(GameObject obj in allies){
				if(obj.GetComponent<CharMovementScript>()){
					if(obj.GetComponent<CharMovementScript>().charPos == 1)
						middle = obj;
					if(obj.GetComponent<CharMovementScript>().charPos == 2)
						right = obj;
				}
			}
			Vector3 tempPos = middle.transform.position;
			
			//swap positions
			middle.transform.position  = Vector3.Lerp(middle.transform.position, right.transform.position, lerpAmount);
			right.transform.position  = Vector3.Lerp(right.transform.position, tempPos, lerpAmount);
			//set new charPos
			middle.GetComponent<CharMovementScript>().charPos = 2;
			right.GetComponent<CharMovementScript>().charPos = 1;
			lerpAmount += Time.deltaTime;
		}
		
		//swap left two partners
		if(Input.GetKeyDown("q")){
			GameObject middle, left = null;
			//find which one is in the middle and the left, assign to correct GameObject
			foreach(GameObject obj in allies){
				if(obj.GetComponent<CharMovementScript>()){
					if(obj.GetComponent<CharMovementScript>().charPos == 1)
						middle = obj;
					if(obj.GetComponent<CharMovementScript>().charPos == 0)
						left = obj;
				}
			}
			Vector3 tempPos = middle.transform.position;
			
			//swap positions
			middle.transform.position  = Vector3.Lerp(middle.transform.position, left.transform.position, lerpAmount);
			left.transform.position  = Vector3.Lerp(left.transform.position, tempPos, lerpAmount);
			//set new charPos
			middle.GetComponent<CharMovementScript>().charPos = 0;
			left.GetComponent<CharMovementScript>().charPos = 1;
			lerpAmount += Time.deltaTime;
		}
	}
}
