using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderValueChanger : MonoBehaviour
{
    public static ShaderValueChanger sVC;
    public Material water;
    public Material sand;
    public Material miniCity;
    public Material parkMat;
    public Material tree;
    public Material cloud;
    public Material secBucket;

    public float a = 1;
    public float fadeSpeed = 0.3f;

    bool mouseUp = false;
    bool mouseDown = false;

    private void Start()
    {
        a = 1;
        setDefaultCol();
        sVC = this;
    }
    void Update()
    {
        colorChange();
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
            mouseUp = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
            mouseUp = true;
        }
    }
    void colorChange()
    {
        if (!CameraFollow.cF.gameOver)
        {
            if (mouseDown)
            {
                a -= Time.deltaTime / fadeSpeed;
                if (a <= 0)
                    a = 0;
                changeShader(a);
            }
            if (mouseUp)
            {
                a += Time.deltaTime / fadeSpeed;
                if (a >= 1)
                    a = 1;
                sVC.changeShader(a);
            }
        }
        
    }
    public void changeShader(float a)
    {
        water.SetFloat("scale", a);
        sand.SetFloat("scale", a);
        miniCity.SetFloat("scale", a);
        parkMat.SetFloat("scale", a);
        tree.SetFloat("scale", a);
        cloud.SetFloat("scale", a);
        secBucket.SetFloat("scale", a);
    }
    void setDefaultCol()
    {
        water.SetFloat("scale", 1);
        sand.SetFloat("scale", 1);
        miniCity.SetFloat("scale", 1);
        parkMat.SetFloat("scale", 1);
        tree.SetFloat("scale", 1);
        cloud.SetFloat("scale", 1);
        secBucket.SetFloat("scale", 1);
    }
}
