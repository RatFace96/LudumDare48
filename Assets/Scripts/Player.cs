using UnityEngine;

public class Player : MonoBehaviour
{
    public float rotationSpeed;
    public float speed;

    Vector2 direction;
    Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {             
        var mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);

        direction = _camera.ScreenToWorldPoint(mouseWorldPos) - transform.position;

        if(direction.sqrMagnitude < 0.1f)
        {
            return;
        }

        mouseWorldPos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, mouseWorldPos, Time.deltaTime);

        var neededRotation = -Vector2.SignedAngle(mouseWorldPos - transform.position, Vector2.up);
        transform.rotation = Quaternion.AngleAxis(neededRotation + 90, Vector3.forward);        
    }
}
