using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class TileView: MonoBehaviour {
    [SerializeField] private Color _baseColor, _offsetColor;
    public SpriteRenderer Renderer;
    public SpriteRenderer ApplianceRenderer;
    public SpriteRenderer FishRenderer;
    public Animator appAnimator;
    [SerializeField] private GameObject _highlight;

    public Vector2 Pos;

    public applianceSprite[] applianceSpriteList;
    public fishSprite[] fishSpriteList;

    public Action<Vector2> ETileClicked;
    public Action<Vector2> ETileRightClicked;

    public void Init(bool isOffset) {
        Renderer.color = isOffset ? _offsetColor : _baseColor;
    }

    void OnMouseEnter() {
        _highlight.SetActive(true);
    }

    void OnMouseExit() {
        _highlight.SetActive(false);
    }
    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) ETileClicked(Pos);
        if (Input.GetMouseButtonDown(1)) ETileRightClicked(Pos);
    }

    public void UpdateFish(FishData.FishState? fish) {
        if (fish is null) {
            FishRenderer.sprite = null;
            return;
        }

        fishSprite relevantSprite = Array.Find(fishSpriteList, (t) => {
            return t.state == fish;
        });
        if (relevantSprite != null) {
            FishRenderer.sprite = relevantSprite.sprite;
        }

    }
    public void RotateAppliance(TileData data) {
        UpdateAnim(data,data.isBusy);
    }

    public void UpdateAnim(TileData app, bool isBusy) {

        string state = isBusy ? "busy" : "idle";
        string key = $"{app.Appliance}_{state}";
        if(app.Appliance == TileData.Appliances.Conveyor){

            key = $"{app.Appliance}_{app.direction}";
        }
        appAnimator.enabled = true;
        if (!appAnimator.GetCurrentAnimatorStateInfo(0).IsName(key)) {
            appAnimator.Play(key);

        }

    }


    public void UpdateSprite(TileData.Appliances appliance,Direction? direction){
        applianceSprite relevantSprite = Array.Find(applianceSpriteList, (t) => {
            return t.appliance == appliance;
        });
        // Debug.Log($"!@# found relevantSprite for {appliance.Appliance}: {relevantSprite != null}");
        if (relevantSprite != null) {
            ApplianceRenderer.sprite = relevantSprite.sprite;
            ApplianceRenderer.flipX = direction == Direction.RIGHT;
        }
    }

    [Serializable]
    public class applianceSprite {

        public Sprite sprite;
        public TileData.Appliances appliance;
    }
    [Serializable]
    public class fishSprite {

        public Sprite sprite;
        public FishData.FishState state;
    }
}