using UnityEngine;
using System.Collections;

public class MainCameraScript : MonoBehaviour {
	GameObject[] allies;
	float lerpAmount;
	// Use this for initialization
	void Start () {		
		allies = GameObject.FindGameObjectsWithTag("Player");
		lerpAmount = 0f;
	}
	
	// Update is called once per frame
	void Update () {
			GameObject target = null;
			//find which one is in the middle and the right, assign to correct GameObject
			foreach(GameObject obj in allies){
				if(obj.GetComponent<CharBehaviorScript>()){
					if(obj.GetComponent<CharBehaviorScript>().charPos == 1)
						target = obj;
				}
			}
		Vector3 newPos = transform.position;
		
		newPos.x = target.transform.position.x;
		newPos.z = target.transform.position.z;
		transform.position = Vector3.Lerp(transform.position, newPos, lerpAmount);
		lerpAmount+= Time.deltaTime;
	}
}
