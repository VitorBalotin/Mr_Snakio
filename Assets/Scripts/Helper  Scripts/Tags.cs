using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tags used to verify itens or things around the map
public class Tags{
    public static string WALL = "Wall";
    public static string FRUIT = "Fruit";
    public static string BOMB = "Bomb";
    public static string TAIL = "Tail";
}

// Metrics used to position/move the nodes of the snake
public class Metrics{
    public static float NODE = 0.2f;
}

// possible player directions
public enum PlayerDirection{
    WEST = 0,
    NORTH = 1,
    EAST = 2,
    SOUTH = 3
}