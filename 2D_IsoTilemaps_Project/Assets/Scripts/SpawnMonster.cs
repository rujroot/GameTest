using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
    [SerializeField] private int monsterPerMap;
    [SerializeField] private List<GameObject> spawnPoints;
    [SerializeField] private List<GameObject> spawnMosnters;
    [SerializeField] private float cooldownSpawn;

    private GameObject monsterGroup;

    // Start is called before the first frame update
    void Start()
    {
        monsterPerMap = 20;
        cooldownSpawn = 10.0f;
        monsterGroup = GameObject.Find("MonsterGroup");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawn(int count)
    {
        
    }
}
