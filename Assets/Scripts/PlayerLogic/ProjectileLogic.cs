using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    [SerializeField]
    public float speed = 10f;

    [SerializeField]
    public float lifetime = 5f;




    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.forward * Time.deltaTime* speed);
    }
}
