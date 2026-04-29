using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	public GameObject HelpPanel;
	public GameObject scoreBorad; 

	public void GameStartButtonAction()
	{
		SceneManager .LoadScene( "Level_1");
	}

	public void OpenHelpPanel()
	{
		HelpPanel.SetActive(true);
	}

	public void CloseHelpPanel()
	{
		HelpPanel.SetActive(false);
	}

	public void OpenScorePanel()
	{
		scoreBorad.SetActive(true);
	}

	public void CloseScorePanel()
	{
		scoreBorad.SetActive(false);
	}
}