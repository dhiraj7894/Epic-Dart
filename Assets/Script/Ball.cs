using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public SphereMovement sm;
    public Rigidbody rb;
    private void Start()
    {
        GetComponent<Ball>().enabled = false;
        if (sm != null)
        {
            sm = GetComponent<SphereMovement>();
        }
        rb.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (sm.stop)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            Physics.gravity = new Vector3(0, -9.8f, 0);
        }
    }
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Goal"))
        {
            Debug.Log("Goal");
        }   
    }
}
