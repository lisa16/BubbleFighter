using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerEnergyBar : MonoBehaviour {

	public static float Player1Energy = 0;

	private float _maxEnergy = 10;

	private Image energyBarImage;

	// Use this for initialization
	void Start () {
		energyBarImage = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		PlayerEnergyBar.Player1Energy += Time.deltaTime;

		energyBarImage.fillAmount = PlayerEnergyBar.Player1Energy / _maxEnergy;
	}
}
