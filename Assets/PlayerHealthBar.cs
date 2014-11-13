using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour {
	private Image image;

	[SerializeField]
	private string _playerHealth, _enemyPlayerScore;

	[SerializeField]
	private Color32 healthyColor, cautionColor, dangerColor;
	private Color32 dangerColorFlash;

	private float _timePassed = 0;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image>();
		dangerColorFlash = new Color (dangerColor.r, dangerColor.g, dangerColor.b, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		_timePassed += Time.deltaTime;

		image.fillAmount = (float)GlobalUtil.Get(_playerHealth);

		if(image.fillAmount > .4)
		{
			image.color = healthyColor;
		}
		else if(image.fillAmount >.2)
		{
			image.color = cautionColor;
		}
		else if(image.fillAmount >0)
		{
			image.color = Color.Lerp(dangerColor, dangerColorFlash, (Mathf.Sin (_timePassed*10) + 1f)/2);
		}
		else
		{
			Application.LoadLevel ("player2end");
		}
	}
}
