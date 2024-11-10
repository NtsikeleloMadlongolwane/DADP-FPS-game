using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public Transform BossFightPosition;
    public SealHealth sealHealth;
    public BossHealth bossHealth;
  
    private bool FightMode = false;

    [Header("SPIKE MOVE")]
    public SpikeWave2 spikeWave;

    [Header("Enemy Spawn Move")]

    public GameObject walkingEnemy;
    public GameObject EnemyBlackOrb;
    public GameObject BlackSplash;
    public Transform Spot1;
    public Transform Spot2;
    public Transform Spot3;
    public Transform Spot4;

    [Header("Target Pillars")]
    public GameObject player;
    public ParticleSystem CrystalFlame;
    public GameObject Pillar;


    [Header("Boss Death")]
    public ScreenShake screenShake;
    public ParticleSystem voidSplash;
    public ParticleSystem ctystalSplash;
    public GameObject bossMesh;
    public GameObject winScreen;

    public void Update()
    {
        if (sealHealth.sealisbroken == true)
        {
            MoveToFightPosition();
            FightMode = true;
        }
    }

    public void Start()
    {
        StartCoroutine(CycleMoves());
    }
    public void MoveToFightPosition()
    {

        transform.position = BossFightPosition.transform.position; // teleport to fight position
    }

    public void RandomMoveSelector()
    {
        int randomNumber = Random.Range(1, 10);

        if (randomNumber >= 1 && randomNumber<=3)
        {
            // move one
            Spikes();
            return;
        }
        else if(randomNumber >=4 && randomNumber <= 6)
        {
            //move two
            //StartCoroutine(CrystalPillar());
            return;
        }
        else
        {
            //move 3
            return;
        }

    
    }

   public void Spikes()
    {
        //crystalBeam.Play();
        StartCoroutine(spikeWave.LoopedWaveSequence());
       // crystalBeam.Stop();
    }

    public IEnumerator EnemySpawnMove()
    {
        //shake Boss
        GameObject blackSlapsh = Instantiate(BlackSplash, gameObject.transform.position, Quaternion.identity);

        // spawn black balls and move them to enemy positions

        GameObject[] BlackOrbs;
        BlackOrbs = new GameObject[4];


        BlackOrbs[0] = Instantiate(EnemyBlackOrb, Spot1.transform.position, Quaternion.identity);
        BlackOrbs[1] = Instantiate(EnemyBlackOrb, Spot2.transform.position, Quaternion.identity);
        BlackOrbs[2] = Instantiate(EnemyBlackOrb, Spot3.transform.position, Quaternion.identity);
        BlackOrbs[3] = Instantiate(EnemyBlackOrb, Spot4.transform.position, Quaternion.identity);

        //Move orbs to spots

        yield return new WaitForSeconds(5f);
        // Spawn enemies
        Instantiate(walkingEnemy, Spot1.transform.position, Quaternion.identity);
        Instantiate(walkingEnemy, Spot2.transform.position, Quaternion.identity);
        Instantiate(walkingEnemy, Spot3.transform.position, Quaternion.identity);
        Instantiate(walkingEnemy, Spot4.transform.position, Quaternion.identity);

        Destroy(BlackOrbs[0]);
        Destroy(BlackOrbs[1]);
        Destroy(BlackOrbs[2]);
        Destroy(BlackOrbs[3]);
      
        yield return new WaitForSeconds(5f);
        Destroy(blackSlapsh);
    }
    
    public IEnumerator TargetPillars()
    { 
            Vector3 spawnPoint = player.transform.position;
            ParticleSystem fire = Instantiate(CrystalFlame, spawnPoint, Quaternion.identity);
            yield return new WaitForSeconds(2f);
            GameObject pillar = Instantiate(Pillar, spawnPoint, Quaternion.identity);
            yield return new WaitForSeconds(3f);
            Destroy(pillar);
            fire.Stop();            
    }

    public IEnumerator PillarSequence()
    {
        for (int i = 0;i <10 ;i++)
        {
            yield return new WaitForSeconds(2f);
            StartCoroutine(TargetPillars());
        }
    }

    public  IEnumerator CycleMoves()
    {
        
            while (bossHealth.isBossAlive == true)
            {
                if(FightMode == true)
                {
                    int moveIndex = Random.Range(0, 3);
                    switch (moveIndex)
                    {
                        case 0:
                            Spikes();
                            break;
                        case 1:
                            yield return StartCoroutine(EnemySpawnMove());
                            break;
                        case 2:
                            yield return StartCoroutine(PillarSequence());
                         break;

                    }

                yield return new WaitForSeconds(5f);
                }
               
            }
            StartCoroutine(screenShake.Shake(10f, 0.5f));
            Instantiate(voidSplash, gameObject.transform.position, Quaternion.identity);
            Instantiate(ctystalSplash, gameObject.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(11f);
            bossMesh.SetActive(false);


            Time.timeScale = 0; //Pause game
                                // win screen    
                                //
            Debug.Log("Boss is dead!!");     
    }
}
