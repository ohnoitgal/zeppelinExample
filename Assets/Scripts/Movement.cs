using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rigi;
    AudioSource audioSource;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationVar = 50f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBoost;
    [SerializeField] ParticleSystem rightBoost;
    [SerializeField] ParticleSystem leftBoost;


    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            float yMovement = mainThrust * Time.deltaTime;
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if(!mainBoost.isPlaying)
            {
                mainBoost.Play();
            }
            rigi.AddRelativeForce(0, yMovement, 0);
        }
        else
        {
            audioSource.Stop();
            mainBoost.Stop();
        }
        
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationVar);
            if(!leftBoost.isPlaying)
            {
                leftBoost.Play();
            }
            
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationVar);
            if(!rightBoost.isPlaying)
            {
                rightBoost.Play();
            }
        }
        else
        {
            leftBoost.Stop();
            rightBoost.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        float zRotation = rotationThisFrame * Time.deltaTime;
        rigi.freezeRotation = true;
        transform.Rotate(Vector3.forward * zRotation);
        rigi.freezeRotation = false;
    }
}
