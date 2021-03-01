using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLinkUnique : MonoBehaviour
{

    public string Link;

    public void OpenLink()
    {
        Application.OpenURL(Link);
    }
}
