using UnityEngine;
using System.Collections;

public class MicrowaveClickGame : MonoBehaviour
{
    private int clickCount = 0;
    public GameObject hammer;
    public GameObject microwave;
    private const int maxClicks = 100;

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
            }
        }
    }

    private IEnumerator ClickAnimation()
    {
        hammer.transform.eulerAngles = new Vector3(0, 0, 50);
        yield return new WaitForSeconds(0.2f);
        hammer.transform.eulerAngles = new Vector3(0, 0, 0);
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
