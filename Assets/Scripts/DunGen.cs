using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DunGen : MonoBehaviour {

	
	public int mapRows = 5;
	public int mapColumns = 10;
	public int minCells = 15;
	public int numRooms = 1;
	
	public char[,] map;
	public bool[,] traversedMap;

	private string boxCharacters;
	private string[] boxCharacterUpNeighbors;
	private string[] boxCharacterDownNeighbors;
	private string[] boxCharacterLeftNeighbors;
	private string[] boxCharacterRightNeighbors;

	private int cellsTraversed = 0;
	
	// Use this for initialization
	void Awake () {
		InitializeBoxCharacters ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GenerateMap(int startX = 1, int startY = 1)
	{
		InitializeMap ();
		traversedMap = DFSTraverse(startX, startY);
		while (cellsTraversed < minCells)
		{
			InitializeMap ();
			traversedMap = DFSTraverse(startX, startY);
		}
		DisplayMap ();
	}
	
	
	public void DisplayMap() {
		string output = "";
		for (int r = 0; r < mapRows; r++) {
			for (int c = 0; c < mapColumns; c++) {
				output += map [r, c];
			}
			output += "\n";
		}
		Debug.Log (output);
		//DFSTraverse(1, 1);
		Debug.Log(cellsTraversed);
	}

	private void InitializeMap() {
		map = new char[mapRows, mapColumns];

		// Put 'X's in top and bottom rows.
		for (int c = 0; c < mapColumns; c++) {
			map [0, c] = 'X';
			map [mapRows - 1, c] = 'X';
		}

		// Put 'X's in the left and right columns.
		for (int r = 0; r < mapRows; r++) {
			map [r, 0] = 'X';
			map [r, mapColumns - 1] = 'X';
		}

		// Set 'O' for the other map spaces (which means 'free').
		for (int r = 1; r < mapRows - 1; r++) {
			for (int c = 1; c < mapColumns - 1; c++) {
				map [r, c] = 'O';
			}
		}

		Random.InitState(System.DateTime.Now.Millisecond); 
		
		//generate random rooms

		for (int x = 0; x < numRooms; x++)
		{
			int width = Random.Range(3, mapColumns / 3);
			int length = Random.Range(3, mapRows / 3);
			int initialX = Random.Range(2, mapColumns - width - 1);
			int initialY = Random.Range(2, mapRows - length - 1);
			Debug.Log("width, length:"+width+", "+length + " x, y:"+initialX+", "+initialY);
			for (int col = initialX; col < initialX + width; col++)
			{
				for (int row = initialY; row < initialY + length; row++)
				{
					map[row, col] = '#';
				}
			}

			map[initialY, (initialX + width/2)] = '∧';
			map[initialY + length-1, (initialX + width/2)] = '∨';
			map[(initialY + length/2), initialX] = '<';
			map[(initialY + length/2), initialX + width-1] = '>';
		}

		for (int r = 1; r < mapRows - 1; r++) {
			for (int c = 1; c < mapColumns - 1; c++) {
				if (map[r, c] == 'O')
				{
					string validCharacters = GetValidBoxCharacters (r, c);
					map [r, c] = validCharacters [Random.Range (0, validCharacters.Length)];
				}
				
			}
		}
	}


	private string GetValidBoxCharacters(int row, int column) {
		string validCharacters = "";

		for (int i = 0; i < boxCharacters.Length; i++) {
			char ch = boxCharacters [i];

			if (
				boxCharacterLeftNeighbors [i].Contains (map [row, column - 1].ToString ()) &&
				boxCharacterRightNeighbors [i].Contains (map [row, column + 1].ToString ()) &&
				boxCharacterUpNeighbors [i].Contains (map [row - 1, column].ToString ()) &&
				boxCharacterDownNeighbors [i].Contains (map [row + 1, column].ToString ()))
			{
				validCharacters += ch.ToString ();
			}
		}

		if (validCharacters.Length == 0) {
			validCharacters = "O";
		}

		return validCharacters;
	}

	public bool[,] DFSTraverse(int initialX, int initialY)
	{
		bool[,] visitedCells = new bool[mapRows,mapColumns];
		DFSTraverseHelper(visitedCells, initialX, initialY);
		return visitedCells;
	}

	private void DFSTraverseHelper(bool[,] visitedCells, int row, int col)
	{
		if (visitedCells[row, col])
		{
			return;
		}

		//if (map[row, col] != 'O')
		//{
			visitedCells[row, col] = true;
		//}
		
		cellsTraversed++;

		switch (map [row, col])
		{
			case '─':
				DFSTraverseHelper(visitedCells, row, col-1);
				DFSTraverseHelper(visitedCells, row, col+1);
				break;
			case '│':
				DFSTraverseHelper(visitedCells, row-1, col);
				DFSTraverseHelper(visitedCells, row+1, col);
				break;
			case '┌':
				DFSTraverseHelper(visitedCells, row, col+1);
				DFSTraverseHelper(visitedCells, row+1, col);
				break;
			case '┐':
				DFSTraverseHelper(visitedCells, row, col-1);
				DFSTraverseHelper(visitedCells, row+1, col);
				break;
			case '└':
				DFSTraverseHelper(visitedCells, row, col+1);
				DFSTraverseHelper(visitedCells, row-1, col);
				break;
			case '┘':
				DFSTraverseHelper(visitedCells, row, col-1);
				DFSTraverseHelper(visitedCells, row-1, col);
				break;
			case '├':
				DFSTraverseHelper(visitedCells, row, col+1);
				DFSTraverseHelper(visitedCells, row-1, col);
				DFSTraverseHelper(visitedCells, row+1, col);
				break;
			case '┤':
				DFSTraverseHelper(visitedCells, row, col-1);
				DFSTraverseHelper(visitedCells, row-1, col);
				DFSTraverseHelper(visitedCells, row+1, col);
				break;
			case '┬':
				DFSTraverseHelper(visitedCells, row, col+1);
				DFSTraverseHelper(visitedCells, row, col-1);
				DFSTraverseHelper(visitedCells, row+1, col);
				break;
			case '┴':
				DFSTraverseHelper(visitedCells, row, col+1);
				DFSTraverseHelper(visitedCells, row, col-1);
				DFSTraverseHelper(visitedCells, row-1, col);
				break;
			case '┼':
			case '<':
			case '>':
			case '∨':
			case '∧':
			case '#':
				DFSTraverseHelper(visitedCells, row, col+1);
				DFSTraverseHelper(visitedCells, row, col-1);
				DFSTraverseHelper(visitedCells, row-1, col);
				DFSTraverseHelper(visitedCells, row+1, col);
				break;
			
			
			
		}
	}


	private void InitializeBoxCharacters() {
		boxCharacters = "─│┌┐└┘├┤┬┴┼";
		boxCharacterUpNeighbors = new string[boxCharacters.Length];
		boxCharacterDownNeighbors = new string[boxCharacters.Length];
		boxCharacterLeftNeighbors = new string[boxCharacters.Length];
		boxCharacterRightNeighbors = new string[boxCharacters.Length];

		boxCharacterLeftNeighbors [0] = "O─┌└├┬┴┼";// ─
		boxCharacterLeftNeighbors [1] = "O│┐┘┤X#";// │
		boxCharacterLeftNeighbors [2] = "O│┐┘┤X#";// ┌
		boxCharacterLeftNeighbors [3] = "O─┌└├┬┴┼";//┐
		boxCharacterLeftNeighbors [4] = "O│┐┘┤X#";//└
		boxCharacterLeftNeighbors [5] = "O─┌└├┬┴┼";//┘
		boxCharacterLeftNeighbors [6] = "O│┐┘┤X#";//├
		boxCharacterLeftNeighbors [7] = "O─┌└├┬┴┼>";//┤
		boxCharacterLeftNeighbors [8] = "O─┌└├┬┴┼>";//┬
		boxCharacterLeftNeighbors [9] = "O─┌└├┬┴┼>";//┴
		boxCharacterLeftNeighbors [10] = "O─┌└├┬┴┼>";//┼

		boxCharacterRightNeighbors [0] = "O─┐┘┤┬┴┼";
		boxCharacterRightNeighbors [1] = "O│┌└├X#";
		boxCharacterRightNeighbors [2] = "O─┐┘┤┬┴┼";
		boxCharacterRightNeighbors [3] = "O│┌└├X#";
		boxCharacterRightNeighbors [4] = "O─┐┘┤┬┴┼";
		boxCharacterRightNeighbors [5] = "O│┌└├X#";
		boxCharacterRightNeighbors [6] = "O─┐┘┤┬┴┼<";
		boxCharacterRightNeighbors [7] = "O│┌└├X#";
		boxCharacterRightNeighbors [8] = "O─┐┘┤┬┴┼<";
		boxCharacterRightNeighbors [9] = "O─┐┘┤┬┴┼<";
		boxCharacterRightNeighbors [10] = "O─┐┘┤┬┴┼<";

		boxCharacterUpNeighbors [0] = "O─└┘┴X#";
		boxCharacterUpNeighbors [1] = "O│┌┐├┤┬┼";
		boxCharacterUpNeighbors [2] = "O─└┘┴X#";
		boxCharacterUpNeighbors [3] = "O─└┘┴X#";
		boxCharacterUpNeighbors [4] = "O│┌┐├┤┬┼";
		boxCharacterUpNeighbors [5] = "O│┌┐├┤┬┼";
		boxCharacterUpNeighbors [6] = "O│┌┐├┤┬┼∨";
		boxCharacterUpNeighbors [7] = "O│┌┐├┤┬┼∨";
		boxCharacterUpNeighbors [8] = "O─└┘┴X#";
		boxCharacterUpNeighbors [9] = "O│┌┐├┤┬┼∨";
		boxCharacterUpNeighbors [10] = "O│┌┐├┤┬┼∨";

		boxCharacterDownNeighbors [0] = "O─┌┐┬X#";
		boxCharacterDownNeighbors [1] = "O│└┘├┤┴┼";
		boxCharacterDownNeighbors [2] = "O│└┘├┤┴┼";
		boxCharacterDownNeighbors [3] = "O│└┘├┤┴┼";
		boxCharacterDownNeighbors [4] = "O─┌┐┬X#";
		boxCharacterDownNeighbors [5] = "O─┌┐┬X#";
		boxCharacterDownNeighbors [6] = "O│└┘├┤┴┼∧";
		boxCharacterDownNeighbors [7] = "O│└┘├┤┴┼∧";
		boxCharacterDownNeighbors [8] = "O│└┘├┤┴┼∧";
		boxCharacterDownNeighbors [9] = "O─┌┐┬X#";
		boxCharacterDownNeighbors [10] = "O│└┘├┤┴┼∧";
	}
}
