using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
    [SerializeField] private int monsterPerMap;
    [SerializeField] private List<GameObject> spawnPoints;
    [SerializeField] private List<GameObject> spawnMosnters;
    [SerializeField] private float cooldownSpawnTime;

    private bool onCooldown = false;
    private GameObject monsterGroup;

    // Start is called before the first frame update
    void Start()
    {
        monsterPerMap = 20;
        cooldownSpawnTime = 10.0f;
        monsterGroup = GameObject.Find("MonsterGroup");
    }

    // Update is called once per frame
    void Update()
    {
        int currentCountMonster = monsterGroup.transform.childCount;
        if((currentCountMonster < monsterPerMap && !onCooldown) || currentCountMonster == 0)
        {
            int randomSpawnCount = Random.Range(1, 6);
            randomSpawnCount = Mathf.Min(monsterPerMap - currentCountMonster, randomSpawnCount);
            Spawn(randomSpawnCount);
            StartCoroutine(CooldownSpawn());
        }
    }

    private void Spawn(int count)
    {
        for(int i = 0; i < count; ++i)
        {
            int randomSpawnPoint = Random.Range(0, spawnPoints.Count);
            int randomMonster = Random.Range(0, spawnMosnters.Count);

            // spawn new monster
            GameObject newMonster = Instantiate(spawnMosnters[randomMonster], monsterGroup.transform);
            newMonster.transform.position = spawnPoints[randomSpawnPoint].transform.position + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
        }
    }

    private IEnumerator CooldownSpawn()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownSpawnTime);
        onCooldown = false;
    }

}
