using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{

    // the image you want to fade, assign in inspector
   [SerializeField] private Image blackOutImage;
   [SerializeField] private GameObject imageGameObject;
 
    private void Start()
    {
        RunFade();
    }



    public void RunFade()
    {
        // fades the image out when you click
        imageGameObject.SetActive(true);
        StartCoroutine(FadeImage(true));
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 2; i >= 0; i -=  Time.deltaTime)
            {
                // set color with i as alpha
                blackOutImage.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 2; i += Time.deltaTime)
            {
                // set color with i as alpha
                blackOutImage.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        imageGameObject.SetActive(false);
    }
}