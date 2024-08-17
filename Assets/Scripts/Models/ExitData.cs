using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitData : TileData
{

    public void setDirection(Direction direction){
        this._direction = direction;
    }
    public override void wantToPull(out Direction? direction)
    {
        direction = this._direction;    
    }
}
