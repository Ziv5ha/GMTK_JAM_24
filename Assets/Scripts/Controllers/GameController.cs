using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class GameController: MonoBehaviour {
    [SerializeField] private TileView _tilePrefab;
    [SerializeField] private Transform _cam;
    [SerializeField] private TileMap _tileMap;
    private Dictionary<Vector2, TileData> CurrentBoardData;
    private Dictionary<Vector2, TileView> CurrentBoardView;
    [SerializeField] private GameObject _gameView;

    public TileData.Appliances? ApplianceInHand;

    private bool gameStarted = false;
    private float timer = 0;
    private float round = 0;
    private int width = 0;
    private int height = 0;

    public void Init() {

    }

    public void CreateBoard() {

        width = UnityEngine.Random.Range(4, 10);
        height = UnityEngine.Random.Range(4, 10);

        CurrentBoardData = new Dictionary<Vector2, TileData>();
        CurrentBoardView = new Dictionary<Vector2, TileView>();

        for (int x = 0; x < height; x++) {
            for (int y = 0; y < width; y++) {

                TileView spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.transform.parent = _gameView.transform;
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.Pos = new Vector2(x, y);
                spawnedTile.ETileClicked += TileClicked;

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);

                Vector2 position = new Vector2(x, y);
                CurrentBoardData[position] = new TileData(TileData.Appliances.empty, position);
                CurrentBoardView[position] = spawnedTile;
            }
        }
        PlaceExit(width, height);

        PlaceAppliance(new FishBinData(new Vector2(0, 2)));
        PlaceAppliance(new ConveyorData(new Vector2(1, 2)));
        PlaceAppliance(new ScalerData(new Vector2(2, 2)));
        PlaceAppliance(new ConveyorData(new Vector2(3, 2)));

        _cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
        gameStarted = true;
    }

    private void PlaceExit(int width, int height) {
        int xPos = UnityEngine.Random.Range(0, 1) > 1 ? width : 0;
        int yPos = UnityEngine.Random.Range(0, 1) > 1 ? height : 0;
        Direction direction = xPos == 0 ? Direction.RIGHT : Direction.LEFT;
        PlaceAppliance(new ExitData(direction, new Vector2(xPos, yPos)));
    }
    private void PlaceAppliance(TileData appliance) {
        Vector2 pos = appliance._position;
        CurrentBoardData[pos] = appliance;
        UpdateApplianceViews(appliance, pos);
    }
    private void UpdateApplianceViews(TileData appliance, Vector2 pos) {
        TileView view = CurrentBoardView[pos];
        TileView.applianceSprite relevantSprite = Array.Find(view.applianceSpriteList, (t) => {
            return t.appliance == appliance.Appliance;
        });
        if (relevantSprite != null) {
            view.ApplianceRenderer.sprite = relevantSprite.sprite;
            view.ApplianceRenderer.flipX = appliance.direction == Direction.RIGHT;
        }
    }

    private void UpdateFishView(TileData app) {

        TileView view = CurrentBoardView[app._position];
        view.UpdateFish(app.fish);
    }

    private void TileClicked(Vector2 pos) {
        TileData tile = CurrentBoardData[pos];

        Debug.Log($"!@# Tile {pos} Clicked! and It's a {tile}");

        switch (tile.Appliance) {
            case TileData.Appliances.empty:
                if (ApplianceInHand != null) {
                    CurrentBoardData[pos] = TileDataFactory(ApplianceInHand.Value, pos);
                }
                break;
            case TileData.Appliances.butcher:
            case TileData.Appliances.conveyor:
            case TileData.Appliances.packer:
            case TileData.Appliances.supplies:
                ApplianceInHand = tile.Appliance;
                break;
            case TileData.Appliances.exit:
            default:
                break;
        }
    }

    public TileData TileDataFactory(TileData.Appliances appliance, Vector2 pos) {
        switch (ApplianceInHand) {
            case TileData.Appliances.supplies:
                return new FishBinData(pos);
            case TileData.Appliances.conveyor:
                return new ConveyorData(pos);
            case TileData.Appliances.butcher:
                return new ScalerData(pos);
            case TileData.Appliances.packer:
                return new PackerData(pos);
            default:
                return new TileData(appliance, pos);
        }
    }

    public void Update() {
        if (gameStarted) {
            timer += Time.deltaTime;
            if (timer >= 1.5f) {
                round += 1;
                timer = 0;
                Advance();
            }
        }
    }
    private void Advance() {
        for (int x = 0; x < height; x++) {
            for (int y = 0; y < width; y++) {
                Vector2 pos = new Vector2(x, y);
                TileData cur = CurrentBoardData[pos];

                if (!cur.Interacable) {
                    continue;
                }

                if (cur.doProcess()) {
                    continue;
                };
                UpdateFishView(cur);
                
                cur.WantToPush(out Vector2? givePos);

                if (givePos.HasValue) {
                    Vector2 giveTargetPos = givePos.Value;
                    TileData giveTarget = CurrentBoardData[giveTargetPos];
                    AttemptFishTransaction(cur, giveTarget, "push");
                    // continue;
                }
                

                cur.WantToTake(out Vector2? takePos);
                if (takePos.HasValue) {

                    Vector2 takeTargetPos = takePos.Value;
                    TileData takeTarget = CurrentBoardData[takeTargetPos];
                    AttemptFishTransaction(takeTarget, cur, "pull");
                }


            }
        }    
    }

    void test (TileData cur,string message){
        if(cur.Appliance == TileData.Appliances.conveyor){
            Debug.Log($"round {round} - "+message);
        }
    }
    private void AttemptFishTransaction( TileData giver,  TileData receiver,string action)
    {
        if (giver.CanGive && receiver.CanReceive)
        {
            receiver.ReceiveFish(giver.PushFish());
            UpdateFishView(receiver);
            UpdateFishView(giver);

            // Debug.Log($"In Round {round} {giver.Appliance} GAVE A FISH to {receiver.Appliance} ({action})");
        }
    }


    public class TileMap {
        public TileData tileData;
        public TileView tileView;
    }
}