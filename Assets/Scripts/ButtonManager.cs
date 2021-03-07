﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public GameObject modelInterface;
    public GameObject textInterface;
    public GameObject textPost;
    public TMP_InputField typeSpace;
    public GameObject drawMenu;
    public GameObject penPoint;  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setStickerMenuActive(bool b) {
        modelInterface.SetActive(b);
    }

    public void openKeyboard() {
        //TouchScreenKeyboard.Open("");
        System.Diagnostics.Process.Start("osk.exe");
    }

    public void createTextPost() {
        string text = typeSpace.text;
        setTextMenuActive(false);
        GameObject post = Instantiate(textPost, Camera.main.transform.position + Camera.main.transform.forward * 1.5f, Quaternion.identity);
        post.GetComponent<TextMeshPro>().text = text;
        post.AddComponent<MoveObj>();
        post.AddComponent<BoxCollider>();
        post.GetComponent<BoxCollider>().size = new Vector3(post.GetComponent<RectTransform>().sizeDelta.x, post.GetComponent<RectTransform>().sizeDelta.y, 3);
    }

    public void setTextMenuActive(bool b) {
        textInterface.SetActive(b);
    }

    public void setDrawMenuActuve(bool b) {
        penPoint.SetActive(b);
        drawMenu.SetActive(b);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
