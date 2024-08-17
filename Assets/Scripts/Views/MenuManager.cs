using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager: MonoBehaviour {
    //[SerializeField] private MainMenuView MainMenuRef;
    [SerializeField] private ShopView ShopViewRef;

    [SerializeField] private GameObject MainMenuRef;
    //[SerializeField] private GameObject ShopViewRef;

    public enum Menus { None, MainMenu, Shop };

    private Menus ActiveMenu {
        set {
            //MainMenuRef.gameObject.SetActive(value == Menus.MainMenu);
            ShopViewRef.gameObject.SetActive(value == Menus.Shop);
            MainMenuRef.SetActive(value == Menus.MainMenu);
            //ShopViewRef.SetActive(value == Menus.Shop);
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
}
