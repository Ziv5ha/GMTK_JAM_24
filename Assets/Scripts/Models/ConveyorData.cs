using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorData : TileData
{

    public ConveyorData(Vector2 position):base(Appliances.conveyor,position){}    
    public new int cost = 5;

    public override bool canReceive()
    {
        
        return  !this._hasFish;
    }


    public void setDirection(Direction direction){
        this.direction = direction;
    }
    public override void wantToTake(out Vector2? direction)
    {
        
        direction = this.canReceive()? _position+TileData.DirectionToVector(this.direction) : null;    
    }
    public override void wantToPush(out Vector2? direction)
    {
        direction = this.canGive()? _position-TileData.DirectionToVector(this.direction): null;    
    }
}
