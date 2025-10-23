using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerGunLogic : MonoBehaviour
{
    [SerializeField]
    public float lockout = 0.25f;

    [SerializeField]
    public GameObject projectilePrefab;

    [SerializeField]
    public Transform shootFrom;

    private Timer shootTimer;

    private void Start()
    {
        shootTimer = new Timer(lockout);
    }

    // Update is called once per frame
    void Update()
    {
        if(shootTimer.CallAgain(Time.deltaTime) && (Input.GetKey(KeyCode.Space)))
        {
            SpawnProjectile();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnProjectile();
        }
    }

    private void SpawnProjectile()
    {
        var spawnedObject = Instantiate(projectilePrefab, shootFrom.position, transform.rotation);
    }

}