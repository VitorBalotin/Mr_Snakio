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
    LEFT = 0,
    UP = 1,
    RIGHT = 2,
    DOWN = 3,
    COUNT = 4
}