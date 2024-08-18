using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopController: MonoBehaviour {


    private int _currentCoins = 999;
    public int CurrentCoins { get { return _currentCoins; } set { _currentCoins = value; } }
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
        UpdateCoinText();
    }

    private void CreateApplianceCostDic() {
        _applianceCosts = new Dictionary<TileData.Appliances, int>();
        _applianceCosts.Add(TileData.Appliances.butcher, 10);
        _applianceCosts.Add(TileData.Appliances.conveyor, 10);
        _applianceCosts.Add(TileData.Appliances.packer, 10);
        _applianceCosts.Add(TileData.Appliances.supplies, 10);
    }

    public bool TryBuyAppliances(TileData.Appliances appliance) {
        if (CurrentCoins < _applianceCosts[appliance]) return false;

        CurrentCoins -= _applianceCosts[appliance];
        // TODO put appliance in "hand"
        GameControllerRef.ApplianceInHand = appliance;
        UpdateCoinText();
        return true;
    }
    public void UpdateCoinText() {
        Debug.Log($"!@# Updating Coin Texts, CurrentText: {CurrentCoins}");
        foreach (var coinText in _coinTexts) {
            coinText.text = CurrentCoins.ToString();
        }
    }

}
