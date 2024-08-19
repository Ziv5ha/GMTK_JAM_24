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
    [SerializeField] private ShopController ShopControllerRef;
    [SerializeField] private StorageController StorageControllerRef;

    public TileData.Appliances? ApplianceInHand;

    private bool gameStarted = false;
    private int round = 0;
    private float timer = 0;
    private int width = 7;
    private int height = 10;

    public void Init() {

    }

    public void CreateBoard() {

        //width = UnityEngine.Random.Range(7, 10);
        //height = UnityEngine.Random.Range(7, 10);

        CurrentBoardData = new Dictionary<Vector2, TileData>();
        CurrentBoardView = new Dictionary<Vector2, TileView>();

        for (int x = 0; x < height; x++) {
            for (int y = 0; y < width; y++) {

                TileView spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.transform.parent = _gameView.transform;
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.Pos = new Vector2(x, y);
                spawnedTile.ETileClicked += TileClicked;
                spawnedTile.ETileRightClicked += TileRightClicked;

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);

                Vector2 position = new Vector2(x, y);
                CurrentBoardData[position] = new TileData(TileData.Appliances.Empty, position);
                CurrentBoardView[position] = spawnedTile;
            }
        }
        // PlaceExit(width, height);

        PlaceAppliance(new FishBinData(new Vector2(0, 2)));
        PlaceAppliance(new ConveyorData(new Vector2(1, 2)));
        PlaceAppliance(new ScalerData(new Vector2(2, 2)));
        PlaceAppliance(new ConveyorData(new Vector2(3, 2)));
        PlaceAppliance(new PackerData(new Vector2(4, 2)));
        PlaceAppliance(new ConveyorData(new Vector2(5, 2)));

        ExitData DebugExitAppliance = new ExitData(Direction.LEFT, new Vector2(6, 2));
        DebugExitAppliance.ESellFish += SellFish;
        PlaceAppliance(DebugExitAppliance);
        //PlaceExit(width, height);

        _cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
        gameStarted = true;
    }

    private void PlaceExit(int width, int height) {
        int xPos = UnityEngine.Random.Range(0, 1) > 1 ? width : 0;
        int yPos = UnityEngine.Random.Range(0, 1) > 1 ? height : 0;
        Direction direction = xPos == 0 ? Direction.RIGHT : Direction.LEFT;
        ExitData exitAppliance = new ExitData(direction, new Vector2(xPos, yPos));
        exitAppliance.ESellFish += SellFish;
        PlaceAppliance(exitAppliance);
    }
    private void PlaceAppliance(TileData appliance) {
        Debug.Log($"!@# PlaceAppliance: {appliance}");
        Vector2 pos = appliance._position;
        CurrentBoardData[pos] = appliance;
        CurrentBoardData[pos].fish = null;
        UpdateFishView(appliance);
        Debug.Log($"!@# CurrentBoardData[pos]: {CurrentBoardData[pos]}");
        UpdateApplianceViews(appliance, pos);
    }
    private void UpdateApplianceViews(TileData appliance, Vector2 pos) {
        TileView view = CurrentBoardView[pos];

        TileView.applianceSprite relevantSprite = Array.Find(view.applianceSpriteList, (t) => {
            return t.appliance == appliance.Appliance;
        });
        // Debug.Log($"!@# found relevantSprite for {appliance.Appliance}: {relevantSprite != null}");
        if (relevantSprite != null) {
            view.ApplianceRenderer.sprite = relevantSprite.sprite;
            view.ApplianceRenderer.flipX = appliance.direction == Direction.RIGHT;
        }
    }

    private void UpdateFishView(TileData app) {

        TileView view = CurrentBoardView[app._position];
        view.UpdateFish(app.fish);
        if (
            app.Appliance == TileData.Appliances.Scaler || 
            app.Appliance == TileData.Appliances.Exit ||
            app.Appliance == TileData.Appliances.Packer || 
            app.Appliance == TileData.Appliances.Conveyor
        ) {
            view.UpdateAnim(app, app.isBusy);
        } else {
            view.appAnimator.enabled = false;
        }
    }

    private void TileClicked(Vector2 pos) {
        TileData tile = CurrentBoardData[pos];

        // Debug.Log($"!@# Tile {pos} Clicked! and It's a {tile}");

        switch (tile.Appliance) {
            case TileData.Appliances.Empty:
                if (ApplianceInHand != null) {
                    PlaceAppliance(TileDataFactory(ApplianceInHand.Value, pos));
                    ApplianceInHand = null;
                }
                break;
            case TileData.Appliances.Scaler:
            case TileData.Appliances.Conveyor:
            case TileData.Appliances.Packer:
            case TileData.Appliances.FishBin:

                if (ApplianceInHand != null) {
                    return;
                }
                ApplianceInHand = tile.Appliance;
                PlaceAppliance(TileDataFactory(TileData.Appliances.Empty, pos));
                break;
            case TileData.Appliances.Exit:
            default:
                break;
        }
    }

    public TileData TileDataFactory(TileData.Appliances appliance, Vector2 pos) {
        switch (appliance) {
            case TileData.Appliances.FishBin:
                return new FishBinData(pos);
            case TileData.Appliances.Conveyor:
                return new ConveyorData(pos);
            case TileData.Appliances.Scaler:
                return new ScalerData(pos);
            case TileData.Appliances.Packer:
                return new PackerData(pos);
            default:
                return new TileData(appliance, pos);
        }
    }

    public void Update() {
        if (gameStarted) {
            timer += Time.deltaTime;
            if (timer >= GameConstants.RoundDuration) {
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

                bool busy = cur.doProcess();
                UpdateFishView(cur);
                if (busy) {
                    continue;
                };
                TryPush(cur);

                TryPull(cur);

            }
        }
        StorageControllerRef.UpdateStorage(round);

        void TryPush(TileData cur) {


            if (cur.WantToPush(out Vector2? givePos)) {
                Vector2 giveTargetPos = givePos.Value;

                if (CurrentBoardData.TryGetValue(giveTargetPos, out TileData giveTarget)) {
                    AttemptFishTransaction(cur, giveTarget, "push");
                }
            }
        }
    }

    private void TryPull(TileData cur) {

        if (cur.WantToTake(out Vector2? takePos)) {
            Vector2 takeTargetPos = takePos.Value;
            if (CurrentBoardData.TryGetValue(takeTargetPos, out TileData takeTarget)) {
                AttemptFishTransaction(takeTarget, cur, "pull");
            }

        }
    }

    void test(TileData cur, string message) {
        if (cur.Appliance == TileData.Appliances.Conveyor) {
            Debug.Log($"round {round} - " + message);
        }
    }
    private void AttemptFishTransaction(TileData giver, TileData receiver, string action) {

        if (giver.CanGive && receiver.CanReceive(giver.fish)) {
            receiver.ReceiveFish(giver.PushFish());
            UpdateFishView(receiver);
            UpdateFishView(giver);

            // Debug.Log($"In Round {round} {giver.Appliance} GAVE A FISH to {receiver.Appliance} ({action})");
        }
    }

    private void SellFish() {
        GameConstants.TotalFishSold++;
        GameConstants.CurrentFishInStorage++;
        ShopControllerRef.SellFish();
        Debug.Log($"!@# GameConstants.TotalFishSold: {GameConstants.TotalFishSold}");
    }

    private void TileRightClicked(Vector2 pos) {
        TileData tile = CurrentBoardData[pos];

        switch (tile.Appliance) {
            case TileData.Appliances.Conveyor:
                RotateApplience(tile);
                break;
            case TileData.Appliances.Empty:
            case TileData.Appliances.Scaler:
            case TileData.Appliances.Packer:
            case TileData.Appliances.FishBin:
            case TileData.Appliances.Exit:
            default:
                break;
        }
    }

    private void RotateApplience(TileData td) {
        // Debug.Log($"!@# Rotating {td}");
        td.direction = GetNextDirection(td.direction);
        CurrentBoardView[td._position].RotateAppliance(td);
    }

    private Direction GetNextDirection(Direction d) {
        switch (d) {
            case Direction.RIGHT:
                return Direction.DOWN;
            case Direction.DOWN:
                return Direction.LEFT;
            case Direction.LEFT:
                return Direction.UP;
            case Direction.UP:
                return Direction.RIGHT;
        }
        return Direction.LEFT;
    }

    public void EndGame() {
        gameStarted = false;
    }

    public class TileMap {
        public TileData tileData;
        public TileView tileView;
    }
}