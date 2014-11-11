using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class PlayerScoreKeeper {

	public static float Health1 = 1f;
	public static float Health2 = 1f;

	public static int Score1 = 0;
	public static int Score2 = 0;

	public const string PLAYER1HEALTH = "Player1Health";
	public const string PLAYER2HEALTH = "Player2Health";

	public const string PLAYER1SCORE = "Player1Score";
	public const string PLAYER2SCORE = "Player2Score";

	public static Dictionary<string, int> StaticIntMap = new Dictionary<string, int>();
	public static Dictionary<string, float> StaticFloatMap = new Dictionary<string, int>();

	public static void Put(string key, int value)
	{
		if(StaticIntMap.ContainsKey(key))
	    {
			StaticIntMap[key] = value;
		}
		else
		{
			StaticIntMap.Add (key, value);
		}
	}
	public static int Get(string key, int value)
	{
		return StaticIntMap[key];
	}
	public static void Put(string key, float value)
	{
		if(StaticFloatMap.ContainsKey(key))
		{
			StaticFloatMap[key] = value;
		}
		else
		{
			StaticFloatMap.Add (key, value);
		}
	}
	public static float Get(string key, float value)
	{
		return StaticFloatMap [key];
	}
}
