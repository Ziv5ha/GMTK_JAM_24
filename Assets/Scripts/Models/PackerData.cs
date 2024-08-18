using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackerData: TileData {

    public PackerData(Vector2 position) : base(Appliances.Scaler, position) {
        ProcessingDuration = 3;

    }
    public new int Cost = 10;
}
