/*! 
 * \file Board.cs
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*!
 * \class Board
 * 
 * \brief The game Board holds the Tiles that the player slides.
 * 
 * The game Board holds the Tiles that the player slides. When the Tiles are arranged from 1..15
 * with the last space being the empty tile, the player wins.
 */
public class Board : MonoBehaviour
{
	public static Board instance;	//!< Static reference to this. Only one Board instance should exist.

	public Tile[] tiles;			//!< All of the board's tiles.
	public Tile emptyTile;			//!< The empty board tile.
	public Text movesText;			//!< uGUI The total number of moves.
	public bool isSolved = true;	//!< Flag for the board being solved.

	private int moves = 0;			// Sentinel counts the total number of moves

	// Use this for initialization
	private void Start()
	{
		instance = this;
	}

	/*!
	 * \brief Scrambles the board and updates the uGUI Tiles & Board accordingly.
	 */
	public void ScrambleBoard()
	{
		// Scramble all board tiles
		Scramble();

		// If the board is not solvable
		while (!IsSolvable())
		{
			// Scramble again
			Scramble();
		}

		// Update UI for gameplay
		moves = 0;
		movesText.text = "Moves: " + moves;
		isSolved = false;
	}

	// Scramble tile locations
	private void Scramble()
	{
		// 0 out bits in char
		int tileBitLocator = 0;

		// Traverse all tiles
		foreach (Tile t in tiles)
		{
			int bitLocation;
			int temp;
			bool isAvailable = false;

			// Search for an available bit location
			while (!isAvailable)
			{
				// Randomize bit location
				bitLocation = Random.Range(0, 16);
				
				// Shift the bit
				temp = 1 << bitLocation;
				
				// If bit is OFF
				if ((tileBitLocator & (1 << bitLocation)) == 0)
				{
					// Flip bit
					tileBitLocator |= temp;

					// Set tile location
					t.transform.SetSiblingIndex(bitLocation);

					// Move on
					isAvailable = true;
				}
			}
		}
	}

	// Check if board is solvable
	private bool IsSolvable()
	{
		// Check that the randomization of the tiles on the board is solvable
		int[] tileNumbers = new int[16];
		for (int i = 0; i < tileNumbers.Length; i++)
		{
			// Cache all tileNumbers based on their index as a child
			tileNumbers[i] = transform.GetChild(i).GetComponent<Tile>().tileNumber;
		}

		// Cache the index of the empty tile
		int emptyIndex = emptyTile.transform.GetSiblingIndex();

		// Compute the number of inversions for the board
		int numInversions = ComputeInversions(tileNumbers);

		bool numInversionsEven = (numInversions % 2) == 0;
		bool blankOnOddRowFromBottom = ((int)(emptyIndex / 4) % 2) != 0;

		/*
		 * If the number of inversions is even and the empty tile is on an odd row from the bottom of the board,
		 * or the number of inversions is odd and the empty tile is on an even row from the bottom of the board,
		 * then the board is solvable
		 */
		return (blankOnOddRowFromBottom == numInversionsEven);
	}

	// Computes the number of inversions for a board
	private int ComputeInversions(int[] tileNumbers)
	{
		// Compute number of inversions
		int numInversions = 0;
		for (int i = 0; i < tileNumbers.Length; i++)
		{
			if (tileNumbers[i] == 16)
			{
				continue;
			}

			int currentTileNumber = tileNumbers[i];
			for (int j = i + 1; j < tileNumbers.Length; j++)
			{
				if (currentTileNumber > tileNumbers[j])
				{
					numInversions++;
				}
			}
		}

		return numInversions;
	}

	/*!
	 * \brief Slides the selected tile to the empty tile if it is valid.
	 * 
	 * \see TrySlide()
	 * 
	 * \param t The tile to slide.
	 * 
	 * \return true if the Tile slid successfully, false otherwise.
	 */
	public bool SlideTile(Tile t)
	{
		bool result = false;

		if (!isSolved && IsValidSlide(t))
		{
			// Cache the tile indices
			int selectedIndex = t.transform.GetSiblingIndex();
			int emptyIndex = emptyTile.transform.GetSiblingIndex();

			// Swap tile locations
			t.transform.SetSiblingIndex(emptyIndex);
			emptyTile.transform.SetSiblingIndex(selectedIndex);

			// Increment the user's moves
			moves++;

			// Update UI
			movesText.text = "Moves: " + moves;

			// Check win condition
			if (IsWin())
			{
				// End game
				isSolved = true;
				GameManager.instance.PlayerWon();
			}

			result = true;
		}

		return result;
	}
	
	/*!
	 * \brief Checks if the selected tile can slide to the empty tile.
	 * 
	 * \see bool SlideTile(Tile)
	 * 
	 * \param t The Tile to check.
	 * 
	 * \return true if the Tile can be slid, false otherwise.
	 */
	public bool IsValidSlide(Tile t)
	{
		float indexDifference = Mathf.Abs(t.transform.GetSiblingIndex() - emptyTile.transform.GetSiblingIndex());
		return (indexDifference == 1) || (indexDifference == 4);
	}

	/*!
	 * \brief Checks if all tiles are in the correct positions, false otherwise.
	 * 
	 * \see bool IsTileInCorrectSpot(Tile)
	 * 
	 * \return true if the tiles are arranged from 1..15 with the last space being the empty tile, false otherwise.
	 */
	public bool IsWin()
	{
		foreach (Tile t in tiles)
		{
			if (IsTileInCorrectSpot(t))
			{
				return false;
			}
		}
		return true;
	}

	/*!
	 * \brief Checks if the tile number is the same as its relative sibling index.
	 * 
	 * \see bool IsWin()
	 * 
	 * \param t The Tile to check.
	 * 
	 * \return true if the Tile is in the correct position, false otherwise.
	 */
	public bool IsTileInCorrectSpot(Tile t)
	{
		return (t.tileNumber - 1) != t.transform.GetSiblingIndex();
	}
}