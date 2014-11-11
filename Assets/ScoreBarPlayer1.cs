using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreBarPlayer1 : MonoBehaviour {
	private Image image;

	[SerializeField]
	private Color32 healthyColor, cautionColor, dangerColor;
	private Color32 dangerColorFlash;

	bool colorFlash = false;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image>();
		dangerColorFlash = new Color32 (dangerColor.r, dangerColor.g, dangerColor.b, .5f);
	}
	
	// Update is called once per frame
	void Update () {
		image.fillAmount = PlayerScoreKeeper.Health1;
		if(PlayerScoreKeeper.Health1 > .4)
		{
			image.color = healthyColor;
		}
		else if(PlayerScoreKeeper.Health1 >.2)
		{
			image.color = cautionColor;
		}
		else
		{
			image.color = Color32.Lerp(dangerColor, dangerColorFlash, Mathf.Sin (Time.deltaTime));
		}
	}
}
