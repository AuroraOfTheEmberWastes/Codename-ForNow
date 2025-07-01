using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class YarnCommandBridge : MonoBehaviour
{
    public Disabler disabler;
    public MoodManager moodManager;


    [YarnCommand("go_to")]
    public void GoTo(string roomName)
    {
        switch (roomName.ToLower())
        {
            case "kitchen": disabler.GoToKitchen(); break;
            case "bedroom": disabler.GoToBedroom(); break;
            case "playroom": disabler.PlayRPS(); break;
            case "microwave": disabler.PlayMicrowave(); break;
            case "cables": disabler.PlayCables(); break;
            default: Debug.LogWarning("Unknown room: " + roomName); break;
        }
    }

    [YarnCommand("enable_object")]
    public void EnableObject(string objectName)
    {
        GameObject target = GameObject.Find(objectName);
        if (target != null)
        {
            target.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"enable_object: GameObject '{objectName}' not found.");
        }
    }

    [YarnCommand("disable_object")]
    public void DisableObject(string objectName)
    {
        GameObject target = GameObject.Find(objectName);
        if (target != null)
        {
            target.SetActive(false);
        }
        else
        {
            Debug.LogWarning($"disable_object: GameObject '{objectName}' not found.");
        }
    }

    [YarnCommand("set_happiness")]
    public void SetHappiness(int value)
    {
        moodManager.happiness = Mathf.Clamp(value, 0, 3);
        Debug.Log($"Happiness set to {moodManager.happiness}");
    }

    [YarnCommand("increase_happiness")]
    public void IncreaseHappiness()
    {
        moodManager.happiness = Mathf.Min(moodManager.happiness + 1, 3);
        Debug.Log($"Happiness increased to {moodManager.happiness}");
    }

    [YarnCommand("decrease_happiness")]
    public void DecreaseHappiness()
    {
        moodManager.happiness = Mathf.Max(moodManager.happiness - 1, 0);
        Debug.Log($"Happiness decreased to {moodManager.happiness}");
    }

    [YarnCommand("set_hunger")]
    public void SetHunger(int value)
    {
        moodManager.hunger = Mathf.Clamp(value, 0, 3);
        Debug.Log($"Hunger set to {moodManager.hunger}");
    }

    [YarnCommand("increase_hunger")]
    public void IncreaseHunger()
    {
        moodManager.hunger = Mathf.Min(moodManager.hunger + 1, 3);
        Debug.Log($"Hunger increased to {moodManager.hunger}");
    }

    [YarnCommand("decrease_hunger")]
    public void DecreaseHunger()
    {
        moodManager.hunger = Mathf.Max(moodManager.hunger - 1, 0);
        Debug.Log($"Hunger decreased to {moodManager.hunger}");
    }

    [YarnCommand("enable_after_dialogue")]
    public void EnableAfterDialogue()
    {
        disabler.enableAfterDialogue();
    }

    [YarnCommand("disable_during_dialogue")]
    public void DisableDuringDialogue(string _ = "")
    {
        disabler.disableDuringDialogue();
    }






    

}
