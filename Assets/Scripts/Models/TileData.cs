using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TileData {
    protected bool _hasFish = false;
    protected bool _isProcessing  = false;
    public Direction direction { get; protected set;} = Direction.LEFT ;

    public Vector2 _position;
    
    static public int cost = 0;

     virtual public void wantToTake ( out Vector2? direction){
        
        direction = null;
    }
     virtual public void wantToPush ( out Vector2? direction){
        direction = null;
    }
    public bool canGive (){
        return _hasFish && !_isProcessing;
    }
    virtual public bool canReceive (){


        return Interacable && !_hasFish && !_isProcessing;
    }

    public TileData(Appliances appliance,Vector2 position){
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

    virtual public void receiveFish(){
        _isProcessing = true;
        _hasFish = true;
    }
    virtual public void pushFish(){
        _hasFish = false;
    }

    public void clearProcess(){
        this._isProcessing = false;
    }
    public static Vector2 DirectionToVector(Direction direction){
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

    void setPosition(Vector2 position ){
        this._position = position;
    }

public enum Appliances { empty, supplies, conveyor, butcher, packer, exit };
    public Appliances Appliance;
    public bool Interacable { get { return Appliance != Appliances.empty; } }

    enum ApplianceState{
        idle,
        processing,
    }
}

public enum Direction{
    UP,
    RIGHT,
    DOWN,
    LEFT
    ,
}