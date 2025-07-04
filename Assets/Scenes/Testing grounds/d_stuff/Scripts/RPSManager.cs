using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class RockPaperScissorsManager : MonoBehaviour
{
    public enum RPS { Rock, Paper, Scissors }

    [Header("Sprites")]
    public Sprite idleSprite;
    public Sprite rockSprite, paperSprite, scissorsSprite;
    public Sprite winSprite, loseSprite;

    [Header("Tamagotchi")]
    public SpriteRenderer tamagotchiRenderer;

    [Header("UI")]
    public TMP_Text resultText;
    public TMP_Text scoreText;
    public Button[] choiceButtons;

    private int playerScore = 0;
    private int opponentScore = 0;
    private bool gameOver = false;

    public List<GameObject> enableObj;
    public List<GameObject> disableObj;
    public InteractionObject D2S4;
    public InteractionObject D2S5;
    public bool storymode = false;
    public int timesPlayed = 0;

    public MoodManager moodManager;

    void OnEnable()
    {
        RestartGame();
    }

    public void PlayerPick(int choiceIndex)
    {
        if (gameOver) return;
        DisableButtons();
        StartCoroutine(PlayRound((RPS)choiceIndex));
    }

    IEnumerator PlayRound(RPS playerChoice)
    {
        if (moodManager != null) moodManager.overrideSprite = true;

        resultText.text = "You chose " + playerChoice.ToString() + "...";
        tamagotchiRenderer.sprite = idleSprite;

        yield return new WaitForSeconds(0.5f);

        RPS opponentChoice = (RPS)Random.Range(0, 3);
        tamagotchiRenderer.sprite = GetSprite(opponentChoice);
        resultText.text += $"\nTamagotchi chose {opponentChoice.ToString()}...";

        yield return new WaitForSeconds(1.5f);

        ResolveRound(playerChoice, opponentChoice);
        UpdateScore();

        if (!gameOver)
        {
            yield return new WaitForSeconds(1.5f);
            resultText.text = "Make your move.";
            tamagotchiRenderer.sprite = idleSprite;
            EnableButtons();

            if (moodManager != null) moodManager.overrideSprite = false;
        }
    }

    void ResolveRound(RPS player, RPS opponent)
    {
        if (player == opponent)
        {
            resultText.text += "\nIt's a draw!";
            return;
        }

        bool playerWins =
            (player == RPS.Rock && opponent == RPS.Scissors) ||
            (player == RPS.Scissors && opponent == RPS.Paper) ||
            (player == RPS.Paper && opponent == RPS.Rock);

        if (playerWins)
        {
            resultText.text += "\nYou win this round!";
            playerScore++;
            tamagotchiRenderer.sprite = loseSprite;
        }
        else
        {
            resultText.text += "\nYou lost this round!";
            opponentScore++;
            tamagotchiRenderer.sprite = winSprite;
        }
    }

    void UpdateScore()
    {
        scoreText.text = $"{playerScore} : {opponentScore}";

        if (playerScore == 2 || opponentScore == 2)
        {
            gameOver = true;
            timesPlayed++;
            resultText.text += "\nGame Over!";
            StartCoroutine(endingGame());
            if (storymode)
            {
                if (timesPlayed == 1)
                    D2S4.OnEventTrigger();
                else if (timesPlayed == 2)
                    D2S5.OnEventTrigger();
            }

            if (moodManager != null) moodManager.overrideSprite = false;
        }
    }

    void DisableButtons()
    {
        foreach (var btn in choiceButtons)
            btn.interactable = false;
    }

    void EnableButtons()
    {
        foreach (var btn in choiceButtons)
            btn.interactable = true;
    }

    Sprite GetSprite(RPS choice)
    {
        switch (choice)
        {
            case RPS.Rock: return rockSprite;
            case RPS.Paper: return paperSprite;
            case RPS.Scissors: return scissorsSprite;
            default: return idleSprite;
        }
    }

    private void RestartGame()
    {
        playerScore = 0;
        opponentScore = 0;
        gameOver = false;
        resultText.text = "Make your move.";
        tamagotchiRenderer.sprite = idleSprite;
        UpdateScore();
        EnableButtons();

        if (moodManager != null) moodManager.overrideSprite = false;
    }

    private IEnumerator endingGame()
    {
        yield return new WaitForSeconds(1f);
        moodManager.happiness++;
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
}
