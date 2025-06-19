using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

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
    public Button playAgainButton;

    private int playerScore = 0;
    private int opponentScore = 0;
    private bool gameOver = false;

    public void PlayerPick(int choiceIndex)
    {
        if (gameOver) return;
        DisableButtons();
        StartCoroutine(PlayRound((RPS)choiceIndex));
    }

    IEnumerator PlayRound(RPS playerChoice)
    {
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
        scoreText.text = $"You {playerScore} : {opponentScore} Tamagotchi";

        if (playerScore == 2 || opponentScore == 2)
        {
            gameOver = true;
            resultText.text += "\nGame Over!";
            playAgainButton.gameObject.SetActive(true);
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

    public void PlayAgain()
    {
        playerScore = 0;
        opponentScore = 0;
        gameOver = false;
        resultText.text = "Make your move.";
        tamagotchiRenderer.sprite = idleSprite;
        UpdateScore();
        EnableButtons();
        playAgainButton.gameObject.SetActive(false);
    }
}
