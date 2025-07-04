using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodManager : MonoBehaviour
{
    [Header("Mood Variables")]
    public int hunger = 0;
    public int happiness = 0;

    [Header("Mood Sprites")]
    public Sprite normalSprite;
    public Sprite angrySprite;
    public Sprite sadSprite;
    public Sprite conflictedSprite;

    [Header("Sprite Renderers")]
    public List<SpriteRenderer> characterSpriteRenderers = new List<SpriteRenderer>();

    [Header("Feeding Settings")]
    public float feedDuration = 3f;
    public GameObject foodObject;
    public Transform foodSpawnPoint;

    [HideInInspector]
    public bool overrideSprite = false;

    private bool feedRunning = false;

    void Start()
    {
        UpdateMoodSprite();
    }

    public void IncreaseHunger()
    {
        hunger = Mathf.Clamp(hunger + 1, 0, 1);
        UpdateMoodSprite();
    }

    public void DecreaseHunger()
    {
        hunger = Mathf.Clamp(hunger - 1, 0, 1);
        UpdateMoodSprite();
    }

    public void IncreaseHappiness()
    {
        happiness = Mathf.Clamp(happiness + 1, 0, 1);
        UpdateMoodSprite();
    }

    public void DecreaseHappiness()
    {
        happiness = Mathf.Clamp(happiness - 1, 0, 1);
        UpdateMoodSprite();
    }

    public void Feed()
    {
        if (!feedRunning)
        {
            StartCoroutine(FeedRoutine());
        }
    }

    private IEnumerator FeedRoutine()
    {
        feedRunning = true;

        GameObject spawnedFood = null;
        if (foodObject != null && foodSpawnPoint != null)
        {
            spawnedFood = Instantiate(foodObject, foodSpawnPoint.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(feedDuration);

        if (spawnedFood != null)
        {
            Destroy(spawnedFood);
        }

        DecreaseHunger();

        feedRunning = false;
    }

    private void UpdateMoodSprite()
    {
        if (overrideSprite || feedRunning) return;

        Sprite targetSprite;

        if (hunger == 1 && happiness == 1)
            targetSprite = conflictedSprite;
        else if (hunger == 1)
            targetSprite = angrySprite;
        else if (happiness == 1)
            targetSprite = sadSprite;
        else
            targetSprite = normalSprite;

        foreach (var sr in characterSpriteRenderers)
        {
            sr.sprite = targetSprite;
        }
    }
}
