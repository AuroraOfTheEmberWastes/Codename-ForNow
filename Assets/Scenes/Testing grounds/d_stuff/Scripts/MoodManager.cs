using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoodManager : MonoBehaviour
{
    [Range(0, 3)] public int hunger = 3;
    [Range(0, 3)] public int happiness = 3;

    public AudioSource eatingSound;

    public SpriteRenderer[] characterSpriteRenderers;
    public Sprite normalSprite;
    public Sprite angrySprite;
    public Sprite sadSprite;
    public Sprite conflictedSprite;
    public Sprite eatingSprite;

    public RawImage[] heartRawImages;
    public RawImage[] burgerRawImages;

    private bool feedRunning = false;
    private bool canFeed = true;
    public bool storymode = false;
    public InteractionObject D2S2;

    [HideInInspector]
    public bool overrideSprite = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) GetHungry();
        if (Input.GetKeyDown(KeyCode.J)) EatFood();
        if (Input.GetKeyDown(KeyCode.K)) GetSad();
        if (Input.GetKeyDown(KeyCode.L)) CheerUp();

        UpdateMoodSprite();
        UpdateUIHeartsAndBurgers();
    }

    public void GetHungry() => hunger = Mathf.Max(hunger - 1, 0);
    public void EatFood() => hunger = Mathf.Min(hunger + 1, 3);
    public void GetSad() => happiness = Mathf.Max(happiness - 1, 0);
    public void CheerUp() => happiness = Mathf.Min(happiness + 1, 3);

    private void UpdateMoodSprite()
    {
        if (overrideSprite || feedRunning) return;

        Sprite targetSprite;

        if (hunger == 1 && happiness == 1)
            targetSprite = conflictedSprite;
        else if (hunger == 1)
        {
            targetSprite = angrySprite;
            //madSound.Play();
        }
        else if (happiness == 1)
        {
            targetSprite = sadSprite;
            //sadSound.Play();
        }
        else 
        { 
            targetSprite = normalSprite;
            //happySound.Play();
        }
          

        foreach (var sr in characterSpriteRenderers)
        {
            sr.sprite = targetSprite;
        }
    }
    

    private void UpdateUIHeartsAndBurgers()
    {
        for (int i = 0; i < heartRawImages.Length; i++)
        {
            heartRawImages[i].gameObject.SetActive(i < happiness);
        }

        for (int i = 0; i < burgerRawImages.Length; i++)
        {
            burgerRawImages[i].gameObject.SetActive(i < hunger);
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

        foreach (var sr in characterSpriteRenderers)
        {
            sr.sprite = eatingSprite;
        }

        yield return new WaitForSeconds(2f);

        foreach (var sr in characterSpriteRenderers)
        {
            sr.sprite = normalSprite;
        }

        feedRunning = false;
        hunger = Mathf.Min(hunger + 1, 3);
        Debug.Log("tama fed.");
        eatingSound.Play();
        if (storymode)
        {
            D2S2.OnEventTrigger();
        }

        yield return new WaitForSeconds(30f);
        canFeed = true;
        Debug.Log("cooldown done");
    }
}
