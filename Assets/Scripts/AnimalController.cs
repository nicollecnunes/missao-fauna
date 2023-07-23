using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
     private Animator anim;
    private GameObject target;
    private Collider[] hitColliders;
    private RaycastHit hit;
    private Rigidbody rb;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] float sightRange = 7;
    [SerializeField] float detectionRange = 5;
    [SerializeField] float walkSpeed = 2.0f;
    [SerializeField] float runSpeed = 4.0f;
    [SerializeField] float range;

    Vector3 destPoint;
    Vector3 directionToGo;
    bool walkpointSet = false;
    bool isWalking = false;
    float walkTimeTolerance = 7;
    float delayToRestartWalking = 3;

    private bool hasTarget = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!hasTarget)
        {
            if(LookForHunter()) RunAway();
            else 
            {
                Patrol();
            }
        } 
        else 
        {
            RunAway();
        }
    }

    private void Patrol()
    {
        if(!walkpointSet && delayToRestartWalking <= 0)
        {
            float z = Random.Range(-range, range);
            float x = Random.Range(-range, range);

            destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
            walkpointSet = true;
            isWalking = true;
            walkTimeTolerance = 7;
        }

        if(isWalking)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isEscaping", false);
            var heading = destPoint - transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;
            float speed = walkSpeed;

            Vector3 move = new Vector3(direction.x * speed, 0, direction.z * speed);
            transform.forward = move;
            rb.velocity = move;
        }

        if(isWalking && Vector3.Distance(destPoint, transform.position) < 1) 
        {
            anim.SetBool("isWalking", false);
            isWalking = false; 
            walkpointSet = false;
            delayToRestartWalking = 3;
        }

        if(walkTimeTolerance <= 0)
        {
            walkpointSet = false;
        }

        walkTimeTolerance -= Time.deltaTime;
        delayToRestartWalking -= Time.deltaTime;
    }

    private bool LookForHunter()
    {
        hitColliders = Physics.OverlapSphere(transform.position, detectionRange);
        foreach(var possibleTarget in hitColliders)
        {
            if(possibleTarget.tag == "Player")
            {
                target = possibleTarget.gameObject;
                hasTarget = true;

                return true;
            }
        }
        
        anim.SetBool("isEscaping", false);
        return false;
    }

    private void RunAway()
    {
        var heading = (target.transform.position - transform.position);
        var distance = heading.magnitude;
        var direction = heading / distance;
        float speed = runSpeed;

        anim.SetBool("isEscaping", true);
        anim.SetBool("isWalking", false);

        Vector3 move = new Vector3(direction.x * speed, 0, direction.z * speed);
        transform.forward = -move;

        rb.velocity = -move;

        CheckHunterSight();
    }

    private void CheckHunterSight()
    {
        hitColliders = Physics.OverlapSphere(transform.position, sightRange);

        bool isPlayerHere = false;

        hitColliders = Physics.OverlapSphere(transform.position, sightRange);
        foreach(var possibleTarget in hitColliders)
        {
            if(possibleTarget.gameObject.tag == "Player") isPlayerHere = true;
        }

        if(!isPlayerHere)
        {
            target = null;
            hasTarget = false;
            rb.velocity = new Vector3(0,0,0);
        }
    }
}
