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
    public void RotateAppliance(Direction d) {
        switch (d) {
            case Direction.UP:
            case Direction.DOWN:
                ApplianceRenderer.flipX = d == Direction.UP;
                ApplianceRenderer.flipY = d == Direction.UP;
                ApplianceRenderer.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case Direction.RIGHT:
            case Direction.LEFT:
                ApplianceRenderer.flipX = d == Direction.LEFT;
                ApplianceRenderer.flipY = false;
                ApplianceRenderer.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            default:
                break;
        }
    }

    public void UpdateAnim(TileData.Appliances app,bool isBusy){
        string state = isBusy? "busy" : "idle";
        string key = $"{app}_{state}";
        
        appAnimator.enabled = true;
        if(!appAnimator.GetCurrentAnimatorStateInfo(0).IsName(key)){
            Debug.Log("playing "+ key);
            appAnimator.Play(key);

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