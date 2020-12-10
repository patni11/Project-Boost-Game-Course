﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rocket : MonoBehaviour
{
    [SerializeField] float thrust = 5f;
    [SerializeField] float rotation = 3f;
    
    [SerializeField] AudioClip rocket_audio;
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip victory;
    
    [SerializeField] ParticleSystem thrust_fire_particle;
    [SerializeField] ParticleSystem explosion_particle;
    [SerializeField] ParticleSystem victory_particle;

    AudioSource audio;
    Rigidbody rigidBody;
    // Start is called before the first frame update

enum State {Alive, Dying, Transending}
State state = State.Alive;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive){
        Rotate();
        Thrust();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Finish":
                if (rigidBody.velocity.magnitude <= 0.01f)
                {
                    if (state != State.Transending){
                    state = State.Transending;
                    audio.Stop(); 
                    victory_particle.Play();
                    audio.PlayOneShot(victory);  
                    print("You Won");
                    Invoke("LoadNextScene", 1f);
                    }
                }
                break;
        }
    }

private void LoadNextScene(){
    SceneManager.LoadScene(1);
}
private void LoadDead(){
       SceneManager.LoadScene(0);
}

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive){
            return;
        }
        
        switch (collision.gameObject.tag)
        {

            case "friendly":
                // do nothing
                break;

            case "Finish":
                //do nothing
                break;

            default:
                audio.Stop();
                thrust_fire_particle.Stop();
                explosion_particle.Play();
                audio.PlayOneShot(explosion);  
                state = State.Dying;
                Invoke("LoadDead",1.5f);
                break;

        }

    }

    private void Rotate()
    {

        float applied_rotation = rotation * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(applied_rotation * Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-applied_rotation * Vector3.forward);
        }

    }

    private void Thrust()
    {
        rigidBody.freezeRotation = true;
        float applied_thrust = thrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(applied_thrust * Vector3.up);

            if (!audio.isPlaying)
            {
                audio.PlayOneShot(rocket_audio);
                thrust_fire_particle.Play();
            }

        }
        else
        {
            thrust_fire_particle.Stop();
            audio.Stop();
        }

        rigidBody.freezeRotation = false;
    }
}
