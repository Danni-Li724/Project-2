using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgroHitbox : MonoBehaviour
{
    public bool playerDetected;
    public Collider2D player;
    public void OnCollisionEnter2D(Collision2D player)
    {
        playerDetected = true;
    }
    public void OnCollisionExit2D(Collision2D player)
    {
        playerDetected = false;
    }
}
