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
    
    //This stuff is for testing with the players
    private GameObject[] players;
    private bool fp1, fp2, cp1, cp2;
    

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
        //ArrayList of players
        players = GameObject.FindGameObjectsWithTag("Player");
        float minPlayerDist = 10000;
        float tempDist;
        int minPlayer;
        for(int x = 0; x < players.Length; x++) {
            tempDist = Vector3.Distance(transform.position, players[x].transform.position);
            if (tempDist < minPlayerDist)
            {
                minPlayer = x;
                minPlayerDist = tempDist;
            }
        }

        //to make color change work with Array, make all green and then make the followed one yellow based on minplayer
        //follow minplayer

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
            //print(Quaternion.Inverse(_lookRotation) * transform.rotation);
            if (differenceOfRotation(_lookRotation, transform.rotation) > 0.1)
                transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
            else {

                targetX = Random.Range(targetX - 0.75f, targetX + 0.75f);
                targetY = Random.Range(targetY - 0.75f, targetY + 0.75f);
                targetZ = Random.Range(targetZ - 0.75f, targetZ + 0.75f);
                lookTransform = new Vector3(targetX, targetY, targetZ);
                _direction = (lookTransform - transform.position).normalized;
                _lookRotation = Quaternion.LookRotation(_direction);
            }

        }
        //Checking for players
        //int layerMask = 1 << 8;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 10, 1 << 8)){
            print("I can see the player");
        }

    }

    
}