using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rigi;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationVar = 50f;

    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody>();
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
            rigi.AddRelativeForce(0, yMovement, 0);
        }
        
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationVar);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationVar);
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
