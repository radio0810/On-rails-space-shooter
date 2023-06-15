using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
     GameObject parentGameObject;
    ScoreBoard scoreBoard;
    [SerializeField] int score = 15;
    [SerializeField] int hitPoints = 2;

    private void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigibody();

    }

    private void AddRigibody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (hitPoints > 0)
        {
            ProcessHit();
        }
        else if (hitPoints <= 0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;

        Destroy(this.gameObject);
    }

    private void ProcessHit()
    {
        GameObject hitProcessVFX = Instantiate(hitVFX, transform.position, Quaternion.identity);
        hitVFX.transform.parent = parentGameObject.transform;
        scoreBoard.IncreaseScore(score);
        hitPoints -= 1;
    }
}
