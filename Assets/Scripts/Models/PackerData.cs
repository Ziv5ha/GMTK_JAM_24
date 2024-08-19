using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackerData: TileData {

    public override bool CanReceive (FishData.FishState? fish){  return fish == FishData.FishState.clean && base.CanReceive(fish); }

    public PackerData(Vector2 position) : base(Appliances.Packer, position) {
        ProcessingDuration = 3;

    }
    public override bool doProcess(){
            if(!HasFish){
                _isProcessing = 0;
                return isBusy;
            }

            bool busy =  base.doProcess();
            if(!busy){
                _Fish = FishData.FishState.packed;
            }
            return busy;
     
    }
    public new int Cost = 10;
}
