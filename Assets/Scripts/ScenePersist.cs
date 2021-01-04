using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{

    private int sceneIndex;
    private void Awake()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        ScenePersist[] persists = FindObjectsOfType<ScenePersist>();
        foreach (var persist in persists)
        {
            if (persist != this)
            {
                if (persist.sceneIndex == currentSceneIndex)
                {
                    Destroy(gameObject);
                    return;
                }
                else
                {
                    Destroy(persist.gameObject);
                }
            }
        }
        sceneIndex = currentSceneIndex;
        DontDestroyOnLoad(gameObject);
    }
}
    /*
    int startingSceneIndex;
    int newCurrentSceneIndex;
    private void Awake()
    {
        int numberOfScenePersist = FindObjectsOfType<ScenePersist>().Length;
        if (numberOfScenePersist > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        newCurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if(newCurrentSceneIndex != startingSceneIndex)
        {
            Destroy(gameObject);
        }
    }*/

