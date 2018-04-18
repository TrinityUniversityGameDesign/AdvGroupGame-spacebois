using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAI : MonoBehaviour {


    //Some public floats for searching
	public float speed; 
    public float radiusLook;
    public float rotationSpeed;
    //More stuff for looking around
    private float targetX;
    private float targetY;
    private float targetZ;
    private float time;
    private float randomTime;
    private bool startSniff;
    private bool hasPatrol; 
    private bool guardCrystal;
    private Vector3 targetTransform;
    private Vector3 lookTransform;
    private Vector3 dummyRotate;
    private Vector3 _direction;
    private Vector3 patrolPoint;
    private Transform dummyTransform;
    private Quaternion _lookRotation;

    public float killRadius;
    public enum EnemyState {Inactive,Search,Wander,Sniff,LowAlert};
	EnemyState curState;
    public int playerKill;
 	public Transform loc;
    public GameObject play;
    public Tuple<int,GameObject> ptup; 
    public List<GameObject> players;
    public List<Tuple<int,GameObject>> playerIDs;

    // colors!
    public Renderer cone;
    public SpriteRenderer eye;
    public GameObject idleParticles;
    public GameObject alertParticles;
    public Color idleColor = new Color(1f, 1f, 1f, 1f); // 41B881FF
    public Color alertColor = new Color(1f, 1f, 1f, 1f); // D41E56
    public float originDistance;

    void Start(){
        loc = null;
		curState = EnemyState.Inactive;
        Renderer rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Custom/FakeVolumetricLightShader");
    } 

	void Update(){
        if (GetComponent<PhotonView>().isMine)
        {
            float enemoriginDistance = Vector3.Distance(transform.position, Vector3.zero);
            if(enemoriginDistance < 25){
                transform.position =  new Vector3(transform.position.x+Random.Range(15f,20f), transform.position.y+Random.Range(15f,20f),transform.position.z+Random.Range(15f,20f));
            }

            switch (curState)
            {
                case EnemyState.Inactive:
                    //Should maybe consider not having to store the number of players? 
                    if (players.Count>0) { curState = EnemyState.Search; }
                    // else {  StartCoroutine(waitSearch());  }
                    //else { UpdateClosestPlayer(); }
                    break;
                case EnemyState.Search:
                    FindPlayer();
                    break;
                case EnemyState.LowAlert:
                    Patrol();
                    cone.material.SetColor("_MyColor", idleColor);
                    eye.color = idleColor;
                    idleParticles.SetActive(true);
                    alertParticles.SetActive(false);
                    break;
                case EnemyState.Sniff: // looking for players
                    originDistance = Vector3.Distance(loc.position, Vector3.zero);
                    if (originDistance < 12.5) // if theyre in the safe zone....
                    {
                        //dont get em
                        curState = EnemyState.Search;
                        break;
                    }
                    SniffPlayer();
                    cone.material.SetColor("_MyColor", idleColor);
                    eye.color = idleColor;
                    idleParticles.SetActive(true);
                    alertParticles.SetActive(false);
                    break;
                case EnemyState.Wander: // chasing a player
                    originDistance = Vector3.Distance(loc.position, Vector3.zero);
                    if (originDistance < 12.5) // if theyre in the safe zone....
                    {
                        //dont get em
                        curState = EnemyState.Search;
                        break;
                    }
                    else
                    {
                        //go get em
                        GoToLocation();
                        cone.material.SetColor("_MyColor", alertColor);
                        eye.color = alertColor;
                        idleParticles.SetActive(false);
                        alertParticles.SetActive(true);
                    }
                    break;
            }
        }
	}

    /*
    //could probably just change to an update in player instantiation?
    private IEnumerator waitSearch(){
        yield return new WaitForSecondsRealtime(3);
        players = GameObject.FindGameObjectsWithTag("Player");
    }
    */

    public void Patrol() {
        if (hasPatrol || guardCrystal)
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

                if (PhotonNetwork.isMasterClient)
                {
                    UpdateClosestPlayer();
                }
                targetX = Random.Range(patrolPoint.x - radiusLook, patrolPoint.x + radiusLook);
                targetY = Random.Range(patrolPoint.y - radiusLook, patrolPoint.y + radiusLook);
                targetZ = Random.Range(patrolPoint.z - radiusLook, patrolPoint.z + radiusLook);
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
                else
                {

                    targetX = Random.Range(targetX - 0.75f, targetX + 0.75f);
                    targetY = Random.Range(targetY - 0.75f, targetY + 0.75f);
                    targetZ = Random.Range(targetZ - 0.75f, targetZ + 0.75f);
                    lookTransform = new Vector3(targetX, targetY, targetZ);
                    _direction = (lookTransform - transform.position).normalized;
                    _lookRotation = Quaternion.LookRotation(_direction);
                }

            }
            if (Vector3.Distance(transform.position, loc.position) < 100f)
            {
                startSniff = false;
                curState = EnemyState.Sniff;
            }
        }
        else {
            patrolPoint = Random.insideUnitSphere * Random.Range(100f, 900f);
            time = 0;
            SetRandomTime();
            targetX = Random.Range(patrolPoint.x - radiusLook, patrolPoint.x + radiusLook);
            targetY = Random.Range(patrolPoint.y - radiusLook, patrolPoint.y + radiusLook);
            targetZ = Random.Range(patrolPoint.z - radiusLook, patrolPoint.z + radiusLook);
            targetTransform = new Vector3(targetX, targetY, targetZ);
            _direction = (targetTransform - transform.position).normalized;
            _lookRotation = Quaternion.LookRotation(_direction);
            hasPatrol = true;
        }
    }

	//Maybe have this take in the argument Transform loc?
	public void GoToLocation(){
        if (Vector3.Distance(transform.position,loc.position) > killRadius){	
            if (Vector3.Distance(transform.position, loc.position) > 100) // break aggro if over 100 units away
            {
                curState = EnemyState.Search;
            }
            originDistance = Vector3.Distance(loc.position, Vector3.zero);
            if (originDistance < 12.5) // if theyre in the safe zone....
                    {
                        //dont get em
                        curState = EnemyState.Search;
                    } 
            else
            {
                //Should this happen every call? 
                transform.LookAt(loc);
                //This definitely should be.
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, loc.position, step);
                //Debug.Log(transform.position);
            }
        }
		else{
			//Setting the state to Inactive, since we have killed our target. 
            //Debug.LogWarning("I. ENEMY CALLING KILL PLAYER");
            //playerKill = playerIDs[System.Array.IndexOf(players,loc.gameObject)];
            GameObject pl = loc.gameObject;
            pl.GetComponent<KillPlayerRemote>().killPlayer(playerKill);
            curState = EnemyState.Search;
            //Debug.LogWarning("II. ENEMY CALLED KILL PLAYER");
            //loc.gameObject; 
            //players.Remove(pl);


            //Might want to change from array to just have the removal of the dead player?
            /*
             * cmw adding game ending transition
             */
            /*
             *   bjo changing game ending transistion to occur in player
             */
            //Debug.LogWarning("ENEMY CALLING DEAD");
        }
	}

	public void FindPlayer(){
        UpdateClosestPlayer();
        //Setting the state to follow after;
        if(loc != null)
        {
            hasPatrol = false;
            curState = EnemyState.LowAlert;
        }
        else
        {
            curState = EnemyState.Inactive;
        }
    }

    public void UpdateClosestPlayer(){

        //GameObject[] players;
        //keeping call for network test
        //players = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject player in players)
        {
            Vector3 diff = player.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            //float originDistance = Vector3.Distance(player.transform.position, Vector3.zero);
            if ((curDistance < distance) && curDistance > 1)
            {
                closest = player;
                distance = curDistance;
            }
        }
            play = closest;
            loc = closest.transform;
            foreach (var tup in playerIDs)
            {
                if (tup.Second == play)
                {
                    playerKill = tup.First;
                }
            }

            
        
        //Setting the state to follow after; 
    }

    public void SniffPlayer()
    {
        if (startSniff == false)
        {
            time = 0;
            SetRandomTime();
            targetX = Random.Range(loc.position.x - radiusLook, loc.position.x + radiusLook);
            targetY = Random.Range(loc.position.y - radiusLook, loc.position.y + radiusLook);
            targetZ = Random.Range(loc.position.z - radiusLook, loc.position.z + radiusLook);
            targetTransform = new Vector3(targetX, targetY, targetZ);

            _direction = (targetTransform - transform.position).normalized;
            _lookRotation = Quaternion.LookRotation(_direction);
            startSniff = true;
        }
        else
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

                if(PhotonNetwork.isMasterClient){
                    UpdateClosestPlayer();
                }
                targetX = Random.Range(loc.position.x - radiusLook, loc.position.x + radiusLook);
                targetY = Random.Range(loc.position.y - radiusLook, loc.position.y + radiusLook);
                targetZ = Random.Range(loc.position.z - radiusLook, loc.position.z + radiusLook);
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
                else
                {

                    targetX = Random.Range(targetX - 0.75f, targetX + 0.75f);
                    targetY = Random.Range(targetY - 0.75f, targetY + 0.75f);
                    targetZ = Random.Range(targetZ - 0.75f, targetZ + 0.75f);
                    lookTransform = new Vector3(targetX, targetY, targetZ);
                    _direction = (lookTransform - transform.position).normalized;
                    _lookRotation = Quaternion.LookRotation(_direction);
                }

            }
            if (Vector3.Distance(transform.position, loc.position) < 25f || IsPlayerInVisionCone())
            {
                //Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 10, 1 << 8) || -- Maybe someday
                print("Enemy can see the player");
                curState = EnemyState.Wander;

            }
        }
    }

    public void UpdateState(){ 
        players = new List<GameObject>();
        curState = EnemyState.Inactive;    
    }

    public void SetPlayers(List<GameObject> ps, List<Tuple<int,GameObject>> pids){
        players = ps;
        playerIDs = pids;
    }

    //This is for setting a random time to wait before deciding to go to a new location
    void SetRandomTime()
    {
        randomTime = Random.Range(1, 5);
    }
    //Used to determine how far needed to turn
    float differenceOfRotation(Quaternion a, Quaternion b)
    {
        Quaternion diff = Quaternion.Inverse(a) * b;
        float sum = Mathf.Abs(diff.x) + Mathf.Abs(diff.y) + Mathf.Abs(diff.z);
        return sum;
    }

    bool IsPlayerInVisionCone()
    {
        float angle;
        Vector3 enemyForward = transform.TransformDirection(Vector3.forward);
        Vector3 playerDirection = (loc.position - transform.position).normalized;
        angle = Vector3.Angle(enemyForward, playerDirection);
        print("Angle is " + angle);
        if (angle > 20 || Vector3.Distance(loc.position, transform.position) > 70)
            return false;
        else
            return true;
    }


}



