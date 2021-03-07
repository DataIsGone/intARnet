using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Draw : MonoBehaviour
{
    public bool mouseLookTesting;
    public GameObject stroke;
    public GameObject spacePenPoint;
    public GameObject currentStroke;
    private GameObject totalPost;
    public Image pencil;
    public Sprite red;
    public Sprite green;
    public Sprite blue;
    public Sprite white;
    public Shader s;
    public Material matR;
    public Material matG;
    public Material matB;
    public Material matW;
    private Material currColor;

    public static bool drawing = false;

    private float pitch = 0;
    private float yaw = 0;

    // Start is called before the first frame update
    void Start()
    {
        totalPost = GameObject.Find("TotalPost");
        currColor = matR;
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseLookTesting)
        {
            yaw += 2 * Input.GetAxis("Mouse X");
            pitch -= 2 * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }

    public void StartStroke()
    {     
        drawing = true;
        currentStroke = Instantiate(stroke, spacePenPoint.transform.position, spacePenPoint.transform.rotation) as GameObject;
        currentStroke.SetActive(true);
        currentStroke.transform.SetParent(spacePenPoint.transform);
        currentStroke.GetComponent<Renderer>().material = currColor;
    }

    public void ChangeColor(int i) {
        Color cool = Color.white;
        Material m = new Material(s);
        pencil.sprite = white;
        currColor = matW;
        if (i == 1) //r
        {
            cool = Color.red;
            pencil.sprite = red;
            currColor = matR;
        }
        else if (i == 2) { // g
            cool = Color.green;
            pencil.sprite = green;
            currColor = matG;
        }
        else if (i == 3) //b
        {
            cool = Color.blue;
            pencil.sprite = blue;
            currColor = matB;
        }
        //m.color = cool;
        //spacePenPoint.GetComponent<Renderer>().material = m;
        currentStroke.GetComponent<TrailRenderer>().startColor = cool;
        currentStroke.GetComponent<TrailRenderer>().endColor = cool;
    }

    public void EndStroke()
    {
        drawing = false;
        currentStroke.transform.SetParent(totalPost.transform);
    }

}