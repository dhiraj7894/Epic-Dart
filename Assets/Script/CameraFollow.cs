using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow cF;
    public GameObject cam;
    public Transform cameraTarget;
    public float sSpeed = 10.0f;
    public Vector3 dist;
    public bool activate = false;
    public bool gameOver = false;



    bool s = false;
    Vector3 dPos,sPos;

    private void Start()
    {
        cF = this;
    }
    void Update()
    {
        level();
        level2();
        if(GameManager.gm.level2Changed && activate && !s)
        {
            activate = false;
            s = true;
        }
    }
    void level()
    {
        if (GameManager.gm.LaunchedBall != null && !GameManager.gm.level2Changed)
        {
            if (SphereMovement.instance.pos_no == 79 && !activate)
            {
                activate = true;
            }

            if (!activate && !GameManager.gm.isBeachChnged)
            {
                dPos = cameraTarget.position + dist;
                sPos = Vector3.Lerp(transform.position, dPos, sSpeed * Time.deltaTime);
                transform.position = sPos;
            }
        }
        if (!GameManager.gm.level2Changed && activate)
        {
            StartCoroutine(cameraPos(0.2f));

        }
    }
    void level2()
    {
        dPos = cameraTarget.position + dist;
        sPos = Vector3.Lerp(transform.position, dPos, sSpeed * Time.deltaTime);
        if (GameManager.gm.LaunchedBall != null && GameManager.gm.level2Changed)
        {
            if (SphereMovement.instance.pos_no == 79 && !activate)
            {
                activate = true;
            }
            
            if (!activate)
            {
                if(SphereMovement.instance.pos_no < 60)
                    dist = new Vector3(0.75f, 0.1f, 0);
                transform.position = sPos;
            }
            if (activate)
            {
                StartCoroutine(end(0.5f));
            }
        }
    }
    IEnumerator end(float t)
    {
        yield return new WaitForSeconds(t);
        sSpeed = 2.5f;
        dist = new Vector3(8, 0.5f, 0);
        cameraTarget = GameManager.gm.Player.transform;
        transform.position = sPos;
        transform.LookAt(GameManager.gm.Player.transform);
        gameOver = true;
    }
    
    IEnumerator cameraPos(float t)
    {
        yield return new WaitForSeconds(t);
        cameraTarget = cam.transform;
        transform.position = Vector3.Lerp(transform.position, new Vector3(-11.26f, 7.39f, -0.45f), 5 * Time.deltaTime);
        transform.rotation = Quaternion.Euler(13.464f, 88.42f, 0);
        /*transform.position = Vector3.Lerp(transform.position, new Vector3(-5.1f, 14f, -0.2f), 5 * Time.deltaTime);
        transform.rotation = Quaternion.Euler(44.31f, 87.86f, -1.12f);*/
    }
    
}
