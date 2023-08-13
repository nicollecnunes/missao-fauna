using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameTime timer;

    void Start()
    {
        timer.SetDuration(timer.time).Begin();    
    }
}
