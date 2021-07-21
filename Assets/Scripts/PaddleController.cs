using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] KeyCode Up;
    [SerializeField] KeyCode down;
    [SerializeField] float speed;
    float lowerBound = -4f;
    float upperBound = 2.75f;

    void Update()
    {
        MovePaddle();
        PaddleBound();
    }


    private void MovePaddle()
    {
        if (Input.GetKey(Up))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else if (Input.GetKey(down))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }

    }

    private void PaddleBound()
    {
        if (transform.position.z < lowerBound)
            transform.position = new Vector3(transform.position.x, transform.position.y, lowerBound);
        else if (transform.position.z > upperBound)
            transform.position = new Vector3(transform.position.x, transform.position.y, upperBound);
    }
}
