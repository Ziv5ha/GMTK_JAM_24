using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class RentController : MonoBehaviour
{
    private int _baseRent = 20;
    private int _rentTime = 5;
    [SerializeField] private Slider RentbarSlider;
    [SerializeField] private ShopController ShopControllerRef;
    // Start is called before the first frame update
    private float curVal = 1f;
    private float nextVal = 1f;
    private float lerpTime = GameConstants.RoundDuration;
    private bool gameStarted = false;

      public void StartGame() {
        gameStarted = true;
    }


    private void Update() {
        if(!gameStarted){
            return;
        }
        
        RentbarSlider.value = nextVal == 1 ? 1 : Mathf.Lerp(curVal,nextVal,lerpTime);
        
        if(lerpTime<GameConstants.RoundDuration){
            lerpTime+=Time.deltaTime;
        }
        
    }

    private void DoLerp(float target){
        curVal = RentbarSlider.value ;
        nextVal =target ;
        lerpTime = 0;
    }

    public  void UpdateRentBar(int round){
        if(nextVal == 0f){
            ShopControllerRef.PayRent(_baseRent);
        }
        float remainder = round % (_rentTime+1);

        float frac =  ( _rentTime - remainder) / _rentTime;
        
        
        DoLerp(frac);
        


    }

}