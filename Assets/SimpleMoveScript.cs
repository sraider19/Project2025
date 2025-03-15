using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleMoveScript : MonoBehaviour
{
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
    }
}