using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Camera _cam;
    public float maxLenght;

    private Ray rayMouse;
    private Vector3 pos;
    private Vector3 dir;
    private Quaternion rot;

    private void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        if(_cam != null)
        {
            RaycastHit hit;
            var mousePos = Input.mousePosition;
            rayMouse = _cam.ScreenPointToRay(mousePos);
            if(Physics.Raycast(rayMouse.origin,rayMouse.direction,out hit, maxLenght))
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

    void movement(GameObject obj, Vector3 dest)
    {
        dir = dest - obj.transform.position;
        rot = Quaternion.LookRotation(dir);
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rot, 1);
    }
}
