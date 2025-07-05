using UnityEngine;
using Yarn.Unity;

public class Pleqase : MonoBehaviour
{

    public LineView theThing;


    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                theThing.OnContinueClicked();
            }
        }
    }
}
