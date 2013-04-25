using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

    public static Entity player1 = new Entity();
    public static Entity player2 = new Entity();
    public static Entity player3 = new Entity();

    public static Entity[] players {
        get {
            return new Entity[] { player1, player2, player3 };
        }
        set {
            player1 = value[0];
            player2 = value[1];
            player3 = value[2];
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
