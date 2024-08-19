using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitData: TileData {
    public Action ESellFish;
    public ExitData(Direction direction, Vector2 position) : base(Appliances.Exit, position) {

        base.direction = direction;
    }
    public void SetDirection(Direction direction) {
        this.direction = direction;
    }
    public override void WantToTake(out Vector2? position) {

        position = _position + DirectionToVector(direction);
    }

    public override bool CanReceive(FishData.FishState? fish)
    {
        Debug.Log($"EXXX1 {fish}");
        return fish == FishData.FishState.packed;
    }
    public override void ReceiveFish(FishData.FishState fish) {
        // Debug.Log($"!@# ExitData RecievedFish");
        if (fish == FishData.FishState.packed) ESellFish();
        _Fish = null;
    }
}
