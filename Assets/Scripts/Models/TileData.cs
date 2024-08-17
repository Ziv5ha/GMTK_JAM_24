using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData {
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
