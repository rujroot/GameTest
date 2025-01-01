using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Monster
{
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private float bulletSpeed;

    protected override void InitStatus()
    {
        detectPlayerRange = 4.0f;
        attackPlayerRange = 1.5f;
        attackDamage = 10.0f;
        health = 75.0f;
        maxHealth = 75.0f;
        attackCooldown = 3.0f;
        bulletSpeed = 2.0f;
    }

    public override void Attack()
    {
        // get current direction
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        Vector2 currentPosition = rbody.position;

        // get player position
        Player player = Player.player;
        Transform playerTransform = player.gameObject.transform;
        Vector2 playerPosition = new Vector2(playerTransform.position.x, playerTransform.position.y);

        // find direction to fire
        Vector2 direction = (playerPosition - currentPosition).normalized;
        GameObject newbullet = Instantiate(bullet);
        newbullet.transform.position = transform.position;
        newbullet.AddComponent<BulletBehavior>().Fire(direction, bulletSpeed, attackDamage);

        StartCoroutine(CooldownAttack());
    }
}
