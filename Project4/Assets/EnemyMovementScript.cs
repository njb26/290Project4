	/*
	 *Code by Niklas Bauer, njb26 
	 * */
	

using UnityEngine;
using System.Collections;

public class EnemyMovementScript : MonoBehaviour {
	/*
	 * Variables needed
	 * */
	
	//these bools are set in the Inspector in the Unity Scene
	public bool vertical; //enemy patrols up and down
	public bool horizontal;// enemy patrols left and right
	//for simplicity, the game will not have the baddies patrol
	//a path that is a combination of these
	public float walkRange; //how far rom the base position they walk
	Vector3 negLimit;
	Vector3 posLimit;
	public int sightRange;//how far they can "see"
	Vector3 negSightLimit;
	Vector3 posSightLimit;
	bool insidePatrol; //true if patrolling, false if they have lef the patrol area and are chasing allies
	
	Vector3 basePos; //initial position they are on the map
	
	//float moveStep = 100.0f;//at a rate 1 = 1 sec, make a move each moveStep
	float moveTime;
	
	//if horizontal, false means walk left, true is walk right
	//if vertical, false is walk down, true is walk up
	bool walkDirection;
	
	
	// Use this for initialization
	void Start () {
		//base values
		basePos = transform.position;	
		moveTime = 0.0f;
		walkDirection = false;
		//set limit on positions for patrolling behavior
		setPatrolLimit();
		insidePatrol = true;
	}
	
	/*
	 * Enemies patrol a certain number of tiles either left-right or up-down of the position they are placed on the map. 
	 * number of tiles set in Unity Inspector, as well as whether or not it moves horizontal or vertical when patrolling
	 * will only move horiz XOR vertical
	 * */
	
	void setPatrolLimit(){
		negLimit = transform.position;
		posLimit = transform.position;
		if(horizontal){
			negLimit.x = basePos.x + walkRange*10;
			posLimit.x = basePos.x - walkRange*10;
		}
		else if(vertical){
			negLimit.z = basePos.z + walkRange*10;
			posLimit.z = basePos.z - walkRange*10;
		}
		else{
			print ("Error: horizontal or vertical movement not specified");
		}
	}

	
	// Update is called once per frame
	void Update () {
		moveTime += Time.deltaTime;
		if(moveTime >=1.0f){
			//sets the positions it can "see"
			setSightLimit();
			//scans those positions for allies
			if(!scanForAllies() && insidePatrol){ //hasn't found allies, and is inside the patrol zone			
				if(horizontal){
					horizontalMove();
				}
				else if(vertical){
					verticalMove();
				}
			}
			/*
			 * Reminder: it is only executing one step of movement. So returnToBasePos() will simply move it one tile closer to the BasePos
			 * and chaseAllies will move it one tile closer to the allies
			 * */
			else if(!scanForAllies() && !insidePatrol){//doesn't see ally, not in patrol zone (you escaped)
				returnToBasePos();//for simplicity, just sending it back to original spawn position
			}
			else{ //else it has seen you and is chasing you
				chaseAllies();
			}
			moveTime = 0;
		}	
	}
	
	/*
	 * As of now, enemies can see a certain number of tiles around them in all directions.
	 * number of tiles is set in Unity Inspector
	 * */	
	
	void setSightLimit(){
		negSightLimit = transform.position;
		posSightLimit = transform.position;
		
		negSightLimit.x = transform.position.x + sightRange*10;
		negSightLimit.z = transform.position.z + sightRange*10;
		
		posSightLimit.x = transform.position.x - sightRange*10;
		posSightLimit.z = transform.position.z - sightRange*10;
	}
	
	/*
	 * logic to move horizontally
	 * */
	
	void horizontalMove(){
		Vector3 newPos;
		if(walkDirection){//if it should walk left
			//move a step to the left
			newPos = transform.position;
			newPos.x +=10;
			transform.position = newPos;
			//check if at max range, if so, next time it walks right
			if(transform.position == negLimit){
				walkDirection = false;
			}
		}
		else{//it should walk right
			//move a step to the right
			newPos = transform.position;
			newPos.x -=10;
			transform.position = newPos;
			//check if at max range, if so, next time it walks left
			if(transform.position == posLimit){
				walkDirection = true;
			}
		}
	}
	/*
	 * logic to move vertically
	 * */
	
	
	void verticalMove(){
		Vector3 newPos;
		if(walkDirection){//if it should walk down
			//move a step down
			newPos = transform.position;
			newPos.z +=10;
			transform.position = newPos;
			//check if at max range, if so, next time it walks up
			if(transform.position == negLimit){
				walkDirection = false;
			}
		}
		else{//it should walk up
			//move a step up
			newPos = transform.position;
			newPos.z -=10;
			transform.position = newPos;
			//check if at max range, if so, next time it walks down
			if(transform.position == posLimit){
				walkDirection = true;
			}
		}
	
	}
	/*
	 * true if it can see any ally
	 * false otherwise
	 * also, if it can see you, insidePatrol = false, and will be false until it can't see you and is back at basePos 
	 * */
	
	
	bool scanForAllies(){
		//get all Allies
		GameObject[] allies;
		allies = GameObject.FindGameObjectsWithTag("Player");
		
		foreach(GameObject obj in allies){
			float xVal = obj.transform.position.x;
			float zVal = obj.transform.position.z;
			//see if inside the horizontal part of sightRange
			if(xVal >= posSightLimit.x && xVal <= negSightLimit.x){
				//check if inside the vertical sightRange
				if(zVal >= posSightLimit.z && zVal <= negSightLimit.z){
					insidePatrol = false;
					return true;
				}			
			}	
		}
		return false;	
	}
	/*
	 * Execute one step towards returning to basePos
	 * If at basePos, insidePatrol = true and it resumes it's set patrol path 
	 * */
	
	//I originally wanted to do a pathfinding AI for the following
	//using what I've learned in EECS 391
	//but the world hasn't been built yet, and I don't want to make assumptions and have to rewrite a bunch of code
	//so it's going to be a nice little amateur code hack
	bool swap = true;
	void returnToBasePos(){
		float xVal = transform.position.x;
		float zVal = transform.position.z;
		Vector3 newPos = transform.position;
		//alternates moving along x and z coordinates to get there
		//can add in check to see if valid position later
		if(swap){
			if(xVal > basePos.x){
				newPos.x-=10;
				transform.position = newPos;				
			}
			else if(xVal < basePos.x){
				newPos.x+=10;
				transform.position = newPos;
			}
			swap = false;
		}
		else{
			if(zVal > basePos.z){
				newPos.z-=10;
				transform.position = newPos;
			}
			else if(zVal < basePos.z){
				newPos.z+=10;
				transform.position = newPos;
			}
			swap = true;
		}
		//check if at basePos
		if(transform.position == basePos){
			insidePatrol = true;
		}
		
	}
	
	/*
	 * Executes one step towards chasing allies.
	 * Will move until ON an ally, can be changed but for right now
	 * I'm forced to assume that ON an ally will initialize an attack/battle sequence 
	 * */
	
	bool swap2 = true;
	void chaseAllies(){
		//find all allies
		GameObject[] allies;
		allies = GameObject.FindGameObjectsWithTag("Player");
		//grab a random one of them
		GameObject target = allies[Random.Range(1, 3)];
		float targetX = target.transform.position.x;
		float targetZ = target.transform.position.z;
		
		float xVal = transform.position.x;
		float zVal = transform.position.z;
		Vector3 newPos = transform.position;
		
		if(swap2){
			if(xVal > targetX){
				newPos.x-=10;
				transform.position = newPos;				
			}
			else if(xVal < targetX){
				newPos.x+=10;
				transform.position = newPos;
			}
			swap2 = false;
		}
		else{
			if(zVal > targetZ){
				newPos.z-=10;
				transform.position = newPos;
			}
			else if(zVal < targetZ){
				newPos.z+=10;
				transform.position = newPos;
			}
			swap2 = true;
		}
				
			
	}

}
