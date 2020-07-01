using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public Rigidbody blackBalls;
    public Rigidbody yellowBalls;
    public GameObject cursor;
    public Transform shootPoint;
    public LayerMask layer;
    public LineRenderer lineVisual;
    public int lineSegment = 10;
    public int speed = 10;
    bool relesedUp = false;

    Vector3 vo;
    [SerializeField]
    float coolDown;
    [SerializeField]
    float fireRate;
    [SerializeField]
    float fireRateYellow;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        lineVisual.positionCount = lineSegment;
    }

    // Update is called once per frame
    void Update()
    {
        LaunchProjectile();
    }

    void LaunchProjectile()
    {
        coolDown -= Time.deltaTime;

        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit, 100f, layer))
        {
            if (!relesedUp)
            {
                cursor.SetActive(true);
                cursor.transform.position = hit.point + Vector3.up * 0.1f;

                vo = CalculateVelocty(hit.point, shootPoint.position, 1f);

                Visualize(vo);

                transform.rotation = Quaternion.LookRotation(vo);
            }
            

            if (Input.GetMouseButton(0) && !relesedUp)
            {
                if (coolDown <= 0)
                {
                    Rigidbody obj = Instantiate(blackBalls, shootPoint.position, Quaternion.identity);
                    //obj.velocity = vo;
                    obj.AddForce(shootPoint.forward * speed, ForceMode.VelocityChange);
                    coolDown = 1f / fireRate;
                }

            }
            if (relesedUp)
            {
                    if (coolDown <= 0)
                    {

                        Rigidbody obj = Instantiate(yellowBalls, shootPoint.position, Quaternion.identity);
                        //obj.velocity = vo;
                        obj.AddForce(shootPoint.forward * speed, ForceMode.VelocityChange);
                        coolDown = 1f / fireRateYellow;
                    }
                    
                
            }
            if(Input.GetMouseButtonUp(0))
            {
                relesedUp = true;
            }
        }
    }

    void Visualize(Vector3 vo)
    {
        for (int i = 0; i < lineSegment; i++)
        {
            Vector3 pos = CalculatePosInTime(vo, i / (float)lineSegment);
            lineVisual.SetPosition(i, pos);
        }
    }

    Vector3 CalculateVelocty(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXz = distance;
        distanceXz.y = 0f;

        float sY = distance.y;
        float sXz = distanceXz.magnitude;

        float Vxz = sXz * time;
        float Vy = (sY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);

        Vector3 result = distanceXz.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }

    Vector3 CalculatePosInTime(Vector3 vo, float time)
    {
        Vector3 Vxz = vo;
        Vxz.y = 0f;

        Vector3 result = shootPoint.position + vo * time;
        float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vo.y * time) + shootPoint.position.y;

        result.y = sY;

        return result;
    }
}
