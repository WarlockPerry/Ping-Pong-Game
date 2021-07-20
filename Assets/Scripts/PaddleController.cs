using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] KeyCode Up;
    [SerializeField] KeyCode down;
    [SerializeField] float speed;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
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
        if (transform.position.z < -4f)
            transform.position = new Vector3(transform.position.x, transform.position.y, -4f);
        else if (transform.position.z > 2.6f)
            transform.position = new Vector3(transform.position.x, transform.position.y, 2.6f);
    }
}
