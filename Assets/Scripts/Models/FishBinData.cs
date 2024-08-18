using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishBinData: TileData {
    public FishBinData(Vector2 position) : base(Appliances.supplies, position) {
    }
    
    public new int Cost = 10;
    override public bool CanGive { get { return true;} }

    public override FishData.FishState PushFish() {
        return FishData.FishState.none;
    }
}
