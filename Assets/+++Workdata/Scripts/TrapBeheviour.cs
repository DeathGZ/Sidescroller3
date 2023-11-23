using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBeheviour : MonoBehaviour
{
    public GameObject freezeTrap;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) 
        {
            Debug.Log("playerFreeze");
            CharacterMovement cM = col.GetComponent<CharacterMovement>();
            col.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            StartCoroutine(Freeze(cM) );
        }
                              
    }

    IEnumerator Freeze(CharacterMovement _cM)
    {
        _cM.isFreezed = true;

        yield return new WaitForSeconds(3);

        _cM.isFreezed = false;

    }
        



}
