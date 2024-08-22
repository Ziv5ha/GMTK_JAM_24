using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackerData: TileData {

    public override bool CanReceive(FishData.FishState? fish) { return fish == FishData.FishState.clean && base.CanReceive(fish); }
    override protected float ProcessingDuration { get { return GameConstants.PackerProcessTime; } }

    public PackerData(Vector2 position) : base(Appliances.Packer, position) {
    }
    public override bool doProcess() {
        if (!HasFish) {
            _isProcessing = 0;
            return isBusy;
        }

        bool busy = base.doProcess();
        if (!busy) {
            _Fish = FishData.FishState.packed;
        }
        return busy;

    }
    public new int Cost = 10;
}
