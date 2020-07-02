using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Refelection : MonoBehaviour
{
    public static Refelection re;
    public int maxReflectionCount = 5;
    public float maxStepDistance = 200;

	public List<Vector3> hitPoint = new List<Vector3>();
    
	public GameObject[] pos;
	public GameObject end_pos,start_pos;
    private void Start()
    {
        re = this;
    }
    void OnDrawGizmos()
    {
        //Handles.color = Color.yellow;
       // Handles.ArrowHandleCap(0, this.transform.position + this.transform.forward * 0.25f, this.transform.rotation, 0.5f, EventType.Repaint);

/*        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, 0.25f);*/
        start_pos.transform.position = this.transform.position;
        DrawPredictedReflectionPattern_2(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, maxReflectionCount);


    }
    private void Update()
    {
        start_pos.transform.position = this.transform.position;
        DrawPredictedReflectionPattern(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, maxReflectionCount);
    }

    private void DrawPredictedReflectionPattern(Vector3 position, Vector3 direction, int reflectionsRemaining)
    {
        //Debug.Log(reflectionsRemaining);
	    if (reflectionsRemaining == 0)
	    {
		    return;
	    }
	    if (reflectionsRemaining == maxReflectionCount)
	    {
		    hitPoint.Clear();
            
	    }
	    else
	    {
		    hitPoint.Add(position);
	    }
	    
        
	    if(hitPoint.Count > 0)
	    pos[hitPoint.Count-1].transform.position = hitPoint[hitPoint.Count-1];
        Vector3 startingPosition = position;
        Ray ray = new Ray(position, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxStepDistance))
        {
            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;
            
        }
        else
        {
            position += direction * maxStepDistance;
        }
	  
	    if(reflectionsRemaining == 1)
	    
	    {
	    	end_pos.transform.position = position;
	    }
        /*Gizmos.color = Color.red;
        Gizmos.DrawLine(startingPosition, position);*/
        
        DrawPredictedReflectionPattern(position, direction, reflectionsRemaining - 1);
    }

    private void DrawPredictedReflectionPattern_2(Vector3 position, Vector3 direction, int reflectionsRemaining)
    {
        //Debug.Log(reflectionsRemaining);
        if (reflectionsRemaining == 0)
        {
            return;
        }
        if (reflectionsRemaining == maxReflectionCount)
        {
            hitPoint.Clear();

        }
        else
        {
            hitPoint.Add(position);
        }


        if (hitPoint.Count > 0)
            pos[hitPoint.Count - 1].transform.position = hitPoint[hitPoint.Count - 1];
        Vector3 startingPosition = position;
        Ray ray = new Ray(position, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxStepDistance))
        {
            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;

        }
        else
        {
            position += direction * maxStepDistance;
        }

        if (reflectionsRemaining == 1)

        {
            end_pos.transform.position = position;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawLine(startingPosition, position);

        DrawPredictedReflectionPattern_2(position, direction, reflectionsRemaining - 1);
    }

}
