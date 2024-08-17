using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorData: TileData {

    public ConveyorData(Vector2 position) : base(Appliances.conveyor, position) { }
    public new int Cost = 5;

    public override bool CanReceive { get { return !this._hasFish; } }


    public void SetDirection(Direction direction) {
        this.Direction = direction;
    }
    public override void WantToTake(out Vector2? direction) {

        direction = this.CanReceive ? _position + TileData.DirectionToVector(this.Direction) : null;
    }
    public override void WantToPush(out Vector2? direction) {
        direction = this.CanGive ? _position - TileData.DirectionToVector(this.Direction) : null;
    }
}
