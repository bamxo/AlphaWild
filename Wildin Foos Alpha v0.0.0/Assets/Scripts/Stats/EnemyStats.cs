using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public float deathAnimationLength = 0.22f;
    public override void Die()
    {
        base.Die();

        // add ragdoll effect / death animation

        Destroy(gameObject, deathAnimationLength);
    }
}
