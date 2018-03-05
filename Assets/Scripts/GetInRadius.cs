using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInRadius : MonoBehaviour
{

    private float targetX;
    private float targetY;
    private float targetZ;
    private Vector3 targetTransform;
    private Vector3 lookTransform;

    public float radius;
    public float speed;
    private float time;
    private float randomTime;
    public float rotationSpeed;

    private Vector3 dummyRotate;
    private Transform dummyTransform;
    private Quaternion _lookRotation;
    private Vector3 _direction;

    public GameObject targetPlayer;

    void SetRandomTime()
    {
        randomTime = Random.Range(1, 5);
    }

    float differenceOfRotation(Quaternion a, Quaternion b)
    {
        Quaternion diff = Quaternion.Inverse(a) * b;
        float sum = Mathf.Abs(diff.x) + Mathf.Abs(diff.y) + Mathf.Abs(diff.z);
        return sum;
    }

void Start()
    {
        //rotationSpeed = 1.75f;
        time = 0;
        SetRandomTime();
        targetX = Random.Range(targetPlayer.transform.position.x - radius, targetPlayer.transform.position.x + radius);
        targetY = Random.Range(targetPlayer.transform.position.y - radius, targetPlayer.transform.position.y + radius);
        targetZ = Random.Range(targetPlayer.transform.position.z - radius, targetPlayer.transform.position.z + radius);
        targetTransform = new Vector3(targetX, targetY, targetZ);

        _direction = (targetTransform - transform.position).normalized;
        _lookRotation = Quaternion.LookRotation(_direction);

    }

    // Update is called once per frame
    void Update()
    {


        float step = speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetTransform) < 2.5)
        {
            time += Time.deltaTime;
        }
        if (time > randomTime)
        {
            
            time = 0;
            SetRandomTime();
            targetX = Random.Range(targetPlayer.transform.position.x - radius, targetPlayer.transform.position.x + radius);
            targetY = Random.Range(targetPlayer.transform.position.y - radius, targetPlayer.transform.position.y + radius);
            targetZ = Random.Range(targetPlayer.transform.position.z - radius, targetPlayer.transform.position.z + radius);
            targetTransform = new Vector3(targetX, targetY, targetZ);

            _direction = (targetTransform - transform.position).normalized;
            _lookRotation = Quaternion.LookRotation(_direction);

            //transform.LookAt(targetTransform);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetTransform, step);
            print(Quaternion.Inverse(_lookRotation) * transform.rotation);
            if (differenceOfRotation(_lookRotation, transform.rotation) > 0.2)
                transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
            else {

                targetX = Random.Range(targetX - 1, targetX + 1);
                targetY = Random.Range(targetY - 1, targetY + 1);
                targetZ = Random.Range(targetZ - 1, targetZ + 1);
                lookTransform = new Vector3(targetX, targetY, targetZ);
                _direction = (lookTransform - transform.position).normalized;
                _lookRotation = Quaternion.LookRotation(_direction);
            }

        }
    }

    
}