using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointRotator : MonoBehaviour
{
    [SerializeField] private float xRotate = 0;
    [SerializeField] private float yRotate = 0.4f;
    [SerializeField] private float zRotate = 0;

    void Update()
    {
        transform.Rotate(xRotate, yRotate, zRotate, Space.Self);
    }
}
