using System.Collections.Generic;
using UnityEngine;

public class Disabler : MonoBehaviour
{
    [Header("Bedroom")]
    public List<GameObject> bedroomEnable;
    public List<GameObject> bedroomDisable;

    [Header("Playroom")]
    public List<GameObject> playroomEnable;
    public List<GameObject> playroomDisable;

    [Header("Kitchen")]
    public List<GameObject> kitchenEnable;
    public List<GameObject> kitchenDisable;
    [Header("Cables")]
    public List<GameObject> cableEnable;
    public List<GameObject> cableDisable;
    [Header("RPS")]
    public List<GameObject> rpsEnable;
    public List<GameObject> rpsDisable;
    [Header("Microwave")]
    public List<GameObject> microwaveEnable;
    public List<GameObject> microwaveDisable;

    [Header("Dialogue")]
    public List<GameObject> dialogueEnable;
    public List<GameObject> dialogueDisable;

    public void GoToBedroom()
    {
        foreach (GameObject obj in bedroomDisable)
        {
            if (obj != null)
                obj.SetActive(false);
        }
        foreach (GameObject obj in bedroomEnable)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }

    public void GoToPlayroom()
    {
        foreach (GameObject obj in playroomDisable)
        {
            if (obj != null)
                obj.SetActive(false);
        }
        foreach (GameObject obj in playroomEnable)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }

    public void GoToKitchen()
    {
        foreach (GameObject obj in kitchenDisable)
        {
            if (obj != null)
                obj.SetActive(false);
        }
        foreach (GameObject obj in kitchenEnable)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }

    public void PlayRPS()
    {
        foreach (GameObject obj in rpsDisable)
        {
            if (obj != null)
                obj.SetActive(false);
        }
        foreach (GameObject obj in rpsEnable)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }

    public void PlayCables()
    {
        foreach (GameObject obj in cableDisable)
        {
            if (obj != null)
                obj.SetActive(false);
        }
        foreach (GameObject obj in cableEnable)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }

    public void PlayMicrowave()
    {
        foreach (GameObject obj in microwaveDisable)
        {
            if (obj != null)
                obj.SetActive(false);
        }
        foreach (GameObject obj in microwaveEnable)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }
    
    public void disableDuringDialogue()
    {
        foreach (GameObject obj in dialogueDisable)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }

    public void enableAfterDialogue()
    {
        foreach (GameObject obj in dialogueEnable)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }
}