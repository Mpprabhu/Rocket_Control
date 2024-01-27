using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]float thrust=1000f, rotatespeed=100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem JetParticle, LeftShiftParticle,RightShiftParticle;
    
    Rigidbody rb;
    AudioSource audioSource; //caching process

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
                    audioSource.PlayOneShot(mainEngine);
                    JetParticle.Play();
                }
        }
        else
            {
                audioSource.Stop();
                JetParticle.Stop();
            }
    }
    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotatespeed);
            RightShiftParticle.Play();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotatespeed);
            LeftShiftParticle.Play();
        }
    }

    void ApplyRotation(float RotationValue)
    {
        rb.freezeRotation = true; //freezing physics for good rotation
        transform.Rotate(Vector3.forward * RotationValue * Time.deltaTime); //manual control for rotation
        rb.freezeRotation = false; //unfreezing after the completion of manual control
    }
}
