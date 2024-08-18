using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalerData : TileData
{

    public ScalerData(Vector2 position) : base(Appliances.Scaler,position){
        ProcessingDuration  = 3;
    }
    public override bool doProcess(){
        if(_Fish==FishData.FishState.none){
            bool busy =  base.doProcess();
            if(!busy){
                _Fish = FishData.FishState.clean;
            }
            return busy;
        }
        _isProcessing = 0;
        return isBusy;
    }
    public new int Cost = 10;
}
