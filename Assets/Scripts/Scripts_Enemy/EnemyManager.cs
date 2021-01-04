using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance; //allow us to get classes and other things from other scripts/objects

    [SerializeField]
    private GameObject prefabFoe, prefabSwine; //for creating duplicates

    [SerializeField]
    private int foeCount, swineCount;

    private int initialFoeCount, initialSwineCount; //to save the values above
    public float waitTimeBeforeSpawn = 10f;

    public Transform[] foeSpawnPoints, swineSpawnPoints;

    void Awake() {
        makeInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        initialFoeCount = foeCount;
        initialSwineCount = swineCount;

        spawnEnemies();
        StartCoroutine("enemiesSpawnCheck");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void makeInstance() {
        if(instance == null) {
            instance = this;
        }
    }

    void spawnEnemies() {
        spawnSwine();
        spawnFoe();
    }

    void spawnSwine() {
        int index = 0;

        //spawn swine at SP_x position
        for(int i = 0; i < swineCount; i++) {
            //reset index back to 0
            if(index >= swineSpawnPoints.Length) {
                index = 0;
            }
            
            Instantiate(prefabSwine, swineSpawnPoints[index].position,  Quaternion.identity);
            index++;
        }

        swineCount = 0;
    }

    void spawnFoe() {
        int index = 0;

        //spawn foe at SP_x position
        for(int i = 0; i < foeCount; i++) {
            //reset index back to 0
            if(index >= foeSpawnPoints.Length) {
                index = 0;
            }

            Instantiate(prefabFoe, foeSpawnPoints[index].position,  Quaternion.identity);
            index++;
        }

        foeCount = 0;
    }

    //if enemy has died, increase count
    public void EnemyDied(bool foe) {
        if(foe) {
            foeCount++;

            if(foeCount > initialFoeCount) {
                foeCount = initialFoeCount;
            }
        } else {
            swineCount++;

            if(swineCount > initialSwineCount) {
                swineCount = initialSwineCount;
            }
        }
    }

    IEnumerator enemiesSpawnCheck() {
        yield return new WaitForSeconds(waitTimeBeforeSpawn);

        spawnSwine();
        spawnFoe();
        StartCoroutine("enemiesSpawnCheck"); //infinite coroutines
    }

    public void stopSpawning() {
        StopCoroutine("enemiesSpawnCheck");
    }
}
