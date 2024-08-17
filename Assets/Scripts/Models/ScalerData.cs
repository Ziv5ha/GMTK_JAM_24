using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalerData : TileData
{
    public ScalerData():base(Appliances.butcher){

    }
    public new int cost = 10;

    public override bool canPush()
    {
        return  this._hasFish&&!this._isProcessing;
    }

}
