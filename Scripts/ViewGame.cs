using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewGame : MonoBehaviour
{
	public Text coinsLabel;
 	public Text scoreLabel;
    public Text highScoreLabel;
     
     // Update is called once per frame
	void Update () {
        
    if (GameManager.sharedInstance.currentGameState == GameState.inTheGame) {
			coinsLabel.text = GameManager.sharedInstance.collectedCoins.ToString ();
            scoreLabel.text = PlayerController.sharedInstance.GetDistance().ToString("f0");
            highScoreLabel.text = PlayerPrefs.GetFloat("highscore",0).ToString("f0");
    	}
	}


}
