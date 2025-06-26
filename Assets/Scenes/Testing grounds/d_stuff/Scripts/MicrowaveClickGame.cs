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

    public SpriteRenderer microwaveSpriteRenderer;
    public Sprite sprite1; // 0–33 clicks
    public Sprite sprite2; // 34–66 clicks
    public Sprite sprite3; // 67–99 clicks
    public Sprite sprite4; // 100 clicks

    void Update()
    {
        if (WasTapped() && uiScript != null && uiScript.timerRunning)
        {
            clickCount++;
            UpdateMicrowaveSprite();

            if (clickCount < maxClicks)
            {
                StartCoroutine(ClickAnimation());
            }
            else if (clickCount == maxClicks)
            {
                //microwave.SetActive(false);
                microwaveSpriteRenderer.sprite = sprite4;
                StartCoroutine(endingGame());
            }
        }
    }

    private void UpdateMicrowaveSprite()
    {
        if (clickCount >= maxClicks)
        {
            microwaveSpriteRenderer.sprite = sprite4;
        }
        else if (clickCount >= 67)
        {
            microwaveSpriteRenderer.sprite = sprite3;
        }
        else if (clickCount >= 34)
        {
            microwaveSpriteRenderer.sprite = sprite2;
        }
        else
        {
            microwaveSpriteRenderer.sprite = sprite1;
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
        yield return new WaitForSeconds(3f);

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
