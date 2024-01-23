using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField]float thrust=1000f,rotatespeed=100f;
    // [SerializeField]float rotatespeed = 100f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThruster();   
        ProcessRotation();     
    }
    void ProcessThruster()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up*thrust*Time.deltaTime);
            if(!audioSource.isPlaying) //isPlaying is a property not a function that to be declared
                {
                    audioSource.Play();
                }
        }
        else
            {
                audioSource.Stop();
            }
    }
    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotatespeed);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotatespeed);

        }
    }

    void ApplyRotation(float RotationValue)
    {
        rb.freezeRotation = true; //freezing physics for good rotation
        transform.Rotate(Vector3.forward * RotationValue * Time.deltaTime); //manual control for rotation
        rb.freezeRotation = false; //unfreezing after the completion of manual control
    }
}
