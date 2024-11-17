using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkingEnemy : MonoBehaviour
{
    private Animator anim;

    [Header("Patroling")]
  //  public Transform[] patrolPoints;
    private NavMeshAgent agent;
    private Transform currentTarget;
    public Transform player;
    public float chaseDistance = 5f;
    


    void Start()
    {
        player = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
       // SetRandomPatrolPoint();

    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= chaseDistance)
        {
            agent.SetDestination(player.position);
            anim.Play("Walking Enemy");
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                //SetRandomPatrolPoint();
            }
            else
            {
                anim.Play("Idle");
            }
        }
    }

 /*   void SetRandomPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        int randomIndex = Random.Range(0, patrolPoints.Length);
        currentTarget = patrolPoints[randomIndex];
        agent.SetDestination(currentTarget.position);

    }*/

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
   
            Debug.Log("Enemy Made Contact");

            // Deal damage to the player
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
               playerHealth.TakeDamage(10); // Adjust the damage value as needed

            }

        }

        else if (collision.gameObject.CompareTag("Bullet"))
        {
            // Deal damage to the enemy
            EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(10); // Adjust the damage value as needed
            }

            // Destroy the bullet
            Destroy(collision.gameObject);
        }
    
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Deal damage to the enemy
            EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(10); // Adjust the damage value as needed
            }

            // Destroy the bullet
            Destroy(other.gameObject);
        }
    }
}
