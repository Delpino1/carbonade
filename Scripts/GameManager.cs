using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Count
	public int currentScore;
	public int highscore;
	public int tokenCount;
	private int totalTokenCount;
	public int currentLevel = 0;
	public int unlockedLevel;

	// Timer variables
	public Rect timerRect;
	public Color warningColorTimer;
	public Color defaultColorTimer;
	public float startTime;
	private string currentTime;

	// GUI SKI
	public GUISkin skin;

	// References
	public GameObject tokenParent;

	private bool completed = false;
	private bool showWinScreen = false;
	public int winScreenWidth, winScreenHeight;

	void Update()
	{
		if (!completed)
		{
			startTime -= Time.deltaTime;
			currentTime = string.Format("Time: {0:0}", startTime);
			if (startTime <= 0)
			{
				startTime = 0;
				Application.LoadLevel("main_menu");
			}
		}
	}

	void Start()
	{
		totalTokenCount = tokenParent.transform.childCount;

		if (PlayerPrefs.GetInt("Level Completed") > 0)
		{
			currentLevel = PlayerPrefs.GetInt("Level Completed");
		} else {
			currentLevel = 0;
		}

			
	}
	
	public void CompleteLevel()
	{
		showWinScreen = true;
		completed = true;
	}

	void LoadNextLevel()
	{
		if (currentLevel < 2)
		{
			currentLevel += 1;
			print (currentLevel);
			SaveGame();
			Application.LoadLevel(currentLevel);
		} else {
			print ("You win!");
		}
	}

	void SaveGame()
	{
		PlayerPrefs.SetInt("Level Completed", currentLevel);
		PlayerPrefs.SetInt("Level " + currentLevel.ToString() + " score", currentScore);
	}

	void OnGUI()
	{
		GUI.skin = skin;
		if (startTime < 5f)
		{
			skin.GetStyle("Timer").normal.textColor = warningColorTimer;
		} else {
			skin.GetStyle("Timer").normal.textColor = defaultColorTimer;
		}
		GUI.Label (timerRect, currentTime, skin.GetStyle ("Timer"));
		GUI.Label (new Rect(45,100,200,200), tokenCount.ToString() + "/" + totalTokenCount.ToString());

		if (showWinScreen)
		{
			Rect winScreenRect = new Rect(Screen.width/2 - (Screen.width *.5f/2), Screen.height/2 - (Screen.height *.5f/2), Screen.width *.5f, Screen.height *.5f);
			GUI.Box(winScreenRect, "Yeah");

			int gameTime = (int)startTime;
			currentScore = tokenCount * gameTime;
			if (GUI.Button(new Rect(winScreenRect.x + winScreenRect.width - 150, winScreenRect.y + winScreenRect.height - 60, 150, 40), "Continue"))
			{
				LoadNextLevel();
			}
			if (GUI.Button(new Rect(winScreenRect.x + 10, winScreenRect.y + winScreenRect.height - 60, 100, 40), "Quit"))
			{
				Application.LoadLevel("main_menu");
			}

			GUI.Label(new Rect(winScreenRect.x + 10, winScreenRect.y + 40, 300, 50), "Your score:" + currentScore.ToString());
			GUI.Label(new Rect(winScreenRect.x + 10, winScreenRect.y + 70, 300, 50), "Completed Level " + currentLevel);

		}
	}
}









