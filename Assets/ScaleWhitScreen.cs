using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWhitScreen : MonoBehaviour
{
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraRightMiddle = Camera.main.ViewportToWorldPoint(new Vector2(1f, 0.5f));
        cameraRightMiddle.z = transform.position.z;
       gameObject.transform.position = cameraRightMiddle;

       
    }
}
