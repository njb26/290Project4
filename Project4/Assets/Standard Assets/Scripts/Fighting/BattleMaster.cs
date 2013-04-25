using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleMaster : MonoBehaviour {
#region Variables

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
    public GameObject g_Fighter, g_Thief, g_Wizard;
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
	
	// Enemy Variables 
	public Entity currentEmeny;
	
	
    // Attack queue variables
    Queue<Entity> waiting;
    HashSet<Entity> toremove;
    public Entity acting;
    public Entity target;

#endregion

#region GUI
    public int R_LINEHEIGHT = 18;
    public int R_LOGCOUNT = 7;

    private readonly string S_DAMAGED =   "{0} attacked {1} for {2} damage!";
    private readonly string S_HUD = "{0}\nHP: {1}/{2}";

    /// <summary>
    ///  The log that is displayed on the bottom.
    /// </summary>
    LinkedList<string> log;

#endregion

	// Use this for initialization
	void Start() {
        InitPlayers();
        acting = null;
        Reset(0,0,0);
	}
	
    private int count = 0;
	// Update is called once per frame
	void Update () {
        // Count down any delays.
        if (delay > 0.0) {
            delay -= Time.deltaTime;
            delaying = true;
            return;
        }

        delaying = false;

        // TODO: Remove
        {
            Fighter.position = (Fighter.position + 1) % 3;
            Thief.position = (Thief.position + 1) % 3;
            Wizard.position = (Wizard.position + 1) % 3;
            Log("Printing: " + ++count);
            delay = 2f;
            delaying = true;
            return;
        }

        while (toremove.Contains(waiting.Peek()))
            waiting.Dequeue();

        switch (state) {
            case State.Playing:
                target = null;
                if (waiting.Peek().type == Entity.Type.Enemy) {
                    acting = waiting.Dequeue();
                    delay = EnemyDecideDelay;
                    delaying = true;
                    state = State.EnemyThinking;
                }
                else if (waiting.Peek().type == Entity.Type.Fighter) {
                    acting = waiting.Dequeue();
                    delay = EnemyDecideDelay;
                    delaying = true;
                    state = State.FighterThinking;
                }
                else if (waiting.Peek().type == Entity.Type.Thief) {
                    acting = waiting.Dequeue();
                    delay = EnemyDecideDelay;
                    delaying = true;
                    state = State.ThiefThinking;
                }
                else if (waiting.Peek().type == Entity.Type.Wizard) {
                    acting = waiting.Dequeue();
                    delay = EnemyDecideDelay;
                    delaying = true;
                    state = State.WizardThinking;
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

                    Log(string.Format(S_DAMAGED, acting.name, target.name, damage));

                    if (target.HP < 0) {
                        var drop = target.Die();
                        GameObject.Destroy(target.parent);

                        // TODO: Add drop to player inv.
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

    void OnGUI() {
        DisplayLog();
        DisplayHUD(Fighter);
        DisplayHUD(Thief);
        DisplayHUD(Wizard);

        switch (state) {
            case State.Playing :
                break;
            case State.EnemyThinking :
                break;
            case State.FighterThinking :
                break;
            case State.ThiefThinking :
                break;
            case State.WizardThinking :
                break;
            case State.Acting :
                break;
            case State.Won :
                break;
            case State.Lost :
                break;
        }
    }

    void DisplayLog() {
        string text = string.Empty;
        foreach (string line in log) {
            text += line + "\n";
        }

        int height = R_LOGCOUNT * R_LINEHEIGHT;
        GUI.Box(new Rect(
                    0,
                    Screen.height - height,
                    Screen.width,
                    height),
                text);
    }

    void DisplayHUD(Entity player) {
        GUI.Box(new Rect(
                    player.position * Screen.width / 3,
                    0,
                    Screen.width / 3,
                    2 * R_LINEHEIGHT),
                string.Format(
                    S_HUD,
                    player.name,
                    player.MaxHP,
                    player.HP));
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

    public void Reset(int physicalEnemyCount, int magicalEnemyCount, int BossEnemyCount) {
        List<Entity> entities = new List<Entity>(players);
        
		while(physicalEnemyCount>0){		
			entities.Add(CreatePhysicalEnemy());
			physicalEnemyCount--;
		}		
		while(magicalEnemyCount>0){
			entities.Add(CreateMagicalEnemy());
			magicalEnemyCount--;
		}
		while(BossEnemyCount>0){
			entities.Add(CreateBossEnemy());
			BossEnemyCount--;
		}
		
        entities.Sort();

        log = new LinkedList<string>();

        waiting = new Queue<Entity>();
        toremove = new HashSet<Entity>();
        foreach (Entity entity in entities)
            waiting.Enqueue(entity);

        acting = null;
        delaying = false;
        FighterIntercept = false;
    }

    public void Log(string text) {
        log.AddLast(text);
        if (log.Count > R_LOGCOUNT) {
            log.RemoveFirst();
        }
    }

    void InitPlayers() {
        // Fighter
        Fighter = new Entity();
        Fighter.name = "Fighter";
        Fighter.parent = g_Fighter;
        Fighter.MaxHP = Fighter.HP = 40;
        Fighter.Atk = 5; Fighter.Def = 6;
        Fighter.Mag = 0; Fighter.Res = 1;
        Fighter.Spd = 3; Fighter.position = 0;

        // Thief
        Thief = new Entity();
        Thief.name = "Thief";
        Thief.parent = g_Thief;
        Thief.MaxHP = Thief.HP = 30;
        Thief.Atk = 3; Thief.Def = 4;
        Thief.Mag = 1; Thief.Res = 2;
        Thief.Spd = 6; Thief.position = 1;

        // Wizard
        Wizard = new Entity();
        Wizard.name = "Wizard";
        Wizard.parent = g_Wizard;
        Wizard.MaxHP = Wizard.HP = 20;
        Wizard.Atk = 1; Wizard.Def = 2;
        Wizard.Mag = 8; Wizard.Res = 8;
        Wizard.Spd = 5; Wizard.position = 2;
    }
	
	
	public Entity CreatePhysicalEnemy(){
		Entity physicMob = new Entity();
		physicMob.name="physicMob";
		//physicMob.parent = 
		physicMob.MaxHP = physicMob.HP = 30;
		physicMob.Atk = 5; physicMob.Def = 5;
       	physicMob.Mag = 0; physicMob.Res = 2;
        physicMob.Spd = 2; physicMob.position = 0;
		
		return physicMob;
	}
	
	public Entity CreateMagicalEnemy(){
		Entity magicMob = new Entity();
		magicMob.name = "magicMob";
        //magicMob.parent = 
        magicMob.MaxHP = magicMob.HP = 18;
        magicMob.Atk = 1; magicMob.Def = 1;
        magicMob.Mag = 7; magicMob.Res = 10;
        magicMob.Spd = 4; magicMob.position = 0;	
		
		return magicMob;	
	}	
	
	public Entity CreateBossEnemy(){
		Entity BossMob = new Entity();
		BossMob.name = "BossMob";
        //BossMob.parent = 
        BossMob.MaxHP = BossMob.HP = 60;
        BossMob.Atk = 8; BossMob.Def = 10;
        BossMob.Mag = 10; BossMob.Res = 12;
        BossMob.Spd = 7; BossMob.position = 0;	
		
		return BossMob;	
	}	
}
