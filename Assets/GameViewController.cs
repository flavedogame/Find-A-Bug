using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameViewController : MonoBehaviour {

    public TextMeshProUGUI pointsText;
    public Button pauseButton;

	// Use this for initialization
	void Start () {
        pauseButton.onClick.AddListener(OnClickPauseButton);

    }

    void OnClickPauseButton()
    {
        new PauseMenuViewController();
    }
	
	// Update is called once per frame
	void Update () {
        pointsText.text = CurrencyManager.Instance.AmountOfCurrency("points").ToString();
	}
}
