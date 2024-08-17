using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishBinData : TileData
{
    public FishBinData():base(Appliances.supplies){

    }
    public new int cost = 10;
    public override bool canPush()
    {
        return true;
    }
}
