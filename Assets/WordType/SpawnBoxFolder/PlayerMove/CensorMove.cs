using System.Collections;
using UnityEngine;

public class CensorMove : MonoBehaviour
{
    public PlayerMovement playerMovement;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "NextBox")
        {
            playerMovement.targetTransform.x = other.gameObject.transform.position.x;
            playerMovement.isMoving = true;
        }
    }
}
