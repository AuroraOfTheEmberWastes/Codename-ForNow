using UnityEngine;
using TMPro;

public class MoodUI : MonoBehaviour
{
    public MoodManager moodManager;
    public TMP_Text hungerText;
    public TMP_Text happinessText;

    void Update()
    {
        if (moodManager == null) return;

        hungerText.text = $"Hunger: {moodManager.hunger}/3";
        happinessText.text = $"Happiness: {moodManager.happiness}/3";
    }
}
