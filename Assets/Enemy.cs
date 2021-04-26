using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyPoint MyPoint;
    public float DistanceFollowPrecision = 0.1f;
    public float Speed;

    Transform target;
    Vector2 direction;

    bool chase;

    private void Awake()
    {
        MyPoint.OnPlayerEnter += StartChaseTarget;
        MyPoint.OnPlayerExit += StopChase;
        target = FindObjectOfType<Player>().transform;
    }

    private void StartChaseTarget(Transform target)
    {
        Debug.Log("chase");
        this.target = target;
        chase = true;
    }

    private void Update()
    {
        if(target != null)
        {
            direction = target.position - transform.position;
            
            if (direction.sqrMagnitude < DistanceFollowPrecision)
            {
                return;
            }

            var neededRotation = -Vector2.SignedAngle(target.position - transform.position, Vector2.up);
            transform.rotation = Quaternion.AngleAxis(neededRotation + 90, Vector3.forward);

            if (chase)
            {
                transform.position = Vector3.Lerp(transform.position, target.position, Speed * Time.deltaTime / 3);
            }
        }
    }

    private void StopChase()
    {
        Debug.Log("stop chase");
        chase = false;
    }

    private void OnCollisionEnter2D(Collision collision)
    {
        Destroy(collision.gameObject);
    }

    private void OnDestroy()
    {
        MyPoint.OnPlayerEnter -= StartChaseTarget;
        MyPoint.OnPlayerExit -= StopChase;
    }
}

