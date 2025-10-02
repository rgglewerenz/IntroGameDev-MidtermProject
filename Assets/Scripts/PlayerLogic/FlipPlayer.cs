using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipPlayer : MonoBehaviour
{
    int maxX = 38;
    int minX = -38;
    int maxZ = 18;
    int minZ = -18;

    private void OnTriggerExit(Collider other)
    {
        if(other.tag != "bounds" && other.tag != "bullet" && other.tag != "Player")
        {
            //Debug.Log("Object exited bouds");
            FixEntity(other.gameObject);
            return;
        }

        if(other.tag == "Player")
        {
            //Debug.Log("Player exited bounds");
            FixPlayer(other.gameObject);
        }
    }

    private void FixPlayer(GameObject gameObject)
    {
        var position = gameObject.transform.position;
        var rb = gameObject.GetComponentInParent<Rigidbody>();

        if (position.x >= maxX)
        {
            rb.MovePosition(new Vector3(minX + 1, position.y, position.z));
        }
        if (position.x <= minX)
        {
            rb.MovePosition(new Vector3(maxX - 1, position.y, position.z));
        }
        if (position.z >= maxZ)
        {
            rb.MovePosition(new Vector3(position.x, position.y, minZ + 1));
        }
        if (position.z <= minZ)
        {
            rb.MovePosition(new Vector3(position.x, position.y, maxZ - 1));
        }
    }

    void FixEntity(GameObject otherObject)
    {
        var position = otherObject.transform.position;
        if (position.x >= maxX)
        {
            otherObject.transform.position = new Vector3(minX + 1, position.y, position.z);
        }

        if (position.x <= minX)
        {
            otherObject.transform.position = new Vector3(maxX - 1, position.y, position.z);
        }

        if (position.z >= maxZ)
        {
            otherObject.transform.position = new Vector3(position.x, position.y, minZ + 1);
        }

        if (position.z <= minZ)
        {
            otherObject.transform.position = new Vector3(position.x, position.y, maxZ - 1);
        }
    }

}
