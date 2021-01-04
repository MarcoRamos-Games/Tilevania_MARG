using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //cashed references
    Animator myAnimator;
    Rigidbody2D myRigidBody2D;
    //state
    [Header("Config Stats")]
    [SerializeField] int health;
    [SerializeField] float enemyDeathTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = FindObjectOfType<EnemyAttack>().GetComponent<Animator>();
        myRigidBody2D = FindObjectOfType<EnemyAttack>().GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Die();
    }
    public void LooseHealth(int damage) //reduce enemy health 
    {
        health -= damage;
        myAnimator.SetTrigger("Hurt");
    }

    private void Die()
    {
        if (health <= 0)
        {
            StartCoroutine(EnemyDeath());

        }

    }

    IEnumerator EnemyDeath() //start death animation, waith for the death time and then destroy the enemy
    {
        
        myAnimator.SetTrigger("Die");
        yield return new WaitForSeconds(enemyDeathTime);
        Destroy(gameObject);
    }
}
