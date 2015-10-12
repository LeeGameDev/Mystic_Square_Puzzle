using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public Text solvedText;

	// Use this for initialization
	void Start()
	{
		instance = this;
	}

	public void PlayerWon()
	{
		solvedText.enabled = true;
	}
}