using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossMovements : MonoBehaviour
{
    [Header("Before Figths Starts")]
    public GameObject seal;
    public Transform ArenaPosition;
    public bool FightMode = false;

    [Header("MOVES")]
    [Space(5)]
    [Header("ENEMY SAPWN MOVE")]
    public bool enemySapwnMove = true;
    public GameObject WalkingEnemyPrefab;
    public GameObject EnemyOrbs;
    public Transform[] SpawnPoints;
    public ParticleSystem Splash;

    [Header("SPIKES MOVES")]
    public Transform[] SpikeSpots;
    public GameObject SpikeParticles;
    public GameObject Spike_Prefab;

    public ParticleSystem GlowingGenStone;
   public void Update()
    {
        if (seal == null)
        {
            gameObject.transform.position = ArenaPosition.transform.position;
            FightMode = true;
        }
    }

    private void Start()
    {
        Splash.Stop();
        GlowingGenStone.Stop();
        StartCoroutine(SpikeMove());
    }
    public IEnumerator EnemySpawnMove()
    {
        Splash.Play();
        // spawn black balls and move them to enemy positions

        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            GameObject Orb = Instantiate(EnemyOrbs, gameObject.transform.position, Quaternion.identity);
            StartCoroutine(MoveOrbToPosition(Orb, SpawnPoints[i].position, 5f)); // Adjust speed as needed
        }

        yield return new WaitForSeconds(3);

        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            GameObject enemy = Instantiate(WalkingEnemyPrefab , SpawnPoints[i].position, SpawnPoints[1].rotation);
        }
    }
    private IEnumerator MoveOrbToPosition(GameObject orb, Vector3 targetPosition, float speed)
    {
        while (orb != null && Vector3.Distance(orb.transform.position, targetPosition) > 0.01f)
        {
            orb.transform.position = Vector3.MoveTowards(orb.transform.position, targetPosition, speed * Time.deltaTime);
            yield return null; // Wait for the next frame } }

        }

        if (orb != null)
        {
            Destroy(orb);
            Splash.Stop();
        }
    }

    public IEnumerator SpikeMove()
    {
        GlowingGenStone.Play();
        yield return new WaitForSeconds(0.5f);
        GameObject spike;
        GameObject effects;
        for (int i = 0;i < SpikeSpots.Length; i++)
        {
            spike = Instantiate(Spike_Prefab, SpikeSpots[i].transform.position, Quaternion.identity);
            effects = Instantiate(SpikeParticles, SpikeSpots[i].transform.position, Quaternion.identity);
           Destroy(spike, 7.5f);
           Destroy(effects, 7.5f);
        } 
        yield return new WaitForSeconds(8f);
        GlowingGenStone.Stop();
    }
}
