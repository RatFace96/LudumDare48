using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action OnDead;

    public UIController UIController;

    public float RotationSpeed;


    public float MaxEatPoints = 20;
    public float EatPoints = 10;    

    public float DistanceFollowPrecision = 0.1f;

    Vector2 direction;
    Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    float timerLostEat = 3f;

    private void Update()
    {
        if (!UIController.IsStart) return;
        
        EatPoints -= Time.deltaTime / timerLostEat;
    }

    float speed;
    private void FixedUpdate()
    {
        // IsStart = true, when we pressed start button in StartMenu
        if (!UIController.IsStart) return;

        var mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);

        direction = _camera.ScreenToWorldPoint(mouseWorldPos) - transform.position;

        if(direction.sqrMagnitude < DistanceFollowPrecision)
        {
            return;
        }

        //correct player speed.
        if(EatPoints > 10)
        {
            speed = EatPoints * Time.deltaTime / 5;
        }
        else if(EatPoints > 5)
        {
            speed = EatPoints * Time.deltaTime / 3;
        }
        else
        {
            speed = EatPoints * Time.deltaTime / 2f;
        }

        mouseWorldPos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, mouseWorldPos, speed);

        var neededRotation = -Vector2.SignedAngle(mouseWorldPos - transform.position, Vector2.up);
        transform.rotation = Quaternion.AngleAxis(neededRotation + 90, Vector3.forward);        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var col = collision.GetComponent<ICollectable>();
        //TODO: Play "ate cheese" animation 
        if (col != null) 
        {
            EatPoints++;            
            Destroy(collision.gameObject);
        }
    }

    private void OnDestroy()
    {
        OnDead?.Invoke();
    }
}


