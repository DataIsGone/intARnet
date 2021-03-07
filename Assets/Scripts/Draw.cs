using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public bool mouseLookTesting;
    public GameObject stroke;
    public GameObject spacePenPoint;
    public GameObject currentStroke;

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
        if (i == 1) //r
        {
            cool = Color.red;
        }
        else if (i == 2) { // g
            cool = Color.green;
        }
        else if (i == 3) //b
        {
            cool = Color.blue;
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