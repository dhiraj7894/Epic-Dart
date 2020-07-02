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
    public Animator anime;
    public bool mouseUp = false;
    bool isBallSpwan = false;
    public float fireRate = 1;
    public float cooldown;
    public GameObject SpwanBall;
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
        }

        if (mouseUp)
        {
            StartCoroutine(throwBall(2f));
            StartCoroutine(lauch(0.8f));
            desableGameOver();
            if (GameManager.gm.level2Changed && SphereMovement.instance.pos_no == 65)
            {
                TimeManager.tM.slowMo();
                CameraFollow.cF.sSpeed = 80f;
                CameraFollow.cF.dist = new Vector3(-8, 2f, 0);
                CameraFollow.cF.cameraTarget = GameManager.gm.LaunchedBall.transform;
                CameraFollow.cF.transform.LookAt(GameManager.gm.LaunchedBall.transform);
            }
            if (GameManager.gm.level2Changed && SphereMovement.instance.pos_no == 77)
            {
                TimeManager.tM.norMotion();
            }
            //cooldown = 1f / fireRate;
        }
    }
    IEnumerator lauch(float t)
    {
        yield return new WaitForSeconds(t);
        if (!isBallSpwan)
        {
            SpwanBall = Instantiate(bBall, transform.position, Quaternion.identity);
            GameManager.gm.LaunchedBall = SpwanBall;
            isBallSpwan = true;
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
        if (_cam != null && Input.GetMouseButton(0) && !CameraFollow.cF.gameOver)
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
    public void desableGameOver()
    {
        Projectile.Pro.TrajectorySpwans.SetActive(false);
        p1.GetComponent<LineRenderer>().enabled = false;
        p2.GetComponent<LineRenderer>().enabled = false;
        p3.GetComponent<LineRenderer>().enabled = false;
        GameManager.gm.Target.SetActive(false);
        GameManager.gm.TargetText.SetActive(false);

        if (GameManager.gm.isBeachChnged)
        {
            GameManager.gm.Target_1.SetActive(false);
            GameManager.gm.TargetText_1.SetActive(false);
        }
       

        
    }
    public void enableEverything()
    {
        if (GameManager.gm.LaunchedBall != null)
        {
            Destroy(Movement.mv.SpwanBall);
        }
        Projectile.Pro.TrajectorySpwans.SetActive(true);
        p1.GetComponent<LineRenderer>().enabled = true;
        p2.GetComponent<LineRenderer>().enabled = true;
        p3.GetComponent<LineRenderer>().enabled = true;
        isBallSpwan = false;
        
    }
    IEnumerator throwBall(float t)
    {
        anime.SetBool("throw", true);
        yield return new WaitForSeconds(t);
        anime.SetBool("throw", false);
    }
}
