using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HunterPatrol : MonoBehaviour
{
  public float moveSpeed = 20f;
    public float rotationSpeed = 20f;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(!isWandering) StartCoroutine(Wander());

        if(isRotatingRight) transform.Rotate(transform.up * Time.fixedDeltaTime * rotationSpeed);
        if(isRotatingLeft) transform.Rotate(transform.up * Time.fixedDeltaTime * -rotationSpeed);

        if(isWalking) rb.AddForce(transform.forward * moveSpeed);
    }

    private IEnumerator Wander()
    {
        int rotationTime = Random.Range(1,3);
        int rotateWait = Random.Range(1,3);
        int rotateDirection = Random.Range(0,2);
        int walkWait = Random.Range(1,3);
        int walkTime = Random.Range(3,6);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);

        isWalking = true;

        yield return new WaitForSeconds(walkTime);

        isWalking = false;

        yield return new WaitForSeconds(rotateWait);

        if(rotateDirection == 1)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotateWait);
            isRotatingLeft = false;
        } 
        else
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotateWait);
            isRotatingRight = false;  
        }

        isWandering = false;
    }
}
