using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotator : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, 0, 0.4f, Space.Self);
    }
}
