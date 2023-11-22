using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBehaviour : MonoBehaviour
{
    public GameObject deathTrap;

    private void OnTriggerEnter2D(Collider2D other)
    { 
     if (other.CompareTag("Player"))
        {                 
        other.gameObject.GetComponent<SpriteRenderer>().color = Color.red;  
        }
    if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<SpriteRenderer>().color = Color.red;

            if (other.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }

        }

    }
}


