using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    public static SphereMovement instance;
    public float speed;
    Vector3 current_target;
    public List<Vector3> pos;
	public int pos_no;
	public LineRenderer[] lineRenderer;
	
	public bool move;
	float distance;

    public bool stop = false;
    
    public bool activate = false;
    
    private void Start()
    {
        instance = this;
    }
    void Update()
    {
        AddPos();

	    if(move)
	    {
	    	distance = Vector3.Distance(transform.position, current_target);
		    if (distance >1)
		    {	
			    // movement of Boomerange 
				
		    }
		    else
		    {
			    // set next target position  
			    if( pos_no <= pos.Count-2 )
			    {
				    pos_no ++;
                    current_target = pos[pos_no];

                }
			    else
			    {
			    	pos_no = 1;
				    current_target =pos[pos_no];
			    	transform.position = pos[pos_no];
			    }
		    }

            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, current_target, speed * Time.fixedDeltaTime);
        }

        
        
    }
    
    
	public void AddPos()
	{

		pos.Clear();
		for(int i =0;i< GameManager.gm.lineRenderers.Length;i++)
		{
			for(int j =0; j< GameManager.gm.lineRenderers[i].positionCount;j++)
			{
				pos.Add(GameManager.gm.lineRenderers[i].GetPosition(j));
                activate = true;
			}
				
		}
        move = true;
	}
}
