using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public GameObject LaunchedBall;
    public GameObject Player;
    public GameObject spwnPrt;
    public GameObject bigSplash;

    public GameObject confetti;
    public GameObject confetti1;
    public GameObject confetti2;


    public Transform confettiPos;
    public Transform confettiPos1;


    public List<GameObject> ball = new List<GameObject>();
    public LineRenderer[] lineRenderers;
    public bool isBeachChnged = false;
    public bool level2Changed = false;
    bool effectSpawn = false;

    [Header("Beach-1")]
    public GameObject beach1;
    public GameObject beach2;

    [Header("Level-1")]
    public GameObject bucket;
    public GameObject Target;
    public GameObject TargetText;
    public Transform particleSpwan;
    public bool sp = false;
    public float Dist;
    public bool isReachedGoal = false;

    [Header("Level-2")]
    public GameObject bucket_1;
    public GameObject Target_1;
    public GameObject TargetText_1;
    public Transform particleSpwan_1;
    public bool sp1 = false;
    public float Dist_1;
    public bool isReachedGoal_1 = false;
    
    void Start()
    {
        Application.targetFrameRate = 60;
        gm = this;
    }

    // Update is called once per fram
    private void Update()
    {
        level1();
        StartCoroutine(nextLev(1.2f));

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        dance();
        vfxEffect();
    }
    void level1()
    {
        if (LaunchedBall != null)
        {
            Dist = Vector3.Distance(bucket.transform.position, LaunchedBall.transform.position);
        }
        if (Dist <= 1 && Movement.mv.mouseUp && LaunchedBall != null && !isReachedGoal)
        {

                isReachedGoal = true;

            
        }
        
    }

    IEnumerator nextLev(float t)
    {
        if (isReachedGoal)
        {
            yield return new WaitForSeconds(t);
            StartCoroutine(changeLand());
        }
    }
    IEnumerator changeLand()
    {
        if (!level2Changed)
        {
            switchLand();
            Movement.mv.anime.SetBool("run", true);
            GameManager.gm.Player.transform.rotation = Quaternion.Lerp(GameManager.gm.Player.transform.rotation, Quaternion.Euler(-1.127f, 150.367f, 1.692f),2*Time.deltaTime);/*Quaternion.Euler(-1.127f, 150.367f, 1.692f);*/
            yield return new WaitForSeconds(1.5f);
            GameManager.gm.Player.transform.rotation = Quaternion.Lerp(GameManager.gm.Player.transform.rotation, Quaternion.Euler(1.103f, 95.871f, 1.708f), 2 * Time.deltaTime);/*Quaternion.Euler(1.103f, 83.871f, 1.708f);*/
            Movement.mv.anime.SetBool("run", false);
            level2Changed = true;
            yield return new WaitForSeconds(0.5f);
            Movement.mv.enableEverything();

        }
    }
    public void switchLand()
    {
        if (!isBeachChnged)
        {
            beach1.SetActive(false);
            beach2.SetActive(true);
            Target.SetActive(false);
            TargetText.SetActive(false);


            isBeachChnged = true;
        }
        
    }
    void dance()
    {
        if(GameManager.gm.isReachedGoal && GameManager.gm.isReachedGoal_1)
        {
            Movement.mv.anime.SetBool("dance", true);
        }
    }
    void vfxEffect()
    {
        
        if (CameraFollow.cF.gameOver)
        {
            
            if (!effectSpawn)
            {
                Instantiate(confetti, confettiPos.position, Quaternion.Euler(-155,0,0));
                //Instantiate(confetti2, confettiPos.position, Quaternion.Euler(-155, 0, 0));
                Instantiate(confetti, confettiPos1.position, Quaternion.Euler(-35,0,0));
                //Instantiate(confetti2, confettiPos1.position, Quaternion.Euler(-35, 0, 0));
                effectSpawn = true;
            }
        }
    }
}
