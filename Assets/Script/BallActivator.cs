using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallActivator : MonoBehaviour
{
    public static BallActivator BA; 
    public Collider col;
    public Rigidbody rb;
    [SerializeField]
    bool activate = false;
    // Update is called once per frame
    void Update()
    {
        if (SphereMovement.instance.move)
        {
            if (SphereMovement.instance.pos_no == 79)
            {
                col.enabled = true;
                gameObject.GetComponent<SphereMovement>().enabled = false;
                rb.isKinematic = false;
                rb.useGravity = true;
                Physics.gravity = new Vector3(0, -9.8f, 0);

                if (!GameManager.gm.sp)
                {
                    rb.AddForce(Vector3.back * 8, ForceMode.Impulse);
                    Destroy(Instantiate(GameManager.gm.bigSplash, GameManager.gm.particleSpwan.position, Quaternion.Euler(140, -48, 0)), 1);
                    GameManager.gm.sp = true;
                }
                if (!GameManager.gm.sp1 && GameManager.gm.level2Changed)
                {
                    
                    rb.AddForce(Vector3.back * 6, ForceMode.Impulse);
                    Destroy(Instantiate(GameManager.gm.bigSplash, GameManager.gm.particleSpwan_1.position, Quaternion.Euler(150, -57, 0)), 1);
                    GameManager.gm.sp1 = true;
                }
            }
        }
        particaleSpwan();
    }
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Goal"))
        {
            GameManager.gm.ball.Add(gameObject);
            //Debug.Log("Hurrey");
            if (!GameManager.gm.isReachedGoal)
            {
                CameraFollow.cF.activate = true;
                //Destroy(Instantiate(GameManager.gm.bigSplash, transform.position, Quaternion.Euler(90,0,0)), 1);
                GameManager.gm.isReachedGoal = true;
            }
            if (GameManager.gm.isBeachChnged)
            {
                Destroy(this.gameObject);
            }
            Movement.mv.mouseUp = false;

        }
        if (coll.gameObject.CompareTag("Goal-1"))
        {
            GameManager.gm.ball.Add(gameObject);
            //Debug.Log("Hurrey_2");
            if (!GameManager.gm.isReachedGoal_1)
            {
                CameraFollow.cF.activate = true;
                //Destroy(Instantiate(GameManager.gm.bigSplash, transform.position, Quaternion.Euler(90, 0, 0)), 1);
                GameManager.gm.isReachedGoal_1 = true;
                
            }
        }
    }
    public void activateSphereMovement()
    {
        gameObject.GetComponent<SphereMovement>().enabled = true;
    }
    void particaleSpwan()
    {
        if(SphereMovement.instance.pos_no == 20 || SphereMovement.instance.pos_no == 40 || SphereMovement.instance.pos_no == 60)
        {
            Destroy(Instantiate(GameManager.gm.spwnPrt, transform.position, Quaternion.identity),1f);
        }
    }
    void ParticleSpwan()
    {

        if (!GameManager.gm.sp && SphereMovement.instance.pos_no == 79)
        {
            Destroy(Instantiate(GameManager.gm.bigSplash, GameManager.gm.particleSpwan.position, Quaternion.Euler(90, 0, 0)), 1);
            GameManager.gm.sp = true;
        }
        if (!GameManager.gm.sp1 && SphereMovement.instance.pos_no == 79 && GameManager.gm.level2Changed)
        {
            Destroy(Instantiate(GameManager.gm.bigSplash, GameManager.gm.particleSpwan.position, Quaternion.Euler(90, 0, 0)), 1);
            GameManager.gm.sp1 = true;
        }
    }
}
