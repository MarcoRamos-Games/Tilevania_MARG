using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyMovement : MonoBehaviour
{

    //Cached References
    Rigidbody2D myRigidBody;

    //State
    bool isMoving;

    [Header("Config Stats")]
    [SerializeField] float moveSpeed = 1f;
    


    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        

    }

    // Update is called once per frame
    void Update()
    {

        ChangeDirections();
        

    }

    
    //Setters and Getters
    public void SetMoveSpeed(float newMoveSpeed)
    {
        moveSpeed = newMoveSpeed;
    }
    public void SetRigidBodyVelocity(Vector2 newVelocity)
    {
        myRigidBody.velocity = newVelocity;
    }

    private bool IsFAcingRight() //Check if the skeleton is facing right.
    {
        return transform.localScale.x > 0;
    }
    public void ChangeDirections()  //If is facing right make him move, if not, rotate him.
    {
        if (IsFAcingRight())
        {
            myRigidBody.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }
    
    

    private void OnTriggerExit2D(Collider2D player)//after exiting the trigger state stop the attacking animation and makehim move again, also
                                                   //manage the enemy rotation when colliding with walls
    {
        if (player.gameObject.tag == "Player Body" || player.gameObject.tag == "Player"  )
        {
            return;
        }

        isMoving = true;
            transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
        
    }

   






























    /*
    Rigidbody2D myRigidBody;
    BoxCollider2D myBoxCollider2D;
    
    [SerializeField] float moveSpeed = 1f;
    
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
   


    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
       

        
    }

    // Update is called once per frame
    void Update()
    {
     
            ChangeDirections();
       
    }

    public void ChangeDirections()
    {
        if (IsFAcingRight())
        {
            myRigidBody.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    private bool IsFAcingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            return;
        }
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
        
    }*/

 
}
