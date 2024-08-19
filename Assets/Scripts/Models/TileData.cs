using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TileData {

    protected FishData.FishState? _Fish;
    protected int _isProcessing = 0;
    protected int ProcessingDuration = 1;
    public Direction direction { get; set; } = Direction.LEFT;

    public FishData.FishState? fish { get { return _Fish; } set { _Fish = value; } }

    public Vector2 _position;

    static public int Cost = 0;
    public string id { get { return $"{Appliance} ({_position})"; } }
    virtual public bool WantToTake(out Vector2? direction) {
        direction = null;
        return false;

    }
    virtual public bool WantToPush(out Vector2? direction) {
        direction = null;
        return false;
    }
    public bool isBusy {
        get { return _isProcessing > 0; }
    }
    public int processingLeft {
        get { return _isProcessing; }
    }

    public bool HasFish { get { return _Fish != null; } }

    virtual public bool CanGive { get { return HasFish && !isBusy; } }

    virtual public bool CanReceive (FishData.FishState? fish){ return Interacable && !HasFish && !isBusy;  }


    public TileData(Appliances appliance, Vector2 position) {
        _position = position;
        Appliance = appliance;
    }

    public TileData(Appliances? applianceInHand, Vector2 pos) {
    }

    public string ToShortString() {
        switch (Appliance) {
            case Appliances.Exit:
                return "X";
            case Appliances.FishBin:
                return "s";
            case Appliances.Scaler:
                return "b";
            case Appliances.Packer:
                return "p";
            case Appliances.Conveyor:
                return "-";
            case Appliances.Empty:
            default:
                return " ";
        }
    }

    public override string ToString() {
        return $"{Appliance} ({_position})";
    }

    virtual public void ReceiveFish(FishData.FishState fish) {
        _isProcessing = ProcessingDuration;
        _Fish = fish;
    }
    virtual public FishData.FishState PushFish() {
        if (!HasFish) {
            throw new System.Exception($"{Appliance} at {_position} Tried to give a fish they dont have");
        }
        FishData.FishState fish = _Fish.Value;
        _Fish = null;
        return fish;
    }

    virtual public bool doProcess() {
        if (isBusy) {
            _isProcessing -= 1;
        }
        return isBusy;
    }
    public static Vector2 DirectionToVector(Direction direction) {
        switch (direction) {
            case Direction.UP:
                return Vector2.up;
            case Direction.DOWN:
                return Vector2.down;
            case Direction.LEFT:
                return Vector2.left;
            case Direction.RIGHT:
                return Vector2.right;
            default:
                throw new System.Exception();
        }
    }

    void SetPosition(Vector2 position) {
        this._position = position;
    }

    public enum Appliances { Empty, FishBin, Conveyor, Scaler, Packer, Exit };
    public Appliances Appliance;
    public bool Interacable { get { return Appliance != Appliances.Empty; } }

    enum ApplianceState {
        idle,
        processing,
    }
}

public enum Direction {
    UP,
    RIGHT,
    DOWN,
    LEFT
    ,
}