using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoint : MonoBehaviour
{
    public event Action<Transform> OnPlayerEnter;
    public event Action OnPlayerExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var col = collision.gameObject.GetComponent<Player>();
        if(col != null)
        {
            OnPlayerEnter?.Invoke(col.transform);
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {     
        var col = collision.gameObject.GetComponent<Player>();
        if (col != null)
        {
            OnPlayerExit?.Invoke();
        }
    }
}

