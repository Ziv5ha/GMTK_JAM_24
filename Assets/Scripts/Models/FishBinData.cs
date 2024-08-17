using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishBinData: TileData {
    public FishBinData(Vector2 position) : base(Appliances.supplies, position) {
        _hasFish = true;
    }
    public new int Cost = 10;
    public override void PushFish() {
        return;
    }
}
