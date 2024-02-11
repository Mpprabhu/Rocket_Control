using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 2f; 

    void Start()
    {

        startingPosition = transform.position;

    }
    void Update()
    {
        if(period <= Mathf.Epsilon) {return;}
        float cycles = Time.time / period; //for continuity of 5 cycles

        const float tau = Mathf.PI *2; // constant value for tau = 6.
        float rawSinWave = Mathf.Sin(cycles*tau); //mathf for angle in radians

        movementFactor = (rawSinWave + 1) /2; //to have range od 0 to 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
