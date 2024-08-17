using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopView: MonoBehaviour {
    public Action ECloseShop;
    [SerializeField] private Animator AnimatorRef;

    public void CloseShop() {
        AnimatorRef.SetTrigger("CloseShop");
        Invoke("CloseShopCorotine", 1);
    }
    private void CloseShopCorotine() {
        ECloseShop();
    }
    //private IEnumerator CloseShopCorotine() {
    //    CancelInvoke("CloseShopCorotine");
    //    ECloseShop();
    //    yield return null;
    //}
}
