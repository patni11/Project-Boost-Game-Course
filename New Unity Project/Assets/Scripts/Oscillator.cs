using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{


    [SerializeField] Vector3 movement = new Vector3(10f,10f,10f);
    [Range(0,1)] [SerializeField] float movementFactor;
    [SerializeField] float period = 2f;
    const float tau = Mathf.PI * 2f;
    

    Vector3 startingpos;

    // Start is called before the first frame update
    void Start()
    {
        startingpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon){return;}

        float cycles = Time.time / period; 
        float raw_sine_wave = Mathf.Sin(cycles * tau);
        movementFactor = (raw_sine_wave / 2f) + 0.5f;

        Vector3 offset = movement * movementFactor;
        transform.position = startingpos + offset;
    }
}
