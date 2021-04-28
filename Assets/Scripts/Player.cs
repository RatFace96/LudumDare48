using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action OnDead;

    public UIController UIController;

    public float RotationSpeed;


    public float MaxSpeedMouse;
    public float MaxEatPoints = 20;
    public float EatPoints = 10;    

    public float DistanceFollowPrecision = 0.1f;

    Vector2 direction;
    Camera _camera;

    public int ateCheeses = 0;

    public AudioSource EatCheeseSource;

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
    Vector3 mouseWorldPos;
    private void FixedUpdate()
    {
        // IsStart = true, when we pressed start button in StartMenu
        if (!UIController.IsStart) return;

        mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);

        direction = _camera.ScreenToWorldPoint(mouseWorldPos) - transform.position;

        if(direction.sqrMagnitude < DistanceFollowPrecision)
        {
            return;
        }

        if(EatPoints < MaxEatPoints)
        {
            speed = EatPoints * Time.deltaTime;
        }
        if(speed < 0 || EatPoints < 0)
        {
            speed = Time.deltaTime;
        }
        
        mouseWorldPos.z = transform.position.z;

        transform.position = Vector2.MoveTowards(transform.position, mouseWorldPos, speed);

        var neededRotation = -Vector2.SignedAngle(mouseWorldPos - transform.position, Vector2.up);
        transform.rotation = Quaternion.AngleAxis(neededRotation + 90, Vector3.forward);        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var col = collision.GetComponent<Cheese>();
        //TODO: Play "ate cheese" animation 
        if (col != null) 
        {
            EatCheeseSource.Play();
            EatPoints++;
            ateCheeses++;
            Destroy(collision.gameObject);
        }
    }

    private void OnDestroy()
    {
        OnDead?.Invoke();
    }
}


