using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    [SerializeField] float thrust = 5f;
    [SerializeField] float rotation = 3f;

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
        Rotate();
        Thrust();
        
    }

    private void Rotate(){

       float applied_rotation = rotation * Time.deltaTime;

        if (Input.GetKey(KeyCode.A)){
            transform.Rotate(applied_rotation*Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D)){
            transform.Rotate(-applied_rotation*Vector3.forward);
        }

    }

    private void Thrust(){
            rigidBody.freezeRotation = true;
            float applied_thrust = thrust * Time.deltaTime;

           if (Input.GetKey(KeyCode.Space)){
            rigidBody.AddRelativeForce(applied_thrust*Vector3.up);
            
            if (!thrusters.isPlaying){
            thrusters.Play();
            }

        }else{
            thrusters.Stop();
        }
      rigidBody.freezeRotation = false; 
    }
}
