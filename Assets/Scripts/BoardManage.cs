using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random=UnityEngine.Random;

public class BoardManage : MonoBehaviour {

	public int columns=8;
	public int rows=8;
	public GameObject floorTile;
	public GameObject outerWallTile;
	public GameObject gateExit;
	public GameObject btnOpen;

	private Transform boardHolder;
	private List<Vector3> gridPositions=new List<Vector3>();
	private float[] num_array=new float[]{0,1/3f,2/3f,1};

	void InititaliseList(){
		gridPositions.Clear ();
		for (float x = 0; x < columns-1; x+=1/3f) {
			for (float y = 0; y < rows-1; y+=1/3f) {
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
	public Vector2 SpawnFood() {
		int temp_index = Random.Range (0, num_array.Length); //index random of num_array
		float x = (float)Random.Range (0,columns-2)+num_array[temp_index];
		float y = (float)Random.Range (0,rows-2)+num_array[temp_index];
		Vector2 temp_pos = new Vector2 (x, y);
		return temp_pos;
	}
	public void SetupScene(){
		boardSetup ();
		InititaliseList ();
		Instantiate (gateExit, new Vector3 (columns - 4/3f, rows - 4/3f, 0f), Quaternion.identity);
		Instantiate (btnOpen, new Vector3 (columns - 3f, rows - 4/3f, 0f), Quaternion.identity);
		//SpawnFood();
	}
}
