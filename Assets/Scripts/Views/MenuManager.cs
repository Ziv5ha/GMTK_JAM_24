using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager: MonoBehaviour {
    //[SerializeField] private MainMenuView MainMenuRef;
    [SerializeField] private ShopView ShopViewRef;

    [SerializeField] private GameObject MainMenuRef;
    [SerializeField] private GameObject EndGameMenuRef;

    [SerializeField] private GameObject ShopContainer;
    [SerializeField] private GameObject UpgradeContainer;
    //[SerializeField] private GameObject ShopViewRef;

    public enum Menus { None, MainMenu, Shop, Upgrade, End };

    private Menus ActiveMenu {
        set {
            ShopViewRef.gameObject.SetActive(value == Menus.Shop || value == Menus.Upgrade);
            MainMenuRef.SetActive(value == Menus.MainMenu);
            EndGameMenuRef.SetActive(value == Menus.End);

            ShopContainer.SetActive(value == Menus.Shop);
            UpgradeContainer.SetActive(value == Menus.Upgrade);
        }
    }

    public void Init() {
        ShopViewRef.ECloseShop += CloseAll;
        GoToMainMenu();
    }

    public void CloseAll() {
        ActiveMenu = Menus.None;
    }
    public void GoToMainMenu() {
        ActiveMenu = Menus.MainMenu;
    }
    public void GoToShop() {
        ActiveMenu = Menus.Shop;
    }
    public void GoToUpgrade() {
        ActiveMenu = Menus.Upgrade;
    }
    public void GoToEndGameMenu() {
        ActiveMenu = Menus.End;
    }

}
