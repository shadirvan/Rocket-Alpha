using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]float mainThrust = 1000f;
    [SerializeField]float rotateThrust = 100f;
    AudioSource thrustAudio;
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

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotatePlayer(rotateThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotatePlayer(-rotateThrust);
        }
    }

    void RotatePlayer(float rotationSpeed)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        rb.freezeRotation = false;
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
            if(!thrustAudio.isPlaying){
                thrustAudio.Play();
            }
        }
        else
        {
            thrustAudio.Stop();
        }
    }
}
