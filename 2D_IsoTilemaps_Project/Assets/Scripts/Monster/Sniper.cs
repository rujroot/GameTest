using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Wizard
{
    private float runAwayRange;

    protected override void InitStatus()
    {
        detectPlayerRange = 4.0f;
        runAwayRange = 2.5f;
        attackPlayerRange = 4.0f;
        attackDamage = 15.0f;
        health = 50.0f;
        maxHealth = 50.0f;
        attackCooldown = 5.0f;
        bulletSpeed = 2.5f;
    }

    // Walk to player if player in detect range
    protected override void WalkToPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance >= detectPlayerRange) // not walk
        {
            monsterMovementController.MoveTo(null);
            stage = "idle";
        }
        else if (distance <= attackPlayerRange)
        {
            monsterMovementController.MoveTo(null);
            stage = "attack";
        }

    }

    protected override void AttackPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance <= runAwayRange) // in run away range
        {
            monsterMovementController.RunAway();
        } else if (distance >= attackPlayerRange) // if not in attack range
        {
            if (distance <= detectPlayerRange) // check if in detect range
            {
                monsterMovementController.MoveTo(player.transform);
                stage = "walk";
            }
            else
            {
                monsterMovementController.MoveTo(null);
                stage = "idle";
            }
        }
        else if (!onAttackCooldown) // attack player
        {
            this.Attack();
        }
    }
}
