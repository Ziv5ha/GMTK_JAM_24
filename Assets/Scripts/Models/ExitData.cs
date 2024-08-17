using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitData : TileData
{
    public ExitData(Direction direction){
        Appliance = Appliances.exit;
        base.direction = direction;
    }
    public void setDirection(Direction direction){
        this.direction = direction;
    }
    public override void wantToPull(out Direction? direction)
    {
        direction = this.direction;    
    }
}
