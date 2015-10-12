using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tile : MonoBehaviour
{
	public int tileNumber;

	// Tells the board to attempt to slide this tile
	// Note: This method exists to take advantage of Unity's GUI system
	public bool TrySlide()
	{
		bool result = false;
		if (tileNumber < 16)
		{
			result = Board.instance.SlideTile(this);
		}
		return result;
	}
}