using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class RentController : MonoBehaviour
{
    private int _baseRent = 20;
    private int _rentTime = 5;
    [SerializeField] private Slider RentbarSlider;
    [SerializeField] private ShopController ShopControllerRef;
    // Start is called before the first frame update
    
    public  void UpdateRentBar(int round){
        float remainder = round % (_rentTime);

        float frac =  ( _rentTime - remainder -1) / _rentTime;
        
        Debug.Log($"{round} {remainder} {_rentTime-remainder} {(_rentTime-remainder)/_rentTime}");
        
        RentbarSlider.value = frac;
        if(frac == 0f){
            ShopControllerRef.PayRent(_baseRent);
        }


    }

}