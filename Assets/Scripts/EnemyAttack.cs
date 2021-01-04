using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //Cached References
    Animator myAnimator;
    EnemyMovement myEnemyMovement;


    [Header("Config Stats")]
    [SerializeField] int damage = 100; // hits 2 times

    [Header("Atacking Parameters")]
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask playerLayers;
    [SerializeField] GameObject skeletonMain;
    



    // Start is called before the first frame update
    void Start()
    {

        myEnemyMovement = skeletonMain.GetComponent<EnemyMovement>();
        myAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame

    private void SetNewMoveSpeed(float newMoveSpeed)
    {
       myEnemyMovement.SetMoveSpeed(newMoveSpeed);
        
    }

    private void Update()
    {
        LookForPlayerHealth();
    }


    private void Attack(Collider2D other)//check if the enemy is colliding with the player, and if so, start the attacking animation and stop the movement
    {
        if (other.gameObject.tag == "Player")
        {
            myEnemyMovement.SetRigidBodyVelocity(new Vector2(0f, 0f));
            LookForPlayerHealth();
            myAnimator.SetBool("isAttacking", true);

        }
        else
        {
            return;

        }
    }
    private void OnTriggerEnter2D(Collider2D player)//make him attack on trigger
    {
        Attack(player);
        
        
    }

    private void OnTriggerExit2D(Collider2D player)//after exiting the trigger state stop the attacking animation and make the enemy move again, also
                                                   //manage the enemy rotation when colliding with walls
    {
        if (player.gameObject.tag == "Player")
        {
            myAnimator.SetBool("isAttacking", false);
            FindObjectOfType<EnemyMovement>().ChangeDirections();
        }
        else
        {
            return;
        }
    }

    private void OnDrawGizmosSelected() //Draw a sphere on the enemy weapon, for utility purpose
    {
        if (!attackPoint)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    public void DealDamgae() //if the attack succed, reduce player health
    {

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        foreach (Collider2D player in hitPlayer)
        {
            if(player.gameObject.tag == "Player")
            {
                player.GetComponent<Player>().LooseHealth(damage);
            }
            
            
        }

    }

    private void LookForPlayerHealth() // if the player is dead, do victory dance :v
    {
        float playerHealth = FindObjectOfType<Player>().GetHealth();
        if (playerHealth <= 0)
        {
            myAnimator.SetBool("isAttacking", false);
            myEnemyMovement.SetRigidBodyVelocity(new Vector2(0f, 0f));
            myAnimator.SetTrigger("Win");
        }
    }
}

