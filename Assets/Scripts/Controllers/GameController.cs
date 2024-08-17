using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController: MonoBehaviour {
    public int Coins = 0;
    [SerializeField] private TileView _tilePrefab;
    [SerializeField] private Transform _cam;
    [SerializeField] private TileMap _tileMap;
     private Dictionary<Vector2, TileData> CurrentBoardData;
     private Dictionary<Vector2, TileView> CurrentBoardView;
     [SerializeField] private GameObject _gameView;
    const int FISH_COIN_VALUE = 1;

    private bool gameStarted = false; 
    private float timer  =0;
    private float round  =0;
    private int width  =0;
    private int height  =0;

    public void CreateBoard() {
        
        width = UnityEngine.Random.Range(3, 10);
        height = UnityEngine.Random.Range(3, 10);

        CurrentBoardData = new Dictionary<Vector2, TileData>();
        CurrentBoardView = new Dictionary<Vector2, TileView>();

        for (int x = 0; x < height; x++) {
            for (int y = 0; y < width; y++) {
                
                TileView spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.transform.parent = _gameView.transform;
                spawnedTile.name = $"Tile {x} {y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);

                CurrentBoardData[new Vector2(x, y)] = new TileData(TileData.Appliances.empty);
                CurrentBoardView[new Vector2(x, y)] = spawnedTile;
            }
        }
        PlaceExit(width, height);
        PlaceAppliance(new FishBinData(),new Vector2(0,2));
        PlaceAppliance(new ConveyorData(),new Vector2(1,2));
        PlaceAppliance(new ScalerData(),new Vector2(2,2));

        _cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
        gameStarted = true;
        
        // PrintBoard(CurrentBoard);
    }

    private void PlaceExit( int width, int height) {
        int xPos = UnityEngine.Random.Range(0, 1) > 1 ? width : 0;
        int yPos = UnityEngine.Random.Range(0, 1) > 1 ? height : 0;
        Direction direction = xPos == 0 ?Direction.RIGHT:Direction.LEFT;
        PlaceAppliance(new ExitData(direction), new Vector2(xPos, yPos));
    }
    private void PlaceAppliance(TileData appliance,Vector2 pos) {
        CurrentBoardData[pos] = appliance;
        TileView view = CurrentBoardView[pos];
        TileView.applianceSprite relevantSprite = Array.Find(view.applianceSpriteList, (t) =>
        {
            return t.appliance == appliance.Appliance;
        });
        if(relevantSprite!=null){
            view.ApplianceRenderer.sprite= relevantSprite.sprite;
            view.ApplianceRenderer.flipX = appliance.direction ==Direction.RIGHT;
        }
    }
    
    private void updateFishPosition(bool hasFish,Vector2 pos) {
        TileView view = CurrentBoardView[pos];
        if(!hasFish){
            view.FishRenderer.sprite= null;
            return;
        }
        TileView.fishSprite relevantSprite = Array.Find(view.fishSpriteList, (t) =>
        {
            return t.state == FishData.FishState.none;
        });
        if(relevantSprite!=null){
            view.FishRenderer.sprite= relevantSprite.sprite;
        }
    }

    private void PrintBoard(List<List<TileData>> Board) {
        string boardString = "\n";
        for (int i = 0; i < Board.Count; i++) {
            string layerString = "|";
            for (int j = 0; j < Board[i].Count; j++) {
                layerString += $"{Board[i][j].ToShortString()}|";
            }
            layerString += "\n";
            boardString += layerString;
        }
        Debug.Log($"Current Board! Width:{Board[0].Count}, height:{Board.Count}");
        Debug.Log($"{boardString}");
    }

    public void SellFish() {
        Coins += FISH_COIN_VALUE;
    }

    public void BuyAppliances(TileData.Appliances appliance) {}

    public void Update(){
        if(gameStarted){
            timer+= Time.deltaTime;
            if(timer >= 1.5f){
                round+=1;
                timer = 0;
                advance();
            }
        }
    }
    private void advance(){
        for (int x = 0; x < height; x++) {
            for (int y = 0; y < width; y++) {
                Vector2 pos = new Vector2(x, y);
                TileData cur = CurrentBoardData[pos];
                cur.clearProcess();
                if(!cur.Interacable){
                    continue;
                }

                cur.wantToPush(out Vector2? givePos);
                
                if(givePos.HasValue){

                    Vector2 giveTargetPos = pos + givePos.Value;
                    TileData giveTarget = CurrentBoardData[giveTargetPos];
                    if(giveTarget.canReceive()){

                        giveTarget.receiveFish();
                        updateFishPosition(true,giveTargetPos);
                        cur.pushFish();
                        updateFishPosition(false,pos);

                        Debug.Log($"In Round {round} {cur.Appliance} GAVE A FISH to {giveTarget.Appliance} ");
                    }
                }
                cur.wantToTake(out Vector2? takePos);
                if(takePos.HasValue){

                    Vector2 takeTargetPos = pos + takePos.Value;
                    TileData takeTarget = CurrentBoardData[takeTargetPos];
                    if(takeTarget.canGive()){
                        takeTarget.pushFish();
                        updateFishPosition(false,takeTargetPos);
                        cur.receiveFish();
                        updateFishPosition(true,pos);
                        Debug.Log($"In Round {round} {cur.Appliance} GOT A FISH from {takeTarget.Appliance} ");
                    }
                }
               
                
            }
        }    
    }
        

public class TileMap{
    public TileData tileData;
    public TileView tileView;
}
}