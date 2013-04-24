using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class MapPlacementScript : MonoBehaviour 
{
	public GameObject Tile0;
	public GameObject Tile1;
	public GameObject Tile2;
	public GameObject Tile3;
	public GameObject Tile4;
	public GameObject Tile5;
	public GameObject Tile6;
	public GameObject Tile7;
	public GameObject Tile8;
	public GameObject Tile9;
	private Dictionary<string,GameObject> Tiles = new Dictionary<string, GameObject>();
	public int tileSize = 10;
	
	public List<int[]> numList = new List<int[]>();
	public Vector3 corner = new Vector3(0,0,0);	
	public Vector3 tileAngle = new Vector3(270,0,90);
	
	void Start () 
	{
		Tiles.Add ("Tile0",Tile0);
		Tiles.Add ("Tile1",Tile1);
		Tiles.Add ("Tile2",Tile2);
		Tiles.Add ("Tile3",Tile3);
		Tiles.Add ("Tile4",Tile4);
		Tiles.Add ("Tile5",Tile5);
		Tiles.Add ("Tile6",Tile6);
		Tiles.Add ("Tile7",Tile7);
		Tiles.Add ("Tile8",Tile8);
		Tiles.Add ("Tile9",Tile9);
		this.LoadLevel (2);
	}
	
	void LoadLevel(int level)
	{
		string levelFile = "level" + level + ".txt"; //The name of the file to be loaded
		try
		{
			using (StreamReader reader = new StreamReader(levelFile))
			{
				string line;
				while ((line = reader.ReadLine ()) != null)
				{
					print (line);
					string[] nums = line.Split (';');
					int[] intNums = new int[nums.Length];
					print (nums.Length);
					for (int i = 0; i < nums.Length; i++)
					{
						intNums[i] = Convert.ToInt32 (nums[i]);
						print (intNums[i]);
					}
					numList.Add(intNums);
				}
				print (numList);
			}
			for (int i = 0; i < numList.Count; i++)
			{
				for (int j = 0; j < numList[i].Length; j++)
				{
					string tileName = "Tile" + numList[i][j];
					print(tileName);
					Vector3 renderPosition = new Vector3(corner.x - j*tileSize, corner.y, corner.z + i*tileSize);
					if (Tiles.ContainsKey(tileName))
					{
						Instantiate (Tiles[tileName],renderPosition,Quaternion.Euler(tileAngle));
					}
				}
			}
		}
		catch (Exception e) 
        {
            Console.WriteLine("The process failed: {0}", e.ToString());
        }
	}
	
	void Update () 
	{
	}
}
