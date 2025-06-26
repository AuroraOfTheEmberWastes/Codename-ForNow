using UnityEngine;
using TMPro;

public class MicrowaveClickUI : MonoBehaviour
{
    public TextMeshProUGUI clickText;
    public TextMeshProUGUI timerText;
    public GameObject timerObj;

    private int clickCount = 0;
    private const int maxClicks = 100;
    private float timeRemaining = 35f;
    public bool timerRunning = true;

    void Start()
    {
        if (clickText != null)
            //clickText.text = "Click count: 0";

        if (timerText != null)
            timerText.text = "Time: 60";
    }

    void Update()
    {
        if (timerRunning)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0f)
            {
                timeRemaining = 0f;
                timerRunning = false;
                Debug.Log("Time's up!");
            }

            if (timerText != null)
                timerText.text = "Time: " + Mathf.CeilToInt(timeRemaining);
        }

        if (timerRunning && WasTapped())
        {
            clickCount++;

            if (clickCount < maxClicks)
            {
                if (clickText != null)
                    clickText.text = clickCount + "/100";

                Debug.Log("click number: " + clickCount);
            }
            else if (clickCount == maxClicks)
            {
                if (clickText != null)
                    clickText.text = "Microwave Broken!";
                    timerObj.SetActive(false);

                Debug.Log("you clicked 100 times!");
            }
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
