using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GlobalUtil {

	public static float Health1 = 1f;
	public static float Health2 = 1f;

	public static int Score1 = 0;
	public static int Score2 = 0;

	public const string PLAYER1HEALTH = "Player1Health";
	public const string PLAYER2HEALTH = "Player2Health";

	public const string PLAYER1SCORE = "Player1Score";
	public const string PLAYER2SCORE = "Player2Score";

	private static Dictionary<string, string> StaticMap = new Dictionary<string, string>();

	public static void Put(string key, string value)
	{
		if(StaticMap.ContainsKey(key))
	    {
			StaticMap[key] = value;
		}
		else
		{
			StaticMap.Add (key, value);
		}
	}

	public static string Get(string key)
	{
		return StaticMap[key];
	}
}
