using System.Collections;
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
    public GameObject viewPos;
    public GameObject addPost;
    public GameObject postUI;
    private GameObject totalPost;
    public GameObject submit;

    // Start is called before the first frame update
    void Start()
    {
        totalPost = GameObject.Find("TotalPost");
    }

    public void postModeActive() {
        if (addPost.activeSelf)
        {
            postUI.SetActive(true);
            addPost.SetActive(false);
        }
        else {
            postUI.SetActive(false);
            addPost.SetActive(true);
        }
    }

    public void submitPost() {
        postUI.SetActive(false);
        addPost.SetActive(true);
        //Send TotalPost somewhere
        for (int i = totalPost.transform.childCount - 1; i >= 0; --i) {
            Destroy(totalPost.transform.GetChild(i));
        }
    }

    public void setStickerMenuActive(bool b) {
        if (modelInterface.activeSelf)
        {
            modelInterface.SetActive(false);
        }
        else
        {
            modelInterface.SetActive(b);
        }
    }
    public void setTextMenuActive(bool b)
    {
        if (textInterface.activeSelf)
        {
            textInterface.SetActive(false);
        }
        else
        {
            textInterface.SetActive(b);
        }
    }

    public void setDrawMenuActuve(bool b)
    {
        submit.SetActive(true);
        if (drawMenu.activeSelf)
        {
            drawMenu.SetActive(false);
            penPoint.SetActive(false);
        }
        else
        {
            if (b) {
                submit.SetActive(false);
            }
            penPoint.SetActive(b);
            drawMenu.SetActive(b);
        }
    }

    public void openKeyboard() {
        //TouchScreenKeyboard.Open("");
        System.Diagnostics.Process.Start("osk.exe");
    }

    public void createTextPost() {
        string text = typeSpace.text;
        setTextMenuActive(false);
        GameObject post = Instantiate(textPost, viewPos.transform.position + viewPos.transform.forward * 1.5f, Quaternion.identity);
        post.GetComponent<TextMeshPro>().text = text;
        post.AddComponent<MoveObj>();
        post.AddComponent<BoxCollider>();
        post.GetComponent<BoxCollider>().size = new Vector3(post.GetComponent<RectTransform>().sizeDelta.x, post.GetComponent<RectTransform>().sizeDelta.y, 3);
        post.transform.SetParent(totalPost.transform);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
