using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    [SerializeField]
    protected float detectPlayerRange, attackPlayerRange, attackDamage, attackCooldown, health, maxHealth;

    [SerializeField]
    private Image backbarImg, frontbarImg;

    private GameObject player;
    private MonsterMovementController monsterMovementController;
    private string stage = "idle";
    private bool onAttackCooldown;

    protected virtual void InitStatus()
    {
        detectPlayerRange = 2.5f;
        attackPlayerRange = 0.5f;
        attackDamage = 10.0f;
        health = 100.0f;
        maxHealth = 100.0f;
        attackCooldown = 1.0f;
        onAttackCooldown = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        monsterMovementController = GetComponent<MonsterMovementController>();
        player = GameObject.FindGameObjectWithTag("Player"); // get player instance with tag
        onAttackCooldown = false;
        InitStatus();
    }

    // Login Control part
    void Update()
    {
        UpdateUI();
        Think();
    }

    private void Think()
    {
        switch (stage)
        {
            case "idle":
                SearchPlayer();
                break;
            case "walk":
                WalkToPlayer();
                break;
            case "attack":
                AttackPlayer();
                break;
        }
    }

    // Search for player if player not in detect range
    public virtual void SearchPlayer() {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= detectPlayerRange) // walk to player
        {
            monsterMovementController.MoveTo(player.transform);
            stage = "walk";
        }
        else // random walk
        {
            monsterMovementController.RandomWalk();
        }

    }

    // Walk to player if player in detect range
    public virtual void WalkToPlayer() {
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

    // attack player if player in attack range
    public virtual void AttackPlayer() {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance >= attackPlayerRange) // if not in attack range
        {
            if(distance <= detectPlayerRange) // check if in detect range
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
        else if(!onAttackCooldown) // attack player
        {
            this.Attack();
        }
    }

    // Attack player
    public virtual void Attack()
    {
        Player player = Player.player;
        player.DealDamage(attackDamage);
        StartCoroutine(CooldownAttack());
    }

    protected IEnumerator CooldownAttack()
    {
        onAttackCooldown = true;
        // Wait for the cooldown time
        yield return new WaitForSeconds(attackCooldown + Random.Range(-attackCooldown * 0.25f, attackCooldown * 0.25f));
        onAttackCooldown = false;
    }

    private void UpdateUI()
    {
        float backbarWidth = backbarImg.GetComponent<RectTransform>().rect.width;
        // convert health to display
        frontbarImg.GetComponent<RectTransform>().offsetMax = new Vector2(-(backbarWidth - (health / maxHealth) * backbarWidth), 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            float damage = other.gameObject.GetComponent<BulletBehavior>().getDamage();
            setHealth(health - damage);
            Destroy(other.gameObject);
        }
    }

    // getter setter
    public float getHealth()
    {
        return health;
    }

    public void setHealth(float health)
    {
        this.health = Mathf.Max(0, health);

        if(health <= 0)
        {
            Destroy(gameObject);
        }

    }

}
