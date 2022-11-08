using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttemptsDisplay : AbstractTextView
{
    [SerializeField]
    private GameController gameController;

    protected override void Awake()
    {
        base.Awake();
        GameController.OnGameEnd += UpdateText;
    }

    private void OnEnable()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        viewText.text = $"Attempts made: {gameController.AttemptsMade}";
    }

    private void OnDestroy()
    {
        GameController.OnGameEnd -= UpdateText;
    }
}
