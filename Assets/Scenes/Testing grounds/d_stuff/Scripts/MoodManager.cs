using UnityEngine;
using System.Collections;

public class MoodManager : MonoBehaviour
{
    [Range(0, 3)] public int hunger = 3;
    [Range(0, 3)] public int happiness = 3;

    public SpriteRenderer characterSpriteRenderer;
    public Sprite normalSprite;
    public Sprite angrySprite;
    public Sprite sadSprite;
    public Sprite conflictedSprite;
    public Sprite eatingSprite;
    private bool feedRunning = false;

    private bool canFeed = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) GetHungry();
        if (Input.GetKeyDown(KeyCode.J)) EatFood();
        if (Input.GetKeyDown(KeyCode.K)) GetSad();
        if (Input.GetKeyDown(KeyCode.L)) CheerUp();

        UpdateMoodSprite();
    }

    public void GetHungry() => hunger = Mathf.Max(hunger - 1, 0);
    public void EatFood() => hunger = Mathf.Min(hunger + 1, 3);
    public void GetSad() => happiness = Mathf.Max(happiness - 1, 0);
    public void CheerUp() => happiness = Mathf.Min(happiness + 1, 3);

    private void UpdateMoodSprite()
    {
        if (feedRunning==false)
        {
            if (hunger == 1 && happiness == 1)
            {
                characterSpriteRenderer.sprite = conflictedSprite;
            }
            else if (hunger == 1)
            {
                characterSpriteRenderer.sprite = angrySprite;
            }
            else if (happiness == 1)
            {
                characterSpriteRenderer.sprite = sadSprite;
            }
            else
            {
                characterSpriteRenderer.sprite = normalSprite;
            }
        }
    }

    public void FeedTama()
    {
        Debug.Log("FeedTama() called - canFeed: " + canFeed);

        if (canFeed)
        {
            StartCoroutine(FeedTamaRoutine());
        }
        else
        {
            Debug.Log("cant feed yet luh twin");
        }
    }

    private IEnumerator FeedTamaRoutine()
    {
        feedRunning = true;
        canFeed = false;

        characterSpriteRenderer.sprite = eatingSprite;
        yield return new WaitForSeconds(2f);
        characterSpriteRenderer.sprite = normalSprite;
        feedRunning = false;

        hunger = Mathf.Min(hunger + 1, 3);
        Debug.Log("tama fed.");

        // cooldown
        yield return new WaitForSeconds(30f);
        canFeed = true;
        Debug.Log("cooldown done");
    }
}
