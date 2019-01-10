using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DunGenPlacer : MonoBehaviour
{

	[SerializeField] private DunGen dunGen;
	public GameObject[] walls;
	public GameObject[] floors;
	public GameObject[] ceilings;
	public GameObject[] doors;

	public GameObject[] wallObjects;
	public GameObject[] floorObjects;
	public GameObject[] ceilingObjects;

	public float wallChance = 0.1f;
	public int floorObjectsNumber = 100;
	public float ceilingChance = 0.1f;

	public Transform wallParent;
	public Transform floorParent;
	public Transform ceilingParent;
	public Transform doorParent;
	
	// Use this for initialization
	void Start () {//should be buildMap function called from gameLogic
		dunGen.GenerateMap();
		PlaceTiles(dunGen.map, dunGen.mapRows, dunGen.mapColumns, dunGen.traversedMap);
		PlaceWallObjects();
		PlaceCeilingObjects();
		//small possibility for trap in room
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
						//todo implement doors for <>^
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
			if (Random.Range(0f, 1f) <= wallChance)
			{
				Instantiate(wallObjects[Random.Range(0, wallObjects.Length)], wall);
			}
		}
	}
	
	private void PlaceCeilingObjects()
	{
		foreach (Transform tile in ceilingParent)
		{
			if (Random.Range(0f, 1f) <= ceilingChance)
			{
				Instantiate(ceilingObjects[Random.Range(0, ceilingObjects.Length)], tile);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
