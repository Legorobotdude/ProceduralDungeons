using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DunGenPlacerPrefab : DunGenPlacer {
	
	[Header("These arrays should contain the prefabs for ─│┌┐└┘├┤┬┴┼O#")]
	[Header("─ prefabs")]
	[SerializeField]private GameObject[] Prefabs1;
	[Header("│ prefabs")]
	[SerializeField]private GameObject[] Prefabs2;
	[Header("┌ prefabs")]
	[SerializeField]private GameObject[] Prefabs3;
	[Header("┐ prefabs")]
	[SerializeField]private GameObject[] Prefabs4;
	[Header("└ prefabs")]
	[SerializeField]private GameObject[] Prefabs5;
	[Header("┘ prefabs")]
	[SerializeField]private GameObject[] Prefabs6;
	[Header("├ prefabs")]
	[SerializeField]private GameObject[] Prefabs7;
	[Header("┤ prefabs")]
	[SerializeField]private GameObject[] Prefabs8;
	[Header("┬ prefabs")]
	[SerializeField]private GameObject[] Prefabs9;
	[Header("┴ prefabs")]
	[SerializeField]private GameObject[] Prefabs10;
	[Header("┼ prefabs")]
	[SerializeField]private GameObject[] Prefabs11;
	[Header("O prefabs (When all 4 walls are present")]
	[SerializeField] private GameObject[] Prefabs12;
	[Header("# prefabs (When no walls are present")]
	[SerializeField] private GameObject[] Prefabs13;

	[SerializeField] private GameObject[] DoorPrefabs;

	[SerializeField] private Transform dungeonParent;
	[SerializeField] private float scale = 5f;
	
	protected override void PlaceDungeon()
	{
		if (dungeonParent == null)
		{
			dungeonParent = this.transform;
		}
		PlacePrefabs(dunGen.map, dunGen.mapRows, dunGen.mapColumns, dunGen.traversedMap);
	}

	private void PlacePrefabs(char[,] map, int mapRows, int mapColumns, bool[,] placementMap)
	{
		for (int r = 0; r < mapRows; r++)
		{
			for (int c = 0; c < mapColumns; c++)
			{
				if (placementMap[r, c])
				{
					
					switch (map [r, c])
					{
						case '─':
							Instantiate(Prefabs1[Random.Range(0, Prefabs1.Length)], new Vector3(scale * r, 0f, scale * c), Quaternion.Euler(0, 0, 0),dungeonParent);
							break;
						case '│':
							Instantiate(Prefabs2[Random.Range(0, Prefabs2.Length)], new Vector3(scale * r, 0f, scale * c), Quaternion.Euler(0, 0, 0),dungeonParent);
							break;
						case '┌':
							Instantiate(Prefabs3[Random.Range(0, Prefabs3.Length)], new Vector3(scale * r, 0f, scale * c), Quaternion.Euler(0, 0, 0),dungeonParent);
							break;
						case '┐':
							Instantiate(Prefabs4[Random.Range(0, Prefabs4.Length)], new Vector3(scale * r, 0f, scale * c), Quaternion.Euler(0, 0, 0),dungeonParent);
							break;
						case '└':
							Instantiate(Prefabs5[Random.Range(0, Prefabs5.Length)], new Vector3(scale * r, 0f, scale * c), Quaternion.Euler(0, 0, 0),dungeonParent);
							break;
						case '┘':
							Instantiate(Prefabs6[Random.Range(0, Prefabs6.Length)], new Vector3(scale * r, 0f, scale * c), Quaternion.Euler(0, 0, 0),dungeonParent);
							break;
						case '├':
							Instantiate(Prefabs7[Random.Range(0, Prefabs7.Length)], new Vector3(scale * r, 0f, scale * c), Quaternion.Euler(0, 0, 0),dungeonParent);
							break;
						case '┤':
							Instantiate(Prefabs8[Random.Range(0, Prefabs8.Length)], new Vector3(scale * r, 0f, scale * c), Quaternion.Euler(0, 0, 0),dungeonParent);
							break;
						case '┬':
							Instantiate(Prefabs9[Random.Range(0, Prefabs9.Length)], new Vector3(scale * r, 0f, scale * c), Quaternion.Euler(0, 0, 0),dungeonParent);
							break;
						case '┴':
							Instantiate(Prefabs10[Random.Range(0, Prefabs10.Length)], new Vector3(scale * r, 0f, scale * c), Quaternion.Euler(0, 0, 0),dungeonParent);
							break;
						case '┼':
							Instantiate(Prefabs11[Random.Range(0, Prefabs11.Length)], new Vector3(scale * r, 0f, scale * c), Quaternion.Euler(0, 0, 0),dungeonParent);
							break;
						//todo implement doors for <>^
						case '<':
							//Instantiate(doors[Random.Range(0, doors.Length)], new Vector3(5 * r - 5, 0f, 5 * c), Quaternion.Euler(0, 180, 0),doorParent);//right
							//break;
						case '>':
							//Instantiate(doors[Random.Range(0, doors.Length)], new Vector3(5 * r, 0f, 5 * c + 5), Quaternion.Euler(0, 0, 0),doorParent);//right
							//break;
						case '∧':
							//Instantiate(doors[Random.Range(0, doors.Length)], new Vector3(5 * r - 5, 0f, 5 * c + 5), Quaternion.Euler(0, 270, 0),doorParent);
							//break;
						case '∨':
							//Instantiate(doors[Random.Range(0, doors.Length)], new Vector3(5 * r, 0f, 5 * c), Quaternion.Euler(0, 90, 0),doorParent);
							//break;
						case '#':
							Instantiate(Prefabs13[Random.Range(0, Prefabs13.Length)], new Vector3(scale * r, 0f, scale * c), Quaternion.Euler(0, 0, 0),dungeonParent);
							break;
						case 'O':
							Instantiate(Prefabs12[Random.Range(0, Prefabs12.Length)], new Vector3(scale * r, 0f, scale * c), Quaternion.Euler(0, 0, 0),dungeonParent);
							break;
			
					}
				}
				
			}
		}
	}
}
