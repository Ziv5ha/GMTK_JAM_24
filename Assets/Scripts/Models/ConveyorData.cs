using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorData : TileData
{

    public ConveyorData():base(Appliances.conveyor){}    
    public new int cost = 5;

    public override bool canPush()
    {
        return  this._hasFish;
    }

    public void setDirection(Direction direction){
        this.direction = direction;
    }
    public override void wantToPull(out Direction? direction)
    {
        direction = !this._hasFish? this.direction: null;    
    }
}
