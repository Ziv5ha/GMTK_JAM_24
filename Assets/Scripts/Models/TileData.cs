using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileData {
    protected bool _hasFish = false;
    protected bool _isProcessing = false;
    protected Direction _direction =Direction.RIGHT {get }{return _direction};

    
    static public int cost = 0;

     virtual public void wantToPull ( out Direction? direction){
        direction = null;
    }
     virtual public bool canPush (){
        return false;
    }

    public enum Appliances { empty, supplies, conveyor, butcher, packer, exit };
    public Appliances Appliance = Appliances.empty;
    public bool Interacable { get { return Appliance != Appliances.empty; } }

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
}

public enum Direction{
    UP,RIGHT,DOWN,LEFT
}