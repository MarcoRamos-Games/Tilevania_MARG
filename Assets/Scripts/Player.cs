using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;
    [SerializeField] float movementSpeed = 3f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float playerDeathTime = 3f;
    
    //State
    bool isAlive = true;

    //Cached references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    PolygonCollider2D myPolligonCollider2D;
    [SerializeField] GameObject playerBody;
    [SerializeField] HealthBar healthBar;
       
    float startingGravity;


    // Start is called before the first frame update

  

    
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myPolligonCollider2D = playerBody.GetComponent<PolygonCollider2D>();
        startingGravity = myRigidBody.gravityScale;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        

    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
        {
            return;
            
        }
        PlayerMovement();
        FlipSprite();
        Jump();
        ClimbChain();
        Die();
        
    }

    //Setters and getters
    public float GetHealth()
    {
        return currentHealth;
    }

    public void SetIsAlive(bool newIsAlive)
    {
        isAlive = newIsAlive;
    }
   

    public Vector2 GetPlayerPosition()
    {
        Vector2 CurrentPlayerPosition = transform.position;
        return CurrentPlayerPosition;
    }

    public void LooseHealth(int damage) //reduce player health and start the hurt animation
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        myAnimator.SetTrigger("Hurt");
    }

    private void PlayerMovement()
    {
        float movement = Input.GetAxisRaw("Horizontal");
        Vector2 playerVelocity = new Vector2((movement * movementSpeed), myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
      
    }

    private void Jump()
    {
        
        if (!myPolligonCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            
            return;
        }
        

        if (Input.GetButtonDown("Jump"))
        {
            
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
            myAnimator.SetTrigger("Jump");
            
           
            
        }
    }

    private void Die()
    {
        if (currentHealth <= 0)
        {
            StartCoroutine(PlayerDeath());
  
        }
    
    }

    IEnumerator PlayerDeath()
    {
        myRigidBody.velocity = new Vector2(0f, 0f);
       isAlive = false;
        myAnimator.SetTrigger("Die");
        yield return new WaitForSeconds(playerDeathTime);
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }

    private void OnTriggerEnter2D(Collider2D spikes)
    {
        if(spikes.gameObject.tag == "Spikes")
        {
            int spikesDamage = spikes.GetComponent<Hazard>().GetHazardDamage();
            LooseHealth(spikesDamage);
        }
        else
        {
            return;
        }
    }
    private void ClimbChain()
    {
        if (!myPolligonCollider2D.IsTouchingLayers(LayerMask.GetMask("Chain")))
        {
            myAnimator.SetBool("isClimbing", false);
            myRigidBody.gravityScale = startingGravity;
            return;
        }
        
            float climb = Input.GetAxisRaw("Vertical");
            Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, climb * climbSpeed);
            myRigidBody.velocity = climbVelocity;
            bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
            myAnimator.SetBool("isClimbing", true);
            myRigidBody.gravityScale = 0;
    }

   


    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x),1f);
        }
    }  
    
  
     
}
