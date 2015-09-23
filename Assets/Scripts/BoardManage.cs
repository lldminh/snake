using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random=UnityEngine.Random;

public class BoardManage : MonoBehaviour {

	public int columns=8;
	public int rows=8;
	public GameObject floorTile;
	public GameObject outerWallTile;
	public GameObject food;

	private Transform boardHolder;
	private List<Vector3> gridPositions=new List<Vector3>();

	void InititaliseList(){
		gridPositions.Clear ();
		for (int x = 0; x < columns-1; x++) {
			for (int y = 0; y < rows-1; y++) {
				gridPositions.Add(new Vector3(x,y,0f));
			}
		}
	}

	void boardSetup(){
		boardHolder = new GameObject ("Board").transform;
		for (int x = -1; x < columns+1; x++) {
			for (int y = -1; y < rows+1; y++) {
				GameObject toInstance=floorTile;
				if(x==-1 || x==columns||y==-1 || y==rows ){
					toInstance=outerWallTile;
					if(y==-1 && x!=-1 || x==columns && y!=rows){
						GameObject botFloor_instance=Instantiate(floorTile,new Vector3(x,y,0f),Quaternion.identity) as GameObject;
						botFloor_instance.transform.SetParent(boardHolder);
					}
				}
				GameObject instance=Instantiate(toInstance,new Vector3(x,y,0f),Quaternion.identity) as GameObject;
				instance.transform.SetParent(boardHolder);
			}
		}
	}
	public void SpawnFood() {
		int x = (int)Random.Range (0,columns);
		int y = (int)Random.Range (0,rows);
		
		Instantiate (food,new Vector3(x,y,0f), Quaternion.identity);
	}
	public void SetupScene(){
		boardSetup ();
		InititaliseList ();
		SpawnFood();
	}
}
