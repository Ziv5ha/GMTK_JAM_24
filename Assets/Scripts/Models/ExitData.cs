using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitData : TileData
{
    public ExitData(Direction direction,Vector2 position):base(Appliances.exit,position){
        
        base.direction = direction;
    }
    public void setDirection(Direction direction){
        this.direction = direction;
    }
    public override void wantToTake(out Vector2? direction)
    {
        direction = TileData.DirectionToVector(this.direction);    
    }
    public override void receiveFish()
    {
        
    }
}
