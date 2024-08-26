using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
   
    public float distance = 5.0f; // Adjust the distance to move
    public float speed = 2.0f;
    private Vector2 startPosition;
    private bool movingRight = true;

    void Start()
    {
        startPosition = transform.localPosition;
    }

    void Update()
    {
        if (movingRight)
        {
           // Debug.Log("up");
            transform.Translate(Vector2.up * speed * Time.deltaTime);

            if (transform.localPosition.x >= startPosition.x + distance)
            {
                movingRight = false;
            }
        }
        else
        {
            //Debug.Log("down");
            transform.Translate(Vector2.down * speed * Time.deltaTime);

            if (transform.localPosition.x <= startPosition.x - distance)
            {
                movingRight = true;
            }
        }
    }

}
