using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TileData {

    protected FishData.FishState? _Fish;
    protected int _isProcessing = 0;
    protected int ProcessingDuration = 1;
    public Direction direction { get; protected set; } = Direction.LEFT;

    public FishData.FishState? fish { get { return _Fish; } }

    public Vector2 _position;

    static public int Cost = 0;
    public string id { get { return $"{Appliance} ({_position})"; } }
    virtual public void WantToTake(out Vector2? direction) {

        direction = null;
    }
    virtual public void WantToPush(out Vector2? direction) {
        direction = null;
    }
    public bool isBusy {
        get { return _isProcessing > 0; }
    }
    public int processingLeft {
        get { return _isProcessing; }
    }

    public bool hasFish { get { return _Fish != null; } }

    public bool CanGive { get { return hasFish && !isBusy; } }

    virtual public bool CanReceive { get { return Interacable && !hasFish && !isBusy; } }


    public TileData(Appliances appliance, Vector2 position) {
        _position = position;
        Appliance = appliance;
    }

    public TileData(Appliances? applianceInHand, Vector2 pos) {
    }

    public string ToShortString() {
        switch (Appliance) {
            case Appliances.exit:
                return "X";
            case Appliances.supplies:
                return "s";
            case Appliances.butcher:
                return "b";
            case Appliances.packer:
                return "p";
            case Appliances.conveyor:
                return "-";
            case Appliances.empty:
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
        if (!hasFish) {
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

    public enum Appliances { empty, supplies, conveyor, butcher, packer, exit };
    public Appliances Appliance;
    public bool Interacable { get { return Appliance != Appliances.empty; } }

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