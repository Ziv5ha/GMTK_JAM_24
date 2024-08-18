using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class TileView: MonoBehaviour {
    [SerializeField] private Color _baseColor, _offsetColor;
    public SpriteRenderer Renderer;
    public SpriteRenderer ApplianceRenderer;
    public SpriteRenderer FishRenderer;
    public Animator animator;
    [SerializeField] private GameObject _highlight;

    public Vector2 Pos;

    public applianceSprite[] applianceSpriteList;
    public fishSprite[] fishSpriteList;

    public Action<Vector2> ETileClicked;

    public void Init(bool isOffset) {
        Renderer.color = isOffset ? _offsetColor : _baseColor;
    }

    void OnMouseEnter() {
        _highlight.SetActive(true);
    }

    void OnMouseExit() {
        _highlight.SetActive(false);
    }
    private void OnMouseUpAsButton() {
        ETileClicked(Pos);
    }

    public  void UpdateFish(FishData.FishState? fish){
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