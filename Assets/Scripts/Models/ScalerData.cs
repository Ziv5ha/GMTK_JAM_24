using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalerData : TileData
{

    public ScalerData(Vector2 position) : base(Appliances.Scaler,position){
        ProcessingDuration  = 3;
    }
    public override bool CanReceive (FishData.FishState? fish){  return fish == FishData.FishState.none && base.CanReceive(fish); }


    public override bool doProcess(){
            if(!HasFish){
                _isProcessing = 0;
                return isBusy;
            }
            bool busy =  base.doProcess();
            if(!busy){
                _Fish = FishData.FishState.clean;
            }
            return busy;
    }
    public new int Cost = 10;
}
