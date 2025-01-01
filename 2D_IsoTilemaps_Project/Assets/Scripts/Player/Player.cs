using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float health, maxHealth, bulletSpeed, bulletDamage, bulletCooldownTime;

    [SerializeField]
    private Image backbarImg, frontbarImg;

    [SerializeField]
    private GameObject bullet;

    private bool OnCooldownBullet = false;

    public static Player player;

    void Start()
    {
        player = this;
        maxHealth = 100.0f;
        bulletSpeed = 2.0f;
        bulletDamage = 50.0f;
        bulletCooldownTime = 0.5f;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        DetectInput();
    }

    private void DetectInput()
    {
        if (Input.GetKeyDown(KeyCode.F) && !OnCooldownBullet) // Fire bullet
        {
            GameObject newbullet = Instantiate(bullet);
            newbullet.transform.position = transform.position;

            Vector2 currentDirection = GetComponent<IsometricPlayerMovementController>().getCurrentDirection();
            newbullet.AddComponent<BulletBehavior>().Fire(currentDirection, bulletSpeed, bulletDamage);
            StartCoroutine(CooldownBullet()); // Cooldown Bullet
        }
    }

    private void UpdateUI()
    {
        float backbarWidth = backbarImg.GetComponent<RectTransform>().rect.width;
        // convert health to display
        frontbarImg.GetComponent<RectTransform>().offsetMax = new Vector2(-(backbarWidth - (health / maxHealth) * backbarWidth) , 0);
    }

    public void DealDamage(float Damage)
    {
        health = Mathf.Max(0, health - Damage);
        if(health <= 0)
        {
            // Game Over
        }
    }

    IEnumerator CooldownBullet()
    {
        OnCooldownBullet = true;
        // Wait for the cooldown time
        yield return new WaitForSeconds(bulletCooldownTime);
        OnCooldownBullet = false;
    }


}
