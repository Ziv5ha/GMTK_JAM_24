using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorData: TileData {

    override protected float ProcessingDuration { get { return GameConstants.ConveyorProcessTime; } }


    public ConveyorData(Vector2 position) : base(Appliances.Conveyor, position) { }
    public new int Cost = 5;

    public override bool CanReceive(FishData.FishState? fish) { return !HasFish; }


    public void SetDirection(Direction direction) {
        this.direction = direction;
    }
    public override bool WantToTake(out Vector2? direction) {
        bool canReceive = this.CanReceive(FishData.FishState.clean);
        direction = canReceive ? base._position + global::TileData.DirectionToVector(this.direction) : null;
        return canReceive;

    }
    public override bool WantToPush(out Vector2? direction) {
        bool canGive = this.CanGive;
        direction = canGive ? base._position - global::TileData.DirectionToVector(this.direction) : null;
        return canGive;
    }
}
