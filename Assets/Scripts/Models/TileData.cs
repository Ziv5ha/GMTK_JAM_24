using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TileData {
    protected bool _hasFish = false;
    protected bool _isProcessing = false;
    public Direction Direction { get; protected set; } = Direction.LEFT;

    public Vector2 _position;

    static public int Cost = 0;

    virtual public void WantToTake(out Vector2? direction) {

        direction = null;
    }
    virtual public void WantToPush(out Vector2? direction) {
        direction = null;
    }
    public bool CanGive { get { return _hasFish && !_isProcessing; } }
    virtual public bool CanReceive { get { return Interacable && !_hasFish && !_isProcessing; } }

    public TileData(Appliances appliance, Vector2 position) {
        _position = position;
        Appliance = appliance;
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
        return Appliance.ToString();
    }

    virtual public void ReceiveFish() {
        _isProcessing = true;
        _hasFish = true;
    }
    virtual public void PushFish() {
        _hasFish = false;
    }

    public void ClearProcess() {
        this._isProcessing = false;
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