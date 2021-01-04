using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CointPickup : MonoBehaviour
{
    [SerializeField] int pickupValue;
    [SerializeField] AudioClip skullPickupSFX;
    bool isCollected;
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.GetComponent<Player>())
        {
            isCollected = true; 
            {
                if (isCollected) // check if the coin is collected
                {
                    GameObject audioListener = GameObject.FindWithTag("AudioListener"); // find the audiolistener so that the audio wont be heard only on left earphone
                    AudioSource.PlayClipAtPoint(skullPickupSFX, Camera.main.transform.position); // play the sfx
                    FindObjectOfType<GameSession>().AddToScore(pickupValue); //adds the value of the coin to the player score
                    Destroy(gameObject); //destroys it
                }
                else
                {
                    return;
                }
           }
           
        }
    }
}
