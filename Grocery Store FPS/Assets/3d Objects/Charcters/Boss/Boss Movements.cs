using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;

public class BossMovements : MonoBehaviour
{
    [Header("BOSS HEALTH")]
    public int maxHealth = 500;
    private int currentHealth;
    public GameObject splash;

    [Header("Before Figths Starts")]
    public bool hasFightStarted = false;

    public ScreenShake2 screenShake;
    public GameObject seal;
    public Transform ArenaPosition;
    public GameObject Roof;
    public bool FightMode = false;
    public ParticleSystem TelePortSplash;
    public GameObject playerCamera;

    [Header("MOVES")]
    public bool isMoveRunning = false;
    [Space(5)]
    [Header("ENEMY SAPWN MOVE")]
    public bool enemySapwnMove = true;
    public GameObject WalkingEnemyPrefab;
    public GameObject EnemyOrbs;
    public Transform[] SpawnPoints;
    public ParticleSystem Splash;

    [Header("SPIKES MOVE")]
    public Transform[] SpikeSpots;
    public GameObject SpikeParticles;
    public GameObject Spike_Prefab;

    public ParticleSystem GlowingGenStone;

    [Header("TARGETING FLAME PILLARS")]
    public GameObject player;
    public ParticleSystem CrystalFlame;
    public GameObject Pillar;

    public ParticleSystem crystalBeam;
    public ParticleSystem crystalFlame;


    public void Update()
    {
        if (seal == null && !hasFightStarted)
        {
            StartCoroutine(FightStart());
            hasFightStarted = true;
        }
    }

    private void Start()
    {
        // stop particles
        Splash.Stop();
        GlowingGenStone.Stop();
        crystalBeam.Stop();
        crystalFlame.Stop();
        TelePortSplash.Stop();

        // cam
        screenShake = playerCamera.GetComponent<ScreenShake2>();
        // move
        StartCoroutine(CheckFightMode());

        //health
        currentHealth = maxHealth;
        Debug.Log("Enemy health: " + currentHealth);
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


    public IEnumerator TargetPillars()
    {
        //Vector3 spawnPoint = player.transform.position;
        Vector3 spawnPoint = new Vector3(player.transform.position.x, 29.824f, player.transform.position.z);
        ParticleSystem fire = Instantiate(CrystalFlame, spawnPoint, Quaternion.identity);
        Destroy(fire.gameObject, 5f);
        yield return new WaitForSeconds(1f);
        GameObject pillar = Instantiate(Pillar, spawnPoint, Quaternion.identity);
        yield return new WaitForSeconds(3f);
        Destroy(pillar);
        fire.Stop();
    }

    public IEnumerator PillarSequence()
    {
        crystalBeam.Play();
        crystalFlame.Play();

        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(TargetPillars());
        }

        yield return new WaitForSeconds(5); // space to shoot boos
        crystalBeam.Stop();
        crystalFlame.Stop();
    }

    public IEnumerator RandomMove()
    {
        while (FightMode)
        {
            if (!isMoveRunning)
            {
                isMoveRunning = true;
                int randomMove = Random.Range(0, 3);

                switch(randomMove)
                {
                    case 0:
                        yield return StartCoroutine(EnemySpawnMove());
                        break;
                    case 1:
                        yield return StartCoroutine(SpikeMove());
                        break;
                    case 2:
                        yield return StartCoroutine(PillarSequence());
                        break;
                }

                isMoveRunning=false;
            }

            yield return new WaitForSeconds(3f);
        }
    }

    public IEnumerator CheckFightMode()
    {
        while (true)
        {
            if(FightMode && !isMoveRunning)
            {
                StartCoroutine(RandomMove());
            }
            yield return null;
        }
    }

    public IEnumerator FightStart()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(screenShake.Shake(10f, 1f));
        yield return new WaitForSeconds(5f);
        Roof.SetActive(false);
        yield return new WaitForSeconds(2f);
        TelePortSplash.Play();
        gameObject.transform.position = ArenaPosition.transform.position;
        TelePortSplash.Play();
        yield return new WaitForSeconds(3f);
        FightMode = true;
    }

    public IEnumerator BossDeath()
    {
        // Stop fighting
        FightMode=false;
        TelePortSplash.Play();
        yield return new WaitForSeconds(1f);

        // shake
        StartCoroutine(screenShake.Shake(10f, 1f));
        Splash.Play();
        yield return new WaitForSeconds(10f);
        // explostion
        Instantiate(TelePortSplash, gameObject.transform.position, Quaternion.identity);
        Splash.Stop();
        Destroy(gameObject);
          
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy health: " + currentHealth);
        if (currentHealth <= 0)
        {
            // Enemy dies
            Debug.Log("Enemy is dead!");
            GameObject EnemySplash = Instantiate(splash, transform.position, Quaternion.identity);
            StartCoroutine(BossDeath());
        }
    }
}
