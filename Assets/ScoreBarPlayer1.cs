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
		dangerColorFlash = new Color32 (dangerColor.r, dangerColor.g, dangerColor.b, 127);
	}
	
	// Update is called once per frame
	void Update () {
		image.fillAmount = GlobalUtil.Health1;
		if(GlobalUtil.Health1 > .4)
		{
			image.color = healthyColor;
		}
		else if(GlobalUtil.Health1 >.2)
		{
			image.color = cautionColor;
		}
		else
		{
			image.color = Color32.Lerp(dangerColor, dangerColorFlash, Mathf.Sin (Time.deltaTime));
		}

		if (GlobalUtil.Health1 <= 0f) {
			Application.LoadLevel ("player2end");
		}
	}

}
