using System;
using System.Collections.Generic;

public class GameConstants {

    static public List<int> FishSoldStages = new List<int> { 10, 30, 50, 80, 110, 140, 170, 220, 280, 350, 450, 600, 800, 1000 };

    static public float RoundDuration = 0.5f;
    static public int TotalFishSold = 0;

    // Storage
    static public float CurrentFishInStorage = 100;
    static public float StorageCapacity = 100;
    static public int ShipmentTime = 10;
    static public int ShipmentSize = 6;


    // Process Time
    static public float FishBinProcessTime;
    static public float ConveyorProcessTime = 2;
    static public float ScalerProcessTime = 3;
    static public float PackerProcessTime = 3;

}