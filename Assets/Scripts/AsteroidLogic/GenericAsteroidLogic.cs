using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class GenericAsteroidLogic : MonoBehaviour
{
    [SerializeField]
    public GameObject smallerAsteroidPrefab;
    [SerializeField]
    public Vector3 velocity;
    [SerializeField]
    public float rotationSpeed = 100f;
    [SerializeField]
    public int breakAmount = 4;
    [SerializeField]
    public Transform player;
    [SerializeField]
    public int despawnRadius = 80;

    [SerializeField]
    public int score = 100;

    private Timer checkDistanceTimer = new Timer(2f);

    // Start is called before the first frame update
    void Start()
    {
        if (velocity == null)
        {
            velocity = new Vector3(1, 1, 0);
        }

        if(player == null)
        {
            player = GameObject.FindGameObjectsWithTag("Player").First().transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (checkDistanceTimer.CallAgain(Time.deltaTime))
        {
            
            if (Vector3.Distance(this.transform.position, player.position) >= despawnRadius)
            {
                Debug.Log("Asteroid despawned");
                Debug.Log("Distance: " + Vector3.Distance(this.transform.position, player.position));
                Destroy(gameObject);
            }
        }

        this.transform.position += velocity * Time.deltaTime;
        this.transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);

        this.transform.position = new Vector3(this.transform.position.x, 0, transform.position.z);
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            var scoreHandler = GameObject.FindFirstObjectByType<ScoreHandler>();
            var healthHandler = GameObject.FindFirstObjectByType<PlayerHealthLogic>();
            onBreak();
            Debug.Log("Asteroid hit");
            healthHandler.addToShield();
            scoreHandler.UpdateScore(score);
            Destroy(gameObject);
            Destroy(collision.gameObject);
            return;
        }
    }


    void onBreak()
    {
        {
            if (smallerAsteroidPrefab == null)
                return;

            for (int i = 0; i < breakAmount; i++)
            {
                var asteroid = Instantiate(smallerAsteroidPrefab);
                int rotationAngle = (360 / breakAmount);
                asteroid.transform.Rotate(new Vector3(0, 1, 0) * ((i * rotationAngle) + 45 ));
                asteroid.transform.position = this.transform.position + (asteroid.transform.forward * 2.8f);
                var asteroidLogic = asteroid.GetComponent<GenericAsteroidLogic>();
                asteroidLogic.velocity = asteroid.transform.forward * this.velocity.magnitude * 1.5f;
                if (rotationAngle >= 180)
                {
                    asteroidLogic.rotationSpeed = -this.rotationSpeed;
                }
            }
        }

    }
}
