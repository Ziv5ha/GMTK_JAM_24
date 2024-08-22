using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFlow: MonoBehaviour {

    [SerializeField] private MenuManager MenuManagerRef;
    [SerializeField] private GameView GameViewRef;
    [SerializeField] private GameController GameControllerRef;
    [SerializeField] private ShopController ShopControllerRef;
    [SerializeField] private StorageController StorageControllerRef;

    private void Start() {
        InitAll();
    }

    public void InitAll() {
        GameControllerRef.Init();
        MenuManagerRef.Init();
        ShopControllerRef.Init();

        StorageControllerRef.EEndGame += EndGame;
    }

    public void StartGame() {
        MenuManagerRef.CloseAll();
        GameViewRef.gameObject.SetActive(true);
        GameControllerRef.CreateBoard();
        StorageControllerRef.StartGame();
    }

    public void OpenShop() {
        GameViewRef.gameObject.SetActive(false);
        MenuManagerRef.GoToShop();
    }
    public void CloseShop() {
        GameViewRef.gameObject.SetActive(true);
        MenuManagerRef.CloseAll();
    }

    public void EndGame() {
        GameControllerRef.EndGame();
        MenuManagerRef.GoToEndGameMenu();
        GameViewRef.gameObject.SetActive(false);
    }

    public void OpenUpgradeMenu() {
        GameViewRef.gameObject.SetActive(false);
        MenuManagerRef.GoToUpgrade();
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
