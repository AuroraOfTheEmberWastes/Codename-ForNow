using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class MicrowaveClickGame : MonoBehaviour
{
    private int clickCount = 0;
    public GameObject hammer;
    public GameObject microwave;
    private const int maxClicks = 100;
    public List<GameObject> enableObj;
    public List<GameObject> disableObj;

    public MicrowaveClickUI uiScript;

    void Update()
    {
        if (WasTapped() && uiScript != null && uiScript.timerRunning)
        {
            clickCount++;

            if (clickCount < maxClicks)
            {
                StartCoroutine(ClickAnimation());
            }
            else if (clickCount == maxClicks)
            {
                microwave.SetActive(false);
                StartCoroutine(endingGame());
            }
        }
    }

    private IEnumerator ClickAnimation()
    {
        hammer.transform.eulerAngles = new Vector3(0, 0, 50);
        yield return new WaitForSeconds(0.2f);
        hammer.transform.eulerAngles = new Vector3(0, 0, 0);
    }


    public IEnumerator endingGame()
    {
        yield return new WaitForSeconds(1f);
        foreach (GameObject obj in enableObj)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        foreach (GameObject obj in disableObj)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }

    private bool WasTapped()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        return Input.GetMouseButtonDown(0);
#else
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
#endif
    }
}
