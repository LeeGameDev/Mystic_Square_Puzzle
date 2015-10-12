using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AISolver : MonoBehaviour
{
	//private List<int> pathIndices = new List<int>();
	private Board board;
	private Tile[] tiles;

	public void SolveBoard()
	{
		board = Board.instance;
		tiles = board.tiles;

		int tileNumberToSolve = 1;

		while (!board.isSolved)
		{
			// Find the index of the tile
			Tile tileToSolve = GetTileByNumber(tileNumberToSolve);
			int tileIndex = tileToSolve.transform.GetSiblingIndex();

			// Rotate empty tile to slot adjacent to this digit and move this digit into empty slot

			// Heuristics:
			// [A] Correct Column: If tile to solve index is == desired index (col)
			// [B] Correct Row: If tile to solve index is == desired index (row)
			// 4 possibilities (A=1 & B=1, A=1 & B=0, A=0 & B=1, A=0 & B=0)

			// If A & B
			// -> Get the next tile to "solve"

			// Else
			// -> Keep sliding
			// What does that mean?

			// Solve by Row
			// If tile to solve index is > desired index (col)
			// -> Create path from (Empty tile location) to (tile to solve location - 1)
			// -> Execute Path
			// -> Complete move by sliding tile to solve
			// Check if [Solve by Column] == 1
			// Loop

			// Solve by Column
			// If tile to solve index is > desired index (col)
			int desiredIndex = 0;
			while (board.IsTileInCorrectSpot(tileToSolve))
			{
				// Create path from (Empty tile) to (tile to solve location - 1)
				List<int> pathIndices = ComputePath(board.emptyTile, tileIndex - 1);

				// Execute Path
				ExecutePath(pathIndices);

				// Complete move by sliding tile to solve
				tileToSolve.TrySlide();
				--tileIndex;
			}
		}
	}
	
	// Create path from tile to tile location
	private List<int> ComputePath(Tile t, int tileLocation)
	{
		List<int> pathIndices = new List<int>();
		return pathIndices;
	}

	// Execute path
	private void ExecutePath(List<int> pathIndices)
	{
		for (int i = 0; i < pathIndices.Count; i++)
		{
			// Get the tile adjacent to the empty tile that satisfies the path and slide it
			board.SlideTile(GetTileByNumber(pathIndices[i]));
		}
	}

	private Tile GetTileByNumber(int tileNumber)
	{
		for (int i = 0; i < tiles.Length; i++)
		{
			if (tiles[i].tileNumber == tileNumber)
			{
				return tiles[i];
			}
		}
		return null;
	}
}