using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public GameObject bucket;

    bool desabledLaunch = false;

    public List<GameObject> ball = new List<GameObject>();
    public LineRenderer[] lineRenderers;

    void Start()
    {
        gm = this;
    }

    // Update is called once per fram
    private void Update()
    {

    }
}
