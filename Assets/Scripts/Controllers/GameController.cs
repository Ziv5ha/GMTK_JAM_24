using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController: MonoBehaviour {
    public List<List<Tile>> CurrentBoard = new List<List<Tile>>();

    public void CreateBoard() {
        List<List<Tile>> Board = new List<List<Tile>>();
        int width = Random.Range(3, 10);
        int height = Random.Range(3, 10);

        for (int i = 0; i < height; i++) {
            Board.Add(new List<Tile>());
            for (int j = 0; j < width; j++) {
                Board[i].Add(new Tile());
            }
        }
        PlaceExit(Board, width, height);
        CurrentBoard = Board;

        PrintBoard(CurrentBoard);
    }

    private void PlaceExit(List<List<Tile>> Board, int width, int height) {
        int xPos = Random.Range(0, 1) > 1 ? width : 0;
        int yPos = Random.Range(0, 1) > 1 ? height : 0;
        Board[yPos][xPos].Appliance = Tile.Appliances.exit;
    }

    private void PrintBoard(List<List<Tile>> Board) {
        string boardString = "";
        for (int i = 0; i < Board.Count; i++) {
            string layerString = "|";
            for (int j = 0; j < Board[i].Count; j++) {
                layerString += $"{Board[i][j].ToShortString()}|";
            }
            layerString += "\n";
            boardString += layerString;
        }
        Debug.Log($"{boardString}");
    }
}
