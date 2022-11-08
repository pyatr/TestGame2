using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDisplay : AbstractTextView
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
        viewText.text = $"Time: {gameController.Timer}";
    }

    private void OnDestroy()
    {
        GameController.OnGameEnd -= UpdateText;
    }
}
