using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CensorMove2 : MonoBehaviour
{
    public PlayerMovement2 playerMovement;
    public bool check;
    public GameObject aGameObject;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "NextBox")
        {
            check = true;
            aGameObject = other.gameObject;
            playerMovement.targetTransform.y = other.gameObject.transform.position.y;
            playerMovement.isMoving = true;
        }
        else
        {
            check = false;
        }
    }
}
