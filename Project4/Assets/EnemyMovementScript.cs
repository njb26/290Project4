using UnityEngine;
using System.Collections;

public class EnemyMovementScript : MonoBehaviour {
	//these bools are set in the Inspector in the Unity Scene
	public bool vertical; //enemy patrols up and down
	public bool horizontal;// enemy patrols left and right
	//for simplicity, the game will not have the baddies patrol
	//a path that is a combination of these
	public float walkRange; //how far rom the base position they walk
	public int sightRange;//how far they can "see"
	Vector3 basePos; //initial position they are on the map
	float moveStep = 100.0f;//at a rate 1 = 1 sec, make a move each moveStep
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
	}
	
	// Update is called once per frame
	void Update () {
		moveTime += Time.deltaTime;
		if(moveTime >=1.0f){
			if(horizontal){
				horizontalMove();
			}
			else if(vertical){
				verticalMove();
			}
		}	
	}
	
	void horizontalMove(){
		Vector3 newPos;
		if(walkDirection){//if it should walk left
			//move a step to the left
			newPos = transform.position;
			newPos.x +=10;
			transform.position = newPos;
			//check if at max range, if so, next time it walks right
			if(transform.position.x == basePos.x + walkRange){
				walkDirection = false;
			}
		}
		else{//it should walk right
			//move a step to the right
			newPos = transform.position;
			newPos.x -=10;
			transform.position = newPos;
			//check if at max range, if so, next time it walks left
			if(transform.position.x == basePos.x - walkRange){
				walkDirection = true;
			}
		}
	}
	
	void verticalMove(){
	
	}

}
