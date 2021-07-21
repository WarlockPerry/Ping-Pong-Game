using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] float speed;
    float maxSpeed = 10;
    Vector3 velocityBallDirection; 
    Rigidbody ballRb;

    public float speedB;


    // Start is called before the first frame update
    void Start()
    {
        //speed increment
        if(speedB <= maxSpeed)
            speedB += SpawnManager.instance.BallSpeedIncrement();
        speed = speedB * speedRatio() ;

        // random direction of ball
        transform.localRotation = Quaternion.Euler(GenerateRandomDirection());
        ballRb = GetComponent<Rigidbody>();

        // adding initial force to ball
        if(SpawnManager.instance.wonR)
            ballRb.AddForce(transform.localRotation * Vector3.left * speed) ;
        else
            ballRb.AddForce(transform.localRotation * Vector3.right * speed) ;

    }

    // Update is called once per frame
    void Update()
    {
        velocityBallDirection = ballRb.velocity;
    }

    private Vector3 GenerateRandomDirection()
    {
        int rand = Random.Range(1, 3);
        float yy  = 1f;

        switch (rand)
        {
            case 1:
                yy = Random.Range(-30f, -60f);
                break;
            case 2:
                yy = Random.Range(30f, 60f);
                break;
        }

        Vector3 direction = new Vector3(transform.rotation.x, yy, transform.rotation.z);
        return direction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var direction = Vector3.Reflect(velocityBallDirection.normalized, collision.contacts[0].normal);

        ballRb.velocity = direction.normalized * speedB;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wallL"))
        {
            Destroy(gameObject);
            SpawnManager.instance.ballIsDestroyed = true;
            SpawnManager.instance.textR.gameObject.SetActive(true);
            SpawnManager.instance.scoreRR++;
            SpawnManager.instance.wonR = true;
        }
        else if (other.gameObject.CompareTag("wallR"))
        {
            Destroy(gameObject);
            SpawnManager.instance.ballIsDestroyed = true;
            SpawnManager.instance.textL.gameObject.SetActive(true);
            SpawnManager.instance.scoreLL++;
            SpawnManager.instance.wonR = false;
        }
    }

    float speedRatio()
    {
        return speed / speedB;
    }

}
