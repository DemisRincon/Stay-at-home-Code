using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLink : MonoBehaviour
{
    public string BansheesVeilContact;

    public string LinkedInContact;

    public string FacebookContact;

    public void OpenBansheesVeil()
    {
        Application.OpenURL(BansheesVeilContact);
    }
    public void OpenFacebook()
    {
        Application.OpenURL(FacebookContact);
    }
    public void OpenLinkedIn()
    {
        Application.OpenURL(LinkedInContact);
    }

}
