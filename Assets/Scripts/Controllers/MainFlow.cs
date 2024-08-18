using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFlow: MonoBehaviour {

    [SerializeField] private MenuManager MenuManagerRef;
    [SerializeField] private GameView GameViewRef;
    [SerializeField] private GameController GameControllerRef;
    [SerializeField] private ShopController ShopControllerRef;

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
    }

    public void BuyAppliance(TileData.Appliances Appliance) {
        if (ShopControllerRef.TryBuyAppliances(Appliance)) {
            MenuManagerRef.CloseAll();
        }
    }
    public void BuySupplies() {
        BuyAppliance(TileData.Appliances.supplies);
    }
    public void BuyConveyor() {
        BuyAppliance(TileData.Appliances.conveyor);
    }
    public void BuyButcher() {
        BuyAppliance(TileData.Appliances.butcher);
    }
    public void BuyPacker() {
        BuyAppliance(TileData.Appliances.packer);
    }

}
