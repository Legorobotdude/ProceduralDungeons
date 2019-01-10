using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DunGenPlacer : MonoBehaviour {
	
	[SerializeField] protected DunGen dunGen;
	[SerializeField] private bool generateOnStart = true;

	void Start () {
		
		if (dunGen == null)
		{
			dunGen = FindObjectOfType<DunGen>();
			if (dunGen == null)
			{
				dunGen = gameObject.AddComponent<DunGen>();
			}
		}
		
		if (generateOnStart)
		{
			BuildDungeon();
		}
		
		
	}

	public void BuildDungeon()
	{
		dunGen.GenerateMap();
		PlaceDungeon();
	}

	public void BuildDungeon(int mapRows, int mapColumns, int minCells = 10, int numRooms = 1, int startX = 1, int startY = 1, bool printMap = false)
	{
		dunGen.mapRows = mapRows;
		dunGen.mapColumns = mapColumns;
		dunGen.minCells = minCells;
		dunGen.numRooms = numRooms;
		dunGen.printMapAfterGen = printMap;
		dunGen.GenerateMap(startX, startY);
		PlaceDungeon();
	}
	

	protected abstract void PlaceDungeon();
}
