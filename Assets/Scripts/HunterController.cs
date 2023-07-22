using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HunterController : MonoBehaviour
{
    private Animator anim;
    private GameObject target;
    private Collider[] hitColliders;
    private RaycastHit hit;
    private Rigidbody rb;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] float sightRange = 12;
    [SerializeField] float detectionRange = 7;
    [SerializeField] float speed = 4.0f;
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
            if(LookForPrey()) StartCoroutine(Chase());
            else 
            {
                Patrol();
            }
        } 
        else 
        {
            KeepChasing();
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

        else if(isWalking)
        {
            anim.SetBool("isWalking", true);
            var heading = destPoint - transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;

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
            walkTimeTolerance = 7;
            destPoint.x = -destPoint.x;
            destPoint.z = -destPoint.z;
        }

        walkTimeTolerance -= Time.deltaTime;
        delayToRestartWalking -= Time.deltaTime;
    }

    private bool LookForPrey()
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
        
        anim.SetBool("isRunning", false);
        return false;
    }

    private IEnumerator Chase()
    {
        var heading = target.transform.position - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;

        anim.SetBool("isRunning", true);


        Vector3 move = new Vector3(direction.x * speed, 0, direction.z * speed);
        transform.forward = move;
        
        yield return new WaitForSeconds(1.0f);
        rb.velocity = move;

        StartCoroutine(CheckPreySight());
    }

    private void KeepChasing()
    {
        var heading = target.transform.position - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;

        Vector3 move = new Vector3(direction.x * speed, 0, direction.z * speed);
        transform.forward = move;
        
        rb.velocity = move;

        StartCoroutine(CheckPreySight());
    }

    private IEnumerator CheckPreySight()
    {
        hitColliders = Physics.OverlapSphere(transform.position, sightRange);

        bool isPlayerHere = false;

        hitColliders = Physics.OverlapSphere(transform.position, sightRange);
        foreach(var possibleTarget in hitColliders)
        {
            if(possibleTarget.gameObject.tag == "Player") isPlayerHere = true;
        }

        Debug.Log(isPlayerHere);

        if(!isPlayerHere)
        {
            target = null;
            hasTarget = false;
            rb.velocity = new Vector3(0,0,0);
            anim.SetTrigger("defeated");
            yield return new WaitForSeconds(3.0f);
            anim.ResetTrigger("defeated");
        }
    }
}
