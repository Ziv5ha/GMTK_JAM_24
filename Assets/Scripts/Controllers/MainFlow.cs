using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFlow: MonoBehaviour {

    [SerializeField] private MenuManager MenuManagerRef;
    [SerializeField] private GameView GameViewRef;
    [SerializeField] private GameController GameControllerRef;

    private void Start() {
        InitAll();
    }

    public void InitAll() {
        MenuManagerRef.Init();
    }

    public void StartGame() {
        MenuManagerRef.CloseAll();
        GameViewRef.gameObject.SetActive(true);
        GameControllerRef.CreateBoard();
    }

}
