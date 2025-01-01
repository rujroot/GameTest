using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private Vector2 direction;
    private float speed, damage, bulletDuration;
    private Rigidbody2D rbody;
    private bool fired = false;

    public void Fire(Vector2 direction, float speed, float damage)
    {
        rbody = GetComponent<Rigidbody2D>();
        this.direction = direction;
        this.damage = damage;
        this.speed = speed;
        bulletDuration = 3.0f;
        fired = true;

        StartCoroutine(DespawnBullet()); // despawn bullet if not hit in 5 seconds
    }

    // Update is called once per frame
    void Update() // move bullet
    {
        if (!fired) return;

        Vector2 currentPos = rbody.position;
        Vector2 movement = direction * speed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        rbody.MovePosition(newPos);
    }

    public float getDamage()
    {
        return damage;
    }


    IEnumerator DespawnBullet()
    {
        yield return new WaitForSeconds(bulletDuration);
        Destroy(gameObject);
    }
}
