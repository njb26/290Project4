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
    public AttackType[] attacks;

    public int MaxHP, HP, Atk, Def, Mag, Res, Spd;
    public int position;

    public double sortOffset;

    public Entity() {
        sortOffset = (Random.value - 0.5) * THRESHOLD;
    }

    public int Attack(int index, Entity enemy) {
        // Get the stats used in the damage calculation.
        int A, D;
        switch (attacks[index]) {
            case AttackType.Physical :
                A = Atk;
                D = enemy.Def;
                break;
            case AttackType.Magical :
                A = Mag;
                D = enemy.Res;
                break;
            default :
                return -1;
        }

        // Calculate thedamage dealt.
        int R = (int)(Random.value * 2 * ATKTHRESHOLD)+ 256 - ATKTHRESHOLD;
        int H = (A - D > 1) ? (A - D) : 1;
        int damage = R * H / 255;

        // Deal the damage calculated.
        enemy.HP -= damage;
        return damage;
    }

    public int CompareTo(Entity other) {
        var ourValue = Spd + sortOffset;
        var theirValue = other.Spd + other.sortOffset;

        return ourValue.CompareTo(theirValue);
    }
}
