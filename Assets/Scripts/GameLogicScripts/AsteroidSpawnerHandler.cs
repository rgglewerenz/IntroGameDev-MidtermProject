using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class AsteroidSpawnerHandler : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    public GameObject LargeAsteroid;
    [SerializeField]
    public GameObject MediumAsteroid;
    [SerializeField]
    public GameObject SmallAsteroid;
    [SerializeField]
    public string AsteroidTags;

    [Header("Player Info")]
    [SerializeField]
    public Transform player;
    [Header("Options")]
    [SerializeField]
    public int maxRadius = 20;
    [SerializeField]
    public int minRadius = 5;
    [SerializeField]
    public int MaxAsteroids = 20;

    private Timer spawnAsteroidMinimumTimer = new Timer(3f);

    private Random Random;

    // Start is called before the first frame update
    void Start()
    {
        Random = new Random();
    }

    void Update()
    {
        if (AbleToSpawn())
        {
            SpawnAsteroid();
        }
    }

    bool AbleToSpawn()
    {
        if (!spawnAsteroidMinimumTimer.CallAgain(Time.deltaTime))
        {
            return false;
        }

        var asteroidCount = GameObject.FindGameObjectsWithTag(AsteroidTags).Count();

        if (asteroidCount >= MaxAsteroids)
        {
            return false;
        }


        return true;
    }

    void SpawnAsteroid()
    {
        AsteroidSize size = (AsteroidSize)Random.Next(0, 3);

        GameObject asteroid;

        switch(size)
        {
            case AsteroidSize.Large:
                asteroid = Instantiate(LargeAsteroid);
                break;
            case AsteroidSize.Medium:
                asteroid = Instantiate(MediumAsteroid);
                break;
            case AsteroidSize.Small:
                asteroid = Instantiate(SmallAsteroid);
                break;
            default:
                asteroid = Instantiate(LargeAsteroid);
                break;
        }

        RandomizeSpawnLocation(asteroid);
    }

    void RandomizeSpawnLocation(GameObject asteroid)
    {
        float radius = Random.Next(minRadius, maxRadius);
        float angle = (float)(Random.NextDouble() * 360);
        Vector3 spawnPosition = new Vector3(player.position.x + radius * Mathf.Cos(angle), 0, player.position.z + radius * Mathf.Sin(angle));
        asteroid.transform.position = spawnPosition;

        asteroid.transform.Rotate(new Vector3(0, 1, 0) * (float)(Random.NextDouble() * 360));

        GenericAsteroidLogic asteroidLogic = asteroid.GetComponent<GenericAsteroidLogic>();


        if (asteroidLogic != null)
        {
            asteroidLogic.velocity = asteroid.transform.forward * (float)(Random.NextDouble() * 5 + 1);
            asteroidLogic.rotationSpeed = (float)(Random.NextDouble() * 90 - 45);
            asteroidLogic.despawnRadius = 100;
        }
    }


    enum AsteroidSize
    {
        Large = 2,
        Medium = 1,
        Small = 0
    }
}