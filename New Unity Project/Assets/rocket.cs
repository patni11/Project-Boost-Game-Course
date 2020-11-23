using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    AudioSource thrusters;
    Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        thrusters = GetComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput(){
        if (Input.GetKey(KeyCode.Space)){
            rigidBody.AddRelativeForce(Vector3.up);
            
            if (!thrusters.isPlaying){
            thrusters.Play();
            }

        }else{
            thrusters.Stop();
        }
       
        //thrusters.Stop();
        if (Input.GetKey(KeyCode.A)){
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D)){
            transform.Rotate(-Vector3.forward);
        }

    }
}
