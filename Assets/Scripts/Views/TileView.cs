using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class TileView: MonoBehaviour {
    [SerializeField] private Color _baseColor, _offsetColor;
    public SpriteRenderer Renderer;
    public SpriteRenderer ApplianceRenderer;
    public SpriteRenderer FishRenderer;
    [SerializeField] private GameObject _highlight;
    
    public applianceSprite[] applianceSpriteList;
    public fishSprite[] fishSpriteList;

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
        // send click event to game controller
    }
        
    [Serializable]
     public class applianceSprite {

       public Sprite sprite;
       public  TileData.Appliances appliance;
    }
    [Serializable]
     public class fishSprite {

       public Sprite sprite;
       public  FishData.FishState state;
    }
}