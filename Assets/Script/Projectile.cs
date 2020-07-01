using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static Projectile Pro;
    public GameObject projectile;
    public Transform shootPoint;
    public LayerMask layer;
    public LineRenderer lineVisual;


    public int lineSegment = 10;
    public GameObject[] Points;

	public Transform start_pos,end_pos;
    public GameObject TrajectorySpwans;
    Vector3 cursor;
    private Camera cam;

    bool desabledLaunch = false;

    // Start is called before the first frame update
    void Start()
    {
        Pro = this;
        cam = Camera.main;
        lineVisual.positionCount = lineSegment;

        Points = new GameObject[lineSegment];
        for(int i = 0; i < lineSegment; i++)
        {
            Points[i] = Instantiate(projectile, transform.position, Quaternion.identity);
            Points[i].transform.parent = TrajectorySpwans.transform;
        }

    }

    // Update is called once per frame
    void LateUpdate()
    {
        LaunchProjectile();

    }

    void LaunchProjectile()
    {

	    Vector3 vo = CalculateVelocty(end_pos.position, start_pos.position, 1f);


        Visualize(vo);

    }

    void Visualize(Vector3 vo)
    {
        for (int i = 0; i < Points.Length; i++)
        {
            Vector3 pos = CalculatePosInTime(vo, i / (float)lineSegment);
            Points[i].transform.position = pos;
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
