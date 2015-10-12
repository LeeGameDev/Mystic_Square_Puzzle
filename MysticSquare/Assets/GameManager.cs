/*!
 *\file GameManager.cs
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*!
 * \class GameManager
 * 
 * \brief Manages the win condition for the Puzzle an the relevant Graphical Interface.
 */
public class GameManager : MonoBehaviour
{
	public static GameManager instance;	//!< Static reference to this. Only one GameManager instance should exist.
	public Text solvedText;				//!< Displays the victory text.

	// Use this for initialization.
	private void Start()
	{
		instance = this;
	}

	/*!
	 * \brief Updates the uGUI to reflect the victory state.
	 */
	public void PlayerWon()
	{
		solvedText.enabled = true;
	}
}