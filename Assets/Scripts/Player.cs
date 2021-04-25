using UnityEngine;

public class Player : MonoBehaviour
{
    public float RotationSpeed;
    public float Speed;

    public int eatPoints = 10;
    public int EatPoints 
    {
        get
        {
            return eatPoints;
        } 
        private set 
        {
            eatPoints = value;
            CalculateSpeed();
        }
    } 

    public float DistanceFollowPrecision = 0.1f;

    Vector2 direction;
    Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    float defaulttimerLostEat = 3f;
    float timerLostEat = 3f;

    private void Update()
    {
        if(timerLostEat > 0)
        {
            timerLostEat -= Time.deltaTime;
            return;
        }
        timerLostEat = defaulttimerLostEat;
        EatPoints--;
        
    }

    void CalculateSpeed()
    {
        Speed = EatPoints;
        Debug.Log(Speed);
    }

    private void FixedUpdate()
    {             
        var mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);

        direction = _camera.ScreenToWorldPoint(mouseWorldPos) - transform.position;

        if(direction.sqrMagnitude < DistanceFollowPrecision)
        {
            return;
        }

        mouseWorldPos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, mouseWorldPos, Time.deltaTime);

        var neededRotation = -Vector2.SignedAngle(mouseWorldPos - transform.position, Vector2.up);
        transform.rotation = Quaternion.AngleAxis(neededRotation + 90, Vector3.forward);        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var col = collision.GetComponent<ICollectable>();
        if (col != null) 
        {
            EatPoints++;            
            Destroy(collision.gameObject);
        }
    }
}

