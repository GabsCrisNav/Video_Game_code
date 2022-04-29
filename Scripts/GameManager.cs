using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    menu,
    inTheGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
	public static GameManager sharedInstance;

	public Canvas menuCanvas;
	public Canvas gameCanvas;
	public Canvas gameOverCanvas;
  public bool difficulty;
	public int collectedCoins = 0;

	public GameState currentGameState = GameState.menu;

	void Awake()
	{
		sharedInstance = this;
	}

	void Start()
	{
		currentGameState = GameState.menu;
		gameCanvas.enabled = false;
		gameOverCanvas.enabled = false;
	}

	void Update()
    {
		if (Input.GetButtonDown("s") && currentGameState != GameState.gameOver) GameOver() ;
    }

	public void StartGameEasy()
	{
    difficulty = true;
		PlayerController.sharedInstance.StartGame();
    PlayerController.sharedInstance.runningSpeed = 10.0f;
    PlayerController.sharedInstance.jumpForce = 35.0f;
		LevelGenerator.sharedInstance.GenerateInitialBlocks();
		ChangeGameState(GameState.inTheGame);
	}

  public void StartGameHard()
  {
    difficulty = false;
    PlayerController.sharedInstance.StartGame();
    PlayerController.sharedInstance.runningSpeed = 15.0f;
    PlayerController.sharedInstance.jumpForce = 25.0f;
    LevelGenerator.sharedInstance.GenerateInitialBlocks();
    ChangeGameState(GameState.inTheGame);
  }

  public void StartGame(){
    if(difficulty){
      StartGameEasy();
    }else{
      StartGameHard();
    }
  }

	public void GameOver()
	{
		ChangeGameState(GameState.gameOver);
		LevelGenerator.sharedInstance.RemoveAllTheBlocks();
    ViewGameOver.sharedInstance.UpdateUI();
	}

	public void BackToMainMenu()
	{
		ChangeGameState(GameState.menu);
	}

	void ChangeGameState(GameState newGameState)
	{


		if (newGameState == GameState.menu)
		{
			menuCanvas.enabled = true;
			gameCanvas.enabled = false;
			gameOverCanvas.enabled = false;

		}
		else if (newGameState == GameState.inTheGame)
		{
			menuCanvas.enabled = false;
			gameCanvas.enabled = true;
			gameOverCanvas.enabled = false;
		}
		else if (newGameState == GameState.gameOver)
		{
			menuCanvas.enabled = false;
			gameCanvas.enabled = false;
			gameOverCanvas.enabled = true;
		}

		currentGameState = newGameState;

	}

	public void CollectCoins(){
		collectedCoins++;
	}

}
