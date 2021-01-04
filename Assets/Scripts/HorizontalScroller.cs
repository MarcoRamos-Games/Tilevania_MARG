using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalScroller : MonoBehaviour
{
    [Tooltip("Game Units per second")]
    [SerializeField] float scrollRate = 0.2f;

   
    // Update is called once per frame
    void Update()
    {
        float xMove = scrollRate * Time.deltaTime;
        transform.Translate(new Vector2(xMove, 0f));
    }
}
