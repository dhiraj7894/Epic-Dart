using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallActivator : MonoBehaviour
{
    public Collider col;
    public Rigidbody rb;
    [SerializeField]
    bool activate = false;
    // Update is called once per frame
    void Update()
    {
        if (SphereMovement.instance.move)
        {
            if (transform.position == SphereMovement.instance.pos[(Projectile.Pro.lineSegment*3)-1])
            {
                col.enabled = true;
                gameObject.GetComponent<SphereMovement>().enabled = false;

                rb.isKinematic = false;
                rb.useGravity = true;
                Physics.gravity = new Vector3(0, -9.8f, 0);
            }
        }

    }
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Goal"))
        {
            GameManager.gm.ball.Add(gameObject);
        }
    }
}
