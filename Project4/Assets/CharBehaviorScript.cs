	/*
	 *Code by Niklas Bauer, njb26 
	 * */
using UnityEngine;
using System.Collections;

public class CharBehaviorScript : MonoBehaviour {
	
	//haha this is all that's left from revising an earlier version of code
	//simply here to store the position variable
	public int charPos;
	// Use this for initialization
	void Start () {
	
	}
	Color newCol = new Color(.9f,.9f,.9f,1f);
	Color standardCol = new Color(1f,1f,1f,1f);
	// Update is called once per frame
	void Update () {
		if(charPos == 0 || charPos == 1){
			transform.renderer.material.color = newCol;		
		}
		else{
			transform.renderer.material.color = standardCol;
		}
	
	}
}
