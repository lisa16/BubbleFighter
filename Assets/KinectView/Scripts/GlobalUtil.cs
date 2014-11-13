using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GlobalUtil
{

    public const string PLAYER1HEALTH = "Player1Health";
    public const string PLAYER2HEALTH = "Player2Health";

    public const string PLAYER1SCORE = "Player1Score";
    public const string PLAYER2SCORE = "Player2Score";

    private static Dictionary<string, double> StaticMap = new Dictionary<string, double>();

    static GlobalUtil()
    {
        GlobalUtil.Put(PLAYER1HEALTH, 1);
        GlobalUtil.Put(PLAYER1SCORE, 0);
        GlobalUtil.Put(PLAYER2HEALTH, 1);
        GlobalUtil.Put(PLAYER2SCORE, 0);
    }

    public static void Put(string key, double value)
    {
        if (StaticMap.ContainsKey(key))
        {
            StaticMap[key] = value;
        }
        else
        {
            StaticMap.Add(key, value);
        }
    }

    public static void AddAmount(string key, double amount)
    {
        Put(key, Get(key) + amount);
    }

    public static double Get(string key)
    {
        if (StaticMap.ContainsKey(key))
        {
            return StaticMap[key];
        }
        else
        {
            return 0;
        }
    }
}
