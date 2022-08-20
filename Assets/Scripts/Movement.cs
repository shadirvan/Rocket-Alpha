using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]float mainThrust = 1000f;
    [SerializeField]float rotateThrust = 100f;

    [SerializeField] AudioClip engineThrust;

    [SerializeField] ParticleSystem EngineThrusterParticles;
    [SerializeField] ParticleSystem LeftThrusterParticles;
    [SerializeField] ParticleSystem RightThrusterParticles;
    AudioSource thrustAudio;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        thrustAudio = GetComponent<AudioSource>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();

    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            StartRotatingLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            StartRotatingRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
        if (!thrustAudio.isPlaying)
        {
            thrustAudio.PlayOneShot(engineThrust);
        }
        if (!EngineThrusterParticles.isPlaying)
        {
            EngineThrusterParticles.Play();
        }
    }
    void StopThrusting()
    {
        thrustAudio.Stop();
        EngineThrusterParticles.Stop();
    }
    
    void StartRotatingLeft()
    {
        RotatePlayer(rotateThrust);
        if (!RightThrusterParticles.isPlaying)
        {
            RightThrusterParticles.Play();
        }
    }
    void StartRotatingRight()
    {
        RotatePlayer(-rotateThrust);
        if (!LeftThrusterParticles.isPlaying)
        {
            LeftThrusterParticles.Play();
        }
    }

    void StopRotating()
    {
        RightThrusterParticles.Stop();
        LeftThrusterParticles.Stop();
    }

    

    void RotatePlayer(float rotationSpeed)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        rb.freezeRotation = false;
    }
}
