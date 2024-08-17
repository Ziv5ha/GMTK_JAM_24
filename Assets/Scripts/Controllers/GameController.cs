using System;
using System.Collections;
using System.Collections.Generic;
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

    public void CreateBoard() {
        
        int width = UnityEngine.Random.Range(3, 10);
        int height = UnityEngine.Random.Range(3, 10);

        CurrentBoardData = new Dictionary<Vector2, TileData>();
        CurrentBoardView = new Dictionary<Vector2, TileView>();

        for (int x = 0; x < height; x++) {
            for (int y = 0; y < width; y++) {
                
                TileView spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.transform.parent = _gameView.transform;
                spawnedTile.name = $"Tile {x} {y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);

                CurrentBoardData[new Vector2(x, y)] = new TileData();
                CurrentBoardView[new Vector2(x, y)] = spawnedTile;
            }
        }
        PlaceExit(width, height);
        _cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);

        // PrintBoard(CurrentBoard);
    }

    private void PlaceExit( int width, int height) {
        int xPos = UnityEngine.Random.Range(0, 1) > 1 ? width : 0;
        int yPos = UnityEngine.Random.Range(0, 1) > 1 ? height : 0;
        Direction direction = xPos == 0 ?Direction.RIGHT:Direction.LEFT;
        Debug.Log($"Fuckiong posiution {xPos},{yPos}");
        ExitData appliance = new ExitData(direction);
        Debug.Log($"11: {appliance.Appliance}");
        PlaceAppliance(appliance, new Vector2(xPos, yPos));
    }
    private void PlaceAppliance(TileData appliance,Vector2 pos) {
        CurrentBoardData[pos] = appliance;
        TileView view = CurrentBoardView[pos];
        Debug.Log(appliance.Appliance);
        TileView.yomama relevantSprite = Array.Find(view.yomamalist, (t) =>
        {
            return t.appliance == appliance.Appliance;
        });
        if(relevantSprite!=null){
            view.ApplianceRenderer.sprite= relevantSprite.sprite;
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

    public void BuyAppliances(TileData.Appliances appliance) {

    }

public class TileMap{
    public TileData tileData;
    public TileView tileView;
}
}