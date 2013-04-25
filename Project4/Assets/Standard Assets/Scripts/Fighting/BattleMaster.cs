using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleMaster : MonoBehaviour {
    public enum State {
        Playing,
        EnemyThinking,
        FighterThinking,
        ThiefThinking,
        WizardThinking,
        Acting,
        Won,
        Lost
    }
    public static State state = State.Playing;

    // Timing variables
    public float ActionResultDelay;
    public float EnemyDecideDelay;
    float delay = 0f;
    bool delaying = false;

    // Hero variables
    public Entity Fighter, Thief, Wizard;
    public Entity[] players {
        get {
            var _players = new Entity[3];
            _players[Fighter.position] = Fighter;
            _players[Thief.position] = Thief;
            _players[Wizard.position] = Wizard;

            return _players;
        }
    }
    public bool FighterIntercept = false;

    // Attack queue variables
    Queue<Entity> waiting;
    HashSet<Entity> toremove;
    public Entity acting;
    public Entity target;

	// Use this for initialization
	void Start() {
        var entities = GameMaster.players;

        for (int i = 0; i < entities.Length; i++) {
            switch (entities[i].type) {
                case Entity.Type.Fighter :
                    Fighter = entities[i];
                    break;
                case Entity.Type.Thief :
                    Thief = entities[i];
                    break;
                case Entity.Type.Wizard :
                    Wizard = entities[i];
                    break;
                default :
                    if (Fighter == null)
                        Fighter = entities[i];
                    else if (Thief == null)
                        Thief = entities[i];
                    else if (Wizard == null)
                        Wizard = entities[i];
                    break;
            }
        }

        acting = null;
	}
	
	// Update is called once per frame
	void Update () {
        // Count down any delays.
        if (delay > 0.0) {
            delay -= Time.deltaTime;
            delaying = true;
            return;
        }

        delaying = false;

        switch (state) {
            case State.Playing:
                target = null;
                if (waiting.Peek().type == Entity.Type.Enemy) {
                    acting = waiting.Dequeue();
                    delay = EnemyDecideDelay;
                    delaying = true;
                    state = State.EnemyThinking;
                }
                break;
            case State.EnemyThinking:
                // Choose target.
                if (FighterIntercept) {
                    target = Fighter;
                }
                else {
                    var chance = (int)(Random.value * 100);
                    if (chance < 66) {
                        target = GetPlayer(0);
                    }
                    else if (chance < 90) {
                        target = GetPlayer(1);
                    }
                    else {
                        target = GetPlayer(2);
                    }
                }

                // Attack target.
                delay = ActionResultDelay;
                delaying = true;
                state = State.Acting;
                break;
            case State.FighterThinking :
                target = null;
                break;
            case State.ThiefThinking :
                target = null;
                break;
            case State.WizardThinking :
                target = null;
                break;
            case State.Acting :
                if (target != null) {
                    int damage = acting.Attack(0, target);

                    // TODO: Display damage result on screen.

                    if (target.HP < 0) {
                        
                    }
                }
                break;
            case State.Won :
                target = null;
                break;
            case State.Lost :
                target = null;
                break;
        }
	}

    public Entity GetPlayer(int position) {
        if (Fighter.position == position)
            return Fighter;
        if (Thief.position == position)
            return Thief;
        if (Wizard.position == position)
            return Wizard;
        return null;
    }

    public void Reset(int enemycount) {
        List<Entity> entities = new List<Entity>(players);
        // TODO: Import enemies.
        entities.Sort();

        waiting = new Queue<Entity>();
        toremove = new HashSet<Entity>();
        foreach (Entity entity in entities)
            waiting.Enqueue(entity);

        acting = null;
        delaying = false;
        FighterIntercept = false;
    }
}
