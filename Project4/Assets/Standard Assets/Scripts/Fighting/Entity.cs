/// Wes Rupert - wkr3
/// EECS 290   - Project 04
/// RPG        - Entity.cs
/// Script to store enemy and player information.

using UnityEngine;
using System.Collections.Generic;

public class Entity : System.IComparable<Entity> {
    public enum AttackType { Physical, Magical }
    public enum Type {
        Enemy,
        Fighter,
        Thief,
        Wizard
    }

    const double THRESHOLD = 5.0;
    const int ATKTHRESHOLD = 7;

    public Type type;
    public AttackType[] a_type;
    public int[]        a_pow;

    public GameObject parent;
    public string name;
    public int MaxHP, HP, Atk, Def, Mag, Res, Spd;
    public int position;
    public int drop;
    public double dropchance;

    public double sortOffset;

    public Entity() {
        sortOffset = (Random.value - 0.5) * THRESHOLD;
    }

    public int Attack(int index, Entity enemy) {
        // Get the stats used in the damage calculation.
        int A, D, P;
        switch (a_type[index]) {
            case AttackType.Physical :
                A = Atk;
                D = enemy.Def;
                P = a_pow[index];
                break;
            case AttackType.Magical :
                A = Mag;
                D = enemy.Res;
                P = a_pow[index];
                break;
            default :
                return -1;
        }

        // Calculate thedamage dealt.
        int R = (int)(Random.value * 2 * ATKTHRESHOLD)+ 256 - ATKTHRESHOLD;
        int H = A + P - D;
        H = H > 1 ? H : 1;
        int damage = R * H / 255;

        // Deal the damage calculated.
        enemy.HP -= damage;
        return damage;
    }

    public int Die() {
        int numdropped = 0;
        while (Random.value + (0.5 * numdropped) > dropchance)
            numdropped++;

        return numdropped;
    }

    public int CompareTo(Entity other) {
        var ourValue = Spd + sortOffset;
        var theirValue = other.Spd + other.sortOffset;

        return ourValue.CompareTo(theirValue);
    }
}
