using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static Movement mv;
    private Camera _cam;
    public float maxLenght;
    private Ray rayMouse;
    private Vector3 pos;
    private Vector3 dir;
    private Quaternion rot;
    public GameObject bBall;
    public GameObject p1, p2, p3;

    public bool mouseUp = false;

    public float fireRate = 1;
    public float cooldown;
    private void Start()
    {
        mv = this;
        _cam = Camera.main;
    }

    private void Update()
    {
        launch();
        cooldown -= Time.deltaTime;
        if (Input.GetMouseButtonUp(0))
        {
            mouseUp = true;
            desableLineRender();
        }

        if (mouseUp && cooldown <= 0 && GameManager.gm.ball.Count<=5)
        {
            Instantiate(bBall, transform.position, Quaternion.identity);
            cooldown = 1f / fireRate;
        }
    }

    void movement(GameObject obj, Vector3 dest)
    {
        dir = dest - obj.transform.position;
        rot = Quaternion.LookRotation(dir);
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rot, 1);
    }
    void launch()
    {
        if (_cam != null && Input.GetMouseButton(0))
        {
            RaycastHit hit;
            var mousePos = Input.mousePosition;
            rayMouse = _cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit, maxLenght))
            {
                movement(gameObject, hit.point);
            }
            else
            {
                var pos = rayMouse.GetPoint(maxLenght);
                movement(gameObject, pos);
            }
        }
    }
    public void desableLineRender()
    {
        Projectile.Pro.TrajectorySpwans.SetActive(false);
        p1.GetComponent<LineRenderer>().enabled = false;
        p2.GetComponent<LineRenderer>().enabled = false;
        p3.GetComponent<LineRenderer>().enabled = false;
    }
}
