using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] Vector3 velocityBallDirection;
    [SerializeField] float speed;
    public float speedB;
    Rigidbody ballRb;


    // Start is called before the first frame update
    void Start()
    {

        if(speedB < 8)
            speedB += SpawnManager.instance.BallSpeedIncrement();
        speed = speedB * 50;

        transform.localRotation = Quaternion.Euler(GenerateRandomDirection());
        ballRb = GetComponent<Rigidbody>();

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
            SpawnManager.instance.scoreR.text = "Score : " + SpawnManager.instance.scoreRR;
        }
        else if (other.gameObject.CompareTag("wallR"))
        {
            Destroy(gameObject);
            SpawnManager.instance.ballIsDestroyed = true;
            SpawnManager.instance.textL.gameObject.SetActive(true);
            SpawnManager.instance.scoreLL++;
            SpawnManager.instance.scoreL.text = "Score : " + SpawnManager.instance.scoreLL;
        }
    }

    
}
