using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;



public class StorageController: MonoBehaviour {
    [SerializeField] private Slider RentbarSlider;
    [SerializeField] private MainFlow MainFlowRef;

    public Action EEndGame;

    private float curVal = 1f;
    private float nextVal = 1f;
    private float lerpTime = GameConstants.RoundDuration;
    private bool gameStarted = false;

    public void StartGame() {
        gameStarted = true;
    }


    private void Update() {
        if (!gameStarted) return;

        RentbarSlider.value = Mathf.Lerp(curVal, nextVal, lerpTime);

        if (lerpTime < GameConstants.RoundDuration) {
            lerpTime += Time.deltaTime;
        }

    }

    private void DoLerp(float target) {
        curVal = RentbarSlider.value;
        nextVal = target;
        lerpTime = 0;
    }

    public void UpdateStorage(int round) {

        bool ItsShipmentTime = round % (GameConstants.ShipmentTime + 1) == 0;
        if (ItsShipmentTime) {
            //Debug.Log($"!@# It's shipment time! shipping {GameConstants.ShipmentSize} of {GameConstants.CurrentFishInStorage} currently in storage.");
            GameConstants.CurrentFishInStorage -= GameConstants.ShipmentSize;
            if (GameConstants.CurrentFishInStorage <= 0) {
                EEndGame();
            }
        }

        float frac = GameConstants.CurrentFishInStorage / GameConstants.StorageCapacity;

        DoLerp(frac);



    }

}