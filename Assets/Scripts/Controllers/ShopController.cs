using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopController: MonoBehaviour {


    private int _currentCoins = 0;
    public int CurrentCoins { get { return _currentCoins; } set { _currentCoins = value; UpdateCoinText(); } }
    private Dictionary<TileData.Appliances, int> _applianceCosts;
    [SerializeField] private TextMeshProUGUI[] _coinTexts;
    [SerializeField] private GameController GameControllerRef;

    const int FISH_COIN_VALUE = 1;

    public void Init() {
        UpdateCoinText();
        CreateApplianceCostDic();
    }

    public void SellFish() {
        CurrentCoins += FISH_COIN_VALUE;
    }

    private void CreateApplianceCostDic() {
        _applianceCosts = new Dictionary<TileData.Appliances, int>();
        _applianceCosts.Add(TileData.Appliances.Scaler, 10);
        _applianceCosts.Add(TileData.Appliances.Conveyor, 10);
        _applianceCosts.Add(TileData.Appliances.Packer, 10);
        _applianceCosts.Add(TileData.Appliances.FishBin, 10);
    }

    public bool TryBuyAppliances(TileData.Appliances appliance) {
        if (CurrentCoins < _applianceCosts[appliance]) return false;

        CurrentCoins -= _applianceCosts[appliance];
        GameControllerRef.ApplianceInHand = appliance;
        return true;
    }
    public void PayRent(int rent) {
        CurrentCoins -= rent;

    }
    private void UpdateCoinText() {
        // Debug.Log($"!@# Updating Coin Texts, CurrentText: {CurrentCoins}");
        string CoinString = $"{CurrentCoins}";
        while (CoinString.Length < 3) {
            CoinString = $"0{CoinString}";
        }
        foreach (var coinText in _coinTexts) {
            coinText.text = CoinString;
        }
    }

}
