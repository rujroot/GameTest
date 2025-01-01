using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovementController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 1f;
    private IsometricCharacterRenderer isoRenderer;
    private Rigidbody2D rbody;

    private Transform playerTransform;
    private Vector2 randomTarget;

    private bool onRandomWalkCooldown;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
        onRandomWalkCooldown = false;
    }

    public void MoveTo(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
        if(playerTransform == null)
        {
            randomTarget = rbody.position; // if playerTransform is null then stand at current position
        }
    }

    public void RandomWalk()
    {
        if (onRandomWalkCooldown) return;

        Vector2 currentPos = rbody.position;
        randomTarget = currentPos + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)); // random position

        StartCoroutine(CooldownWalk()); // start cooldown
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerTransform == null && randomTarget == null) return; // do nothing if no target

        Vector2 currentPos = rbody.position;
        // if playerTransform is null then walk to randomTarget otherwise walk to player.z
        Vector2 targetPos = (playerTransform == null) ? randomTarget : new Vector2(playerTransform.position.x, playerTransform.position.y);

        // if distance between target is less than 0.05 then monster don't walk
        if ((targetPos - currentPos).magnitude <= 0.05f) {
            isoRenderer.SetDirection(new Vector2(0.0f, 0.0f)); // set idle animation
            return;
        } 

        Vector2 direction = (targetPos - currentPos).normalized; // get direction target
        Vector2 movement = direction * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        isoRenderer.SetDirection(movement);
        rbody.MovePosition(newPos);
    }

    IEnumerator CooldownWalk()
    {
        onRandomWalkCooldown = true;
        // Wait for the cooldown time
        yield return new WaitForSeconds(Random.Range(3.0f, 5.0f));
        onRandomWalkCooldown = false;
    }

}
