using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public Vector3 targetTransform;
    public float speed;
    public bool isMoving;
    
    //Censor
    [SerializeField] CircleCollider2D circleCollider;

    void Update()
    {
        if (isMoving)
        {
            circleCollider.enabled = false;
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), 
                                                            new Vector3(transform.position.x, targetTransform.y, transform.position.z), speed * Time.deltaTime);
        }

        if (transform.position.y >= targetTransform.y)
        {
            circleCollider.enabled = true;
            isMoving = false;
        }
    }
}
