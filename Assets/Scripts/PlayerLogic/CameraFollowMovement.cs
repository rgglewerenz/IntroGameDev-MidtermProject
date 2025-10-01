using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowMovement : MonoBehaviour
{
    [SerializeField]
    public Transform followEntity;
    [SerializeField]
    public int heightOffset = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        //var destination = followEntity.position;
        //destination.y += heightOffset;

        //this.transform.position = Vector3.Lerp(destination, this.transform.position, 0.25f);
    }
}
