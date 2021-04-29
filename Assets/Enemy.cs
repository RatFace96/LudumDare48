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

    bool isAlert = false;
    private void StartChase(Transform target)
    { 
        if(!isAlert)
            myAnim.SetBool("isAlerted", true);
        this.target = target;
    }

    private void SetChaseTrue()
    {
        isAlert = true;
        chase = true;
        myAnim.SetBool("isChase", true);
    }

    private void SetChaseFalse()
    {
        isAlert = false;
        chase = false;
        myAnim.SetBool("isChase", false);
    }

    private void Update()
    {
        if(target)
        {
            direction = target.position - transform.position;
            
            if (direction.sqrMagnitude > 100) return;
            
            if (direction.sqrMagnitude < DistanceFollowPrecision)
            {
                return;
            }

            //looks on player
            var neededRotation = -Vector2.SignedAngle(target.position - transform.position, Vector2.up);
            transform.rotation = Quaternion.AngleAxis(neededRotation - 90, Vector3.forward);
        }
    }

    private void FixedUpdate()
    {
        if (chase && target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
        }
    }

    private void StopChase()
    {
        SetChaseFalse();
        myAnim.SetBool("isAlerted", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var col = collision.gameObject.GetComponent<Enemy>();
        if (col == null)
        {
            var player = collision.gameObject.GetComponent<Player>();
            if (player == null)
                Destroy(collision.gameObject);
            else
            {
                Destroy(collision.gameObject, UIController.EndGameDelay);
            }
        }
    }

    private void OnDestroy()
    {
        MyPoint.OnPlayerEnter -= StartChase;
        MyPoint.OnPlayerExit -= StopChase;
    }
}

