using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    //cached references
    Player player;
    Animator playerAnimator;
    Rigidbody2D playerRigidbody2D;


    [SerializeField] float timeBeforeNextScene = 1f;
    

    private void Start()
    {
        player = FindObjectOfType<Player>();
        playerAnimator = player.GetComponent<Animator>();
        playerRigidbody2D = player.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        StartCoroutine(LoadNextScene());


    }

    IEnumerator LoadNextScene() // make the player stop moving during the load next scene animation, and take him to the next level
    {
        bool newIsAlive = false;
        playerRigidbody2D.velocity = new Vector2(0, 0);
        playerAnimator.SetTrigger("LoadNextLevel");
        FindObjectOfType<Player>().SetIsAlive(newIsAlive);
        player.transform.position = gameObject.transform.position;

        yield return new WaitForSeconds(timeBeforeNextScene);

        var currenSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currenSceneIndex + 1);
    }
}
