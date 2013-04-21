using UnityEngine;
using System.Collections;

public class EnemyMovementScript : MonoBehaviour {
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
	}
	
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
	int count = 0;
	// Update is called once per frame
	void Update () {
		count++;
		moveTime += Time.deltaTime;
		if(moveTime >=1.0f){
			//sets the positions it can "see"
			setSightLimit();
			//scans those positions for allies
			scanForAllies();			
			if(horizontal){
				horizontalMove();
			}
			else if(vertical){
				verticalMove();
			}
			moveTime = 0;
		}	
	}
	
	void setSightLimit(){
		negSightLimit = transform.position;
		posSightLimit = transform.position;
		
		negSightLimit.x = transform.position.x + sightRange*10;
		negSightLimit.z = transform.position.z + sightRange*10;
		
		posSightLimit.x = transform.position.x - sightRange*10;
		posSightLimit.z = transform.position.z - sightRange*10;
	}
	
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
					return true;
				}			
			}	
		}
		return false;	
	}

}
