using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{  
        public GameObject slashAttackPrefab; // Prefab of the slash attack
        private Transform player;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(SpawnSlashAttack());

        }

    }

    IEnumerator SpawnSlashAttack()
        {
            yield return new WaitForSeconds(2f); // Wait for 2 seconds
            slashAttackPrefab.SetActive(true);

            yield return new WaitForSeconds(0.5f);
            slashAttackPrefab.SetActive(false);
        }
}
