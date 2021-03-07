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
    public Image pencil;
    public Sprite red;
    public Sprite green;
    public Sprite blue;
    public Sprite white;

    public static bool drawing = false;

    private float pitch = 0;
    private float yaw = 0;

    // Start is called before the first frame update
    void Start()
    {

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
    }

    public void ChangeColor(int i) {
        Color cool = Color.white;
        pencil.sprite = white;
        if (i == 1) //r
        {
            cool = Color.red;
            pencil.sprite = red;
        }
        else if (i == 2) { // g
            cool = Color.green;
            pencil.sprite = green;
        }
        else if (i == 3) //b
        {
            cool = Color.blue;
            pencil.sprite = blue;
        }
        spacePenPoint.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", cool);
        currentStroke.GetComponent<TrailRenderer>().startColor = cool;
        currentStroke.GetComponent<TrailRenderer>().endColor = cool;
    }

    public void EndStroke()
    {
        drawing = false;
        currentStroke.transform.parent = null;
    }

}