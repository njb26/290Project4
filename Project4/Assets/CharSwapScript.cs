	/*
	 *Code by Niklas Bauer, njb26 
	 * */
using UnityEngine;
using System.Collections;

public class CharSwapScript : MonoBehaviour {
	GameObject[] allies;
	// Use this for initialization
	void Start () {
		//find all players		
		allies = GameObject.FindGameObjectsWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
		//swap right two partners
		if(Input.GetKeyDown("w")){
			GameObject middle = null;
			GameObject right = null;
			//find which one is in the middle and the right, assign to correct GameObject
			foreach(GameObject obj in allies){
				if(obj.GetComponent<CharBehaviorScript>()){
					if(obj.GetComponent<CharBehaviorScript>().charPos == 1)
						middle = obj;
					if(obj.GetComponent<CharBehaviorScript>().charPos == 2)
						right = obj;
				}
			}
			Vector3 tempPos = middle.transform.position;
			
			//swap positions
			middle.transform.position = right.transform.position;
			right.transform.position  = tempPos;
			//set new charPos
			middle.GetComponent<CharBehaviorScript>().charPos = 2;
			right.GetComponent<CharBehaviorScript>().charPos = 1;
		}
		
		//swap left two partners
		if(Input.GetKeyDown("q")){
			GameObject middle = null;
			GameObject left = null;
			//find which one is in the middle and the left, assign to correct GameObject
			foreach(GameObject obj in allies){
				if(obj.GetComponent<CharBehaviorScript>()){
					if(obj.GetComponent<CharBehaviorScript>().charPos == 1)
						middle = obj;
					if(obj.GetComponent<CharBehaviorScript>().charPos == 0)
						left = obj;
				}
			}
			Vector3 tempPos = middle.transform.position;
			
			//swap positions
			middle.transform.position  =  left.transform.position;
			left.transform.position  =  tempPos;
			//set new charPos
			middle.GetComponent<CharBehaviorScript>().charPos = 0;
			left.GetComponent<CharBehaviorScript>().charPos = 1;
		}
	}
}
