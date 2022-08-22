using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField]float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon ){return;} //Here we epsilon because it is the smallest float in mathf and comapring a float to zero is not good.
        float cycles = Time.time / period; // continually growing over time.

        const float tau = Mathf.PI *2; // constan value of 6.28
        float rawSineWave = Mathf.Sin(cycles * tau); // going from -1 to 1.
        movementFactor = (rawSineWave + 1f) / 2; // recalculated to go from 0 to 1.

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
        
    }
}
