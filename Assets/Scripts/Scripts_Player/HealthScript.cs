using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
        private EnemyAnimation enemyAnimation;
        private EnemyController enemyController;
        private PlayerStats playerStats;
        private NavMeshAgent navAgent;
        private EnemySound enemySound;

        public bool isPlayer, isSwine, isFoe;
        private bool isDead;
        public float health = 100f;


    void Awake() {
        if(isSwine || isFoe) {
            enemyAnimation = GetComponent<EnemyAnimation>();
            enemyController = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();
            enemySound = GetComponentInChildren<EnemySound>();
        }

        //player stats
        if(isPlayer) {
            playerStats = GetComponent<PlayerStats>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void applyDamage(float damage) {
        //if we died don't execute the rest of the code
        if(isDead) {
            return;
        }

        health -= damage;

        //show health stats
        if(isPlayer) {
            playerStats.displayHealthStats(health);
        }

        if(isSwine || isFoe) {
            if(enemyController.EnemyState == EnemyState.PATROL) {
                enemyController.chaseDistance = 50f;
            }
        }

        if(health <= 0f) {

            playerDied();

            isDead = true;
        }
    }

    void playerDied() {
        if(isPlayer) {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            //deactivate EnemyController script when we die
            for(int i = 0; i < enemies.Length; i++) {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }

            //stop spawning enemies
            EnemyManager.instance.stopSpawning();

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().getSelectedWeapon().gameObject.SetActive(false);
        }

        if(isSwine) {
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;

            enemyController.enabled = false;
            enemyAnimation.death();

            //play death audio
            StartCoroutine(deathSound()); //coroutine can pause execution and then continue where it left off

            //spawn more enemies
            EnemyManager.instance.EnemyDied(false);
        }

        if(isFoe) {
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;

            enemyController.enabled = false;
            enemyAnimation.enabled = false;
            navAgent.enabled = false;

            //play death audio
            StartCoroutine(deathSound());

            //spawn more enemies
            EnemyManager.instance.EnemyDied(true);
        }

        if(tag == "Player") {
            Invoke("restartGame", 3f);
        } else {
            Invoke("turnOffGameObject", 1.3f);
        }
    }

    void restartGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    void turnOffGameObject() {
        gameObject.SetActive(false);
    }

    IEnumerator deathSound() {
        yield return new WaitForSeconds(0.3f);
        enemySound.playDeathSound();
    }
}
