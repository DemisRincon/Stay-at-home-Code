using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkmark : MonoBehaviour
{
    private GameObject CheckmarkImage;

    //GameObject CheckmarkImageStatus = GameObject.Find("Checkmark");
    // Start is called before the first frame update
    void Start()
    {
        CheckmarkImage = GameObject.Find("CheckmarkImageB");
        //.gameObject.SetActive(true); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("g")) CheckmarkImage.SetActive(true);
        if (Input.GetKeyDown("f")) CheckmarkImage.SetActive(false);

        //CheckmarkImage.gameObject.SetActive(true);
    }

}
