using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController: MonoBehaviour {
    public List<List<Tile1>> CurrentBoard = new List<List<Tile1>>();

    public void CreateBoard() {
        List<List<Tile1>> Board = new List<List<Tile1>>();
        int width = Random.Range(3, 10);
        int height = Random.Range(3, 10);

        for (int i = 0; i < height; i++) {
            Board.Add(new List<Tile1>());
            for (int j = 0; j < width; j++) {
                Board[i].Add(new Tile1());
            }
        }
        PlaceExit(Board, width, height);
        CurrentBoard = Board;

        PrintBoard(CurrentBoard);
    }

    private void PlaceExit(List<List<Tile1>> Board, int width, int height) {
        int xPos = Random.Range(0, 1) > 1 ? width : 0;
        int yPos = Random.Range(0, 1) > 1 ? height : 0;
        Board[yPos][xPos].Appliance = Tile1.Appliances.exit;
    }

    private void PrintBoard(List<List<Tile1>> Board) {
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

public class Tile1 {
    public enum Appliances { empty, supplies, conveyor, butcher, packer, exit };
    public Appliances Appliance = Appliances.empty;
    public bool Interacable { get { return Appliance != Appliances.empty; } }

    public string ToShortString() {
        switch (Appliance) {
            case Appliances.exit:
                return "X";
            case Appliances.supplies:
                return "s";
            case Appliances.butcher:
                return "b";
            case Appliances.packer:
                return "p";
            case Appliances.conveyor:
                return "-";
            case Appliances.empty:
            default:
                return " ";
        }
    }

    public override string ToString() {
        return Appliance.ToString();
    }
}