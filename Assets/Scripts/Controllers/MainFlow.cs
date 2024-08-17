using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFlow: MonoBehaviour {

    [SerializeField] private MenuManager MenuManagerRef;
    [SerializeField] private GameView GameViewRef;
    [SerializeField] private GameController GameControllerRef;

    public void StartGame() {
        MenuManagerRef.CloseAll();
        GameControllerRef.CreateBoard();
    }
}
