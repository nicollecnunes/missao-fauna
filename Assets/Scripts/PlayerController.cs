using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // life and points
    private HealthAndPointsController hpController;

    //input fields
    private ThirdPersonInputAssets playerActionAsset;
    private InputAction move;

    //movement fields 
    private Rigidbody rb;
    private Vector3 forceDirection = Vector3.zero;

    [SerializeField]
    private float movementForce = 1f;

    [SerializeField]
    private float maxSpeed = 5f;

    [SerializeField]
    private Camera playerCamera;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        hpController = GetComponent<HealthAndPointsController>();
        playerActionAsset = new ThirdPersonInputAssets();
    }

    //Inscrever os eventos
    private void OnEnable()
    {
        move = playerActionAsset.Player.Move;
        playerActionAsset.Player.Enable();
    }

    //Desinscrever eventos
    private void OnDisabled()
    {
        playerActionAsset.Player.Disable();
    }

    private void FixedUpdate()
    {
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight() * movementForce;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward() * movementForce;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;

        if(horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;

        LookAt();
    }

    private Vector3 GetCameraRight()
    {
        Vector3 right = Camera.main.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private Vector3 GetCameraForward()
    {
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if(move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
        {
            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        else
            rb.angularVelocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if(hpController.points >= hpController.pointsToConvert)
            {
                hpController.ConvertHunter();
                Destroy(other.gameObject);
            }
            else 
            {
                hpController.TakeDamage(1);
                rb.AddForce(-transform.forward * 100f, ForceMode.Impulse);

                if(hpController.health == 0) Die();
            }
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Collectable")
        {
            hpController.AddPoint(1);
            Destroy(other.gameObject);
        }
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
