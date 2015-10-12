using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[ExecuteInEditMode]
public class ColourManager : MonoBehaviour
{
	public bool placeTiles = false;
	public bool updateColours = false;
	public Image boardImage;
	public Image backgroundImage;

	public Color tileColour;
	public Color tilePressedColour;
	public Color tileTextColour;
	public Color boardColour;
	public Color backgroundColour;

	void Update ()
	{
		if (updateColours) {
			updateColours = false;
			UpdateAllColours ();
		}
	}

	public void UpdateAllColours ()
	{
		Tile[] tiles = GameObject.FindObjectsOfType<Tile> ();
		foreach (Tile t in tiles) {
			Button tButton = t.GetComponent<Button> ();
			Text tText = t.GetComponentInChildren<Text> ();

			if (tButton != null) {
				ColorBlock colourBlock = tButton.colors;
				colourBlock.normalColor = tileColour;
				colourBlock.pressedColor = tilePressedColour;

				tButton.colors = colourBlock;
			}
			if (tText != null) {
				tText.color = tileTextColour;
			}
		}

		if (boardImage != null) {
			boardImage.color = boardColour;
		}
		if (backgroundImage != null) {
			backgroundImage.color = backgroundColour;
		}
	}
}
