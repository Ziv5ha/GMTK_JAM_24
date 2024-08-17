using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController: MonoBehaviour {
    public int Coins = 0;
    public List<List<TileData>> CurrentBoard = new List<List<TileData>>();

    const int FISH_COIN_VALUE = 1;

    public void CreateBoard() {
        List<List<TileData>> Board = new List<List<TileData>>();
        int width = Random.Range(3, 10);
        int height = Random.Range(3, 10);

        for (int i = 0; i < height; i++) {
            Board.Add(new List<TileData>());
            for (int j = 0; j < width; j++) {
                Board[i].Add(new TileData());
            }
        }
        PlaceExit(Board, width, height);
        CurrentBoard = Board;

        PrintBoard(CurrentBoard);
    }

    private void PlaceExit(List<List<TileData>> Board, int width, int height) {
        int xPos = Random.Range(0, 1) > 1 ? width : 0;
        int yPos = Random.Range(0, 1) > 1 ? height : 0;
        Board[yPos][xPos].Appliance = TileData.Appliances.exit;
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

}