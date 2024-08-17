using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitData : TileData
{
    public ExitData(Direction direction,Vector2 position):base(Appliances.exit,position){
        
        base.Direction = direction;
    }
    public void setDirection(Direction direction){
        this.Direction = direction;
    }
    public override void WantToTake(out Vector2? direction)
    {
        direction = TileData.DirectionToVector(this.Direction);    
    }
    public override void ReceiveFish()
    {
        
    }
}
