using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnaticHolder : MonoBehaviour
{
    public Vector3 position;
    public Vector3 scale;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("blockLarge"))
        {
            other.transform.localPosition = position;
            other.transform.localScale = scale;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            other.GetComponent<Rigidbody>().mass = 1000;
            other.GetComponent<Rigidbody>().drag = 1000;
            other.GetComponent<Rigidbody>().angularDrag = 1000;
            other.transform.localRotation = Quaternion.identity;
        }
    }

}
