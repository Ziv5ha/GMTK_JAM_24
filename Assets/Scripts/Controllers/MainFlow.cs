using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFlow: MonoBehaviour {

    [SerializeField] private MenuManager MenuManagerRef;
    [SerializeField] private GameView GameViewRef;
    [SerializeField] private GameController GameControllerRef;
    [SerializeField] private ShopController ShopControllerRef;
    [SerializeField] private RentController rentControllerRef;

    private void Start() {
        InitAll();
    }

    public void InitAll() {
        GameControllerRef.Init();
        MenuManagerRef.Init();
        ShopControllerRef.Init();
    }

    public void StartGame() {
        MenuManagerRef.CloseAll();
        GameViewRef.gameObject.SetActive(true);
        GameControllerRef.CreateBoard();
        rentControllerRef.StartGame();
    }

    public void BuyAppliance(TileData.Appliances Appliance) {
        if (ShopControllerRef.TryBuyAppliances(Appliance)) {
            MenuManagerRef.CloseAll();
        }
    }
    public void BuySupplies() {
        BuyAppliance(TileData.Appliances.FishBin);
    }
    public void BuyConveyor() {
        BuyAppliance(TileData.Appliances.Conveyor);
    }
    public void BuyButcher() {
        BuyAppliance(TileData.Appliances.Scaler);
    }
    public void BuyPacker() {
        BuyAppliance(TileData.Appliances.Packer);
    }

}
