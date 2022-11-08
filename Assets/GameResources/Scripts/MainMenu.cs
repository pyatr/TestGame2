using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject menuObject;
    [SerializeField]
    private GameController gameController;

    public void StartGame()
    {
        menuObject.SetActive(false);
        gameController.StartGame();        
    }
}
