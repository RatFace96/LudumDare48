using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchArrow : MonoBehaviour
{
    public List<Cheese> AroundCheese = new List<Cheese>();

    public float MaxSearchDist;

    Transform item;

    Coroutine search;

    CircleCollider2D collider2D;

    private void Start()
    {
        collider2D = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if(collider2D.radius < MaxSearchDist)
        {
            collider2D.radius++;
        }
        if (item)
        {
            var neededRotation = -Vector2.SignedAngle(item.position - transform.position, Vector2.up);
            transform.rotation = Quaternion.AngleAxis(neededRotation, Vector3.forward);
        }
    }

    IEnumerator Searching()
    {
        item = AroundCheese[0].transform;

        while (AroundCheese.Count > 0)                    
        {
            for(var i = 0; i < AroundCheese.Count; i++)
            {
                if(item == null)
                {
                    item = AroundCheese[0].transform;
                }
                if((transform.position - AroundCheese[i].transform.position).sqrMagnitude < (transform.position - item.position).sqrMagnitude)
                {
                    item = AroundCheese[i].transform;
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    void DelCheese(Cheese cheese)
    {
        AroundCheese.Remove(cheese);
        if (search != null) StopCoroutine(search);
        search = StartCoroutine(Searching());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var cheese = collision.GetComponent<Cheese>();

        if (cheese)
        {
            if (!AroundCheese.Contains(cheese))
            {
                AroundCheese.Add(cheese);
                cheese.OnCollected += DelCheese; 
                if (search == null)
                {
                    search = StartCoroutine(Searching());
                }
            }
        }        
    }

    private void OnDestroy()
    {
        foreach(var ch in AroundCheese)
        {
            ch.OnCollected -= DelCheese; 
        }
    }
}
