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
    Animator myAnim;

    private void Awake()
    {
        MyPoint.OnPlayerEnter += StartChase;
        MyPoint.OnPlayerExit += StopChase;
        target = FindObjectOfType<Player>().transform;
        myAnim = GetComponent<Animator>();
    }

    private void StartChase(Transform target)
    { 
        //TODO: Trigger alert animation and when animation is over set "catch = true"
        myAnim.SetBool("isAlerted", true);
        this.target = target;
        //SetChase(true);
    }

    private void SetChaseTrue()
    {
        chase = true;
        myAnim.SetBool("isChase", true);
    }

    private void SetChaseFalse()
    {
        chase = false;
        myAnim.SetBool("isChase", false);
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

            //looks on player
            var neededRotation = -Vector2.SignedAngle(target.position - transform.position, Vector2.up);
            transform.rotation = Quaternion.AngleAxis(neededRotation + 90, Vector3.forward);

            //when chase = true, then enemy start a chase player.
            if (chase)
            {
                transform.position = Vector3.Lerp(transform.position, target.position, Speed * Time.deltaTime / 3);
            }
        }
    }

    private void StopChase()
    {
        //TODO: Set Idle Animation     
        SetChaseFalse();
        myAnim.SetBool("isAlerted", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy all what we collide
        //TODO: At first play "death" animation of Player. It can be trans to Player.cs if it need
        Destroy(collision.gameObject);        
    }

    private void OnDestroy()
    {
        MyPoint.OnPlayerEnter -= StartChase;
        MyPoint.OnPlayerExit -= StopChase;
    }
}

