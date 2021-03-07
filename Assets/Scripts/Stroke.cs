using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stroke : MonoBehaviour
{
    private GameObject penPoint;

    // Start is called before the first frame update
    void Start()
    {
        penPoint = GameObject.Find("PenPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (Draw.drawing)
        {
            this.transform.position = penPoint.transform.position;
            this.transform.rotation = penPoint.transform.rotation;
        }
        else
        {
            this.enabled = false;
        }

    }
}