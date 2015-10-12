/*!
 * \file Tile.cs
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*!
 * \class Tile
 * 
 * \brief A basic Tile on the game Board.
 */
public class Tile : MonoBehaviour
{
	public int tileNumber; //!< The graphical number on the Tile.

	/*!
	 * \brief Button Event for Unity's GUI system attempts to slide the tile into the empty position.
	 * 
	 * Note: This method exists to take advantage of Unity's GUI system.
	 * 
	 * \see bool SlideTile(Tile)
	 */
	public void TrySlide()
	{
		Board.instance.SlideTile(this);
	}
}