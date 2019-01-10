using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DunGenPlacerTiles : DunGenPlacer
{
	[Header("Prefabs for the tiles that make up the walls, floors, and ceilings")]
	public GameObject[] walls;
	public GameObject[] floors;
	public GameObject[] ceilings;
	public GameObject[] doors;
	
	[Header("Prefabs for objects that have a chance of being attached to their respective tile")]
	public GameObject[] wallObjects;
	public GameObject[] floorObjects;
	public GameObject[] ceilingObjects;
	
	[Header("The chance that a tile will have an object attached")]
	public float wallObjectsChance = 0f;
	public float floorObjectsChance = 0f;
	public float ceilingObjectsChance = 0f;
	
	[Header("The object all tiles are parented to")]
	public Transform wallParent;
	public Transform floorParent;
	public Transform ceilingParent;
	public Transform doorParent;
	
	

	protected override void PlaceDungeon()
	{
		PlaceTiles(dunGen.map, dunGen.mapRows, dunGen.mapColumns, dunGen.traversedMap);
		PlaceWallObjects();
		PlaceCeilingObjects();
		//todo add small possibility for trap in room
	}

	private void PlaceTiles(char[,] map, int mapRows, int mapColumns, bool[,] placementMap)
	{
		for (int r = 0; r < mapRows; r++)
		{
			for (int c = 0; c < mapColumns; c++)
			{
				if (placementMap[r, c])
				{
					//place floor/ceiling
					Instantiate(floors[Random.Range(0, floors.Length)], new Vector3(5 * r, 0f, 5 * c), Quaternion.identity, floorParent);
					Instantiate(ceilings[Random.Range(0, ceilings.Length)], new Vector3(5 * r, 5, 5 * c + 5),  Quaternion.AngleAxis(180, Vector3.right), ceilingParent);
					
					switch (map [r, c])
					{
						case '─':
							Instantiate(walls[Random.Range(0, walls.Length)], new Vector3(5 * r - 5, 0f, 5 * c), Quaternion.Euler(0, 90, 0),wallParent);//above
							Instantiate(walls[Random.Range(0, walls.Length)], new Vector3(5 * r, 0f, 5 * c + 5), Quaternion.Euler(0, 270, 0),wallParent);//below
							break;
						case '│':
							Instantiate(walls[Random.Range(0, walls.Length)], new Vector3(5 * r, 0f, 5 * c), Quaternion.Euler(0, 0, 0),wallParent);//left
							Instantiate(walls[Random.Range(0, walls.Length)], new Vector3(5 * r - 5, 0f, 5 * c + 5), Quaternion.Euler(0, 180, 0),wallParent);//right
							break;
						case '┌':
							Instantiate(walls[Random.Range(0, walls.Length)], new Vector3(5 * r - 5, 0f, 5 * c), Quaternion.Euler(0, 90, 0),wallParent);
							Instantiate(walls[Random.Range(0, walls.Length)], new Vector3(5 * r, 0f, 5 * c), Quaternion.Euler(0, 0, 0),wallParent);
							break;
						case '┐':
							Instantiate(walls[Random.Range(0, walls.Length)], new Vector3(5 * r - 5, 0f, 5 * c), Quaternion.Euler(0, 90, 0),wallParent);
							Instantiate(walls[Random.Range(0, walls.Length)], new Vector3(5 * r - 5, 0f, 5 * c + 5), Quaternion.Euler(0, 180, 0),wallParent);
							break;
						case '└':
							Instantiate(walls[Random.Range(0, walls.Length)], new Vector3(5 * r, 0f, 5 * c + 5), Quaternion.Euler(0, 270, 0),wallParent);
							Instantiate(walls[Random.Range(0, walls.Length)], new Vector3(5 * r, 0f, 5 * c), Quaternion.Euler(0, 0, 0),wallParent);//left
							break;
						case '┘':
							Instantiate(walls[Random.Range(0, walls.Length)], new Vector3(5 * r, 0f, 5 * c + 5), Quaternion.Euler(0, 270, 0),wallParent);
							Instantiate(walls[Random.Range(0, walls.Length)], new Vector3(5 * r - 5, 0f, 5 * c + 5), Quaternion.Euler(0, 180, 0),wallParent);
							break;
						case '├':
							Instantiate(walls[Random.Range(0, walls.Length)], new Vector3(5 * r, 0f, 5 * c), Quaternion.Euler(0, 0, 0),wallParent);//left
							break;
						case '┤':
							Instantiate(walls[Random.Range(0, walls.Length)], new Vector3(5 * r - 5, 0f, 5 * c + 5), Quaternion.Euler(0, 180, 0),wallParent);//right
							break;
						case '┬':
							Instantiate(walls[Random.Range(0, walls.Length)], new Vector3(5 * r - 5, 0f, 5 * c), Quaternion.Euler(0, 90, 0),wallParent);//above
							break;
						case '┴':
							Instantiate(walls[Random.Range(0, walls.Length)], new Vector3(5 * r, 0f, 5 * c + 5), Quaternion.Euler(0, 270, 0),wallParent);//below
							break;
						case '┼':
							break;
						case '<':
							Instantiate(doors[Random.Range(0, doors.Length)], new Vector3(5 * r - 5, 0f, 5 * c), Quaternion.Euler(0, 180, 0),doorParent);//right
							break;
						case '>':
							Instantiate(doors[Random.Range(0, doors.Length)], new Vector3(5 * r, 0f, 5 * c + 5), Quaternion.Euler(0, 0, 0),doorParent);//right
							break;
						case '∧':
							Instantiate(doors[Random.Range(0, doors.Length)], new Vector3(5 * r - 5, 0f, 5 * c + 5), Quaternion.Euler(0, 270, 0),doorParent);
							break;
						case '∨':
							Instantiate(doors[Random.Range(0, doors.Length)], new Vector3(5 * r, 0f, 5 * c), Quaternion.Euler(0, 90, 0),doorParent);
							break;
						case 'O':
							Instantiate(walls[Random.Range(0, walls.Length)], new Vector3(5 * r - 5, 0f, 5 * c), Quaternion.Euler(0, 90, 0),wallParent);//above
							Instantiate(walls[Random.Range(0, walls.Length)], new Vector3(5 * r, 0f, 5 * c + 5), Quaternion.Euler(0, 270, 0),wallParent);//below
							Instantiate(walls[Random.Range(0, walls.Length)], new Vector3(5 * r, 0f, 5 * c), Quaternion.Euler(0, 0, 0),wallParent);//left
							Instantiate(walls[Random.Range(0, walls.Length)], new Vector3(5 * r - 5, 0f, 5 * c + 5), Quaternion.Euler(0, 180, 0),wallParent);//right
							break;
			
					}
				}
				
			}
		}
	}

	private void PlaceWallObjects()
	{
		foreach (Transform wall in wallParent)
		{
			if (Random.Range(0f, 1f) <= wallObjectsChance)
			{
				Instantiate(wallObjects[Random.Range(0, wallObjects.Length)], wall);
			}
		}
	}
	
	private void PlaceCeilingObjects()
	{
		foreach (Transform tile in ceilingParent)
		{
			if (Random.Range(0f, 1f) <= ceilingObjectsChance)
			{
				Instantiate(ceilingObjects[Random.Range(0, ceilingObjects.Length)], tile);
			}
		}
	}
	
	private void PlaceFloorObjects()
	{
		foreach (Transform tile in floorParent)
		{
			if (Random.Range(0f, 1f) <= floorObjectsChance)
			{
				Instantiate(floorObjects[Random.Range(0, floorObjects.Length)], tile);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
