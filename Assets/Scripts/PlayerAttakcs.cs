using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttakcs : MonoBehaviour
{
    //Cached References
    Animator myAnimator;
    


    [Header("Config Stats")]
    [SerializeField] LayerMask enemyLayers;

    [Header("Attack 1 parameters")]
    [SerializeField] int attack1Damage = 100;
    [SerializeField] Transform attackPoint1;
    [SerializeField] float attackRange1 = 0.5f;
   

    [Header("Attack 2 parameters")]
    [SerializeField] int attack2Damage = 100;
    [SerializeField] Transform attackPoint2; 
    [SerializeField] float attackRange2 = 0.5f;


    //state
    int hasAttacked1;
    int hasAttacked2;



    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        Attack1();
        Attack2();
    }

  
    private void Attack1() // check if the player pressed the fire key, and if so make the atack animation
    {
       
        if (Input.GetButtonDown("Fire1"))
        {
           
            myAnimator.SetTrigger("Attack 1");

        }
    }

    private void Attack2()
    {

        if (Input.GetButtonDown("Fire2"))
        {
           
            myAnimator.SetTrigger("Attack 2");

        }
    }


    private void OnDrawGizmosSelected() //Draw a sphere on the enemy weapon, for utility purpose
    {
        if (!attackPoint1 || !attackPoint2 )
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint1.position, attackRange1);
        Gizmos.DrawWireSphere(attackPoint2.position, attackRange2);
    }
    public void DealDamage1() //create a circle that saves all the colliders it has collide with, if one of those colliders is the enemy body then reduce their health
    {

        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint1.position, attackRange1, enemyLayers);
        foreach (Collider2D enemy in hitEnemy)
        {
            if (enemy.gameObject.tag == "Enemy Body")
            {
                enemy.GetComponent<Enemy>().LooseHealth(attack1Damage);
            }
        }

    }
    public void DealDamage2() 
    {

        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint2.position, attackRange2, enemyLayers);
        foreach (Collider2D enemy in hitEnemy)
        {
            if (enemy.gameObject.tag == "Enemy Body")
            {
                enemy.GetComponent<Enemy>().LooseHealth(attack2Damage);
            }
            else
            {
                return;
            }
        }

    }

}
