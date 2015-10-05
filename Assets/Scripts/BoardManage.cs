using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random=UnityEngine.Random;

public class BoardManage : MonoBehaviour {

	public int columns=8;
	public int rows=8;
	public float xDoor=6.61f;
	public float yDoor=6.47f;
	public GameObject floorTile;
	public GameObject outerWallTile;
	public GameObject gateExit;
	public GameObject btnOpen;
	public GameObject doorLock;
	public GameObject[] impediment;

	private Transform boardHolder;
	private List<Vector3> gridPositions=new List<Vector3>();
	public List<Vector3> gridPosNotFood=new List<Vector3>();
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
		SetBoardForLevel (Application.loadedLevel);

		//SpawnFood();
	}

	public void SetBoardForLevel(int _level){
		switch (_level) {
		case 1:
			level_1();
			break;
		default:
			break;
		}
	}

	public void level_1(){
		Vector3 pos_gateExit = new Vector3 (columns - 4 / 3f, rows - 4 / 3f, 0f); 
		Vector3 pos_doorLock = new Vector3 (xDoor, yDoor, 0f);
		Vector3 pos_btnOpen = new Vector3 (6+2/3f, 5f, 0f);

		Vector3 pos_impediment_01 = new Vector3 (4f, 4f, 0f);
		Vector3 pos_impediment_02 = new Vector3 (1f, 1f, 0f);
		Vector3 pos_impediment_03 = new Vector3 (6f, 2f, 0f);

		Instantiate (gateExit, pos_gateExit, Quaternion.identity);
		Instantiate (doorLock, pos_doorLock, Quaternion.identity);
		Instantiate (btnOpen, pos_btnOpen, Quaternion.identity);
		Instantiate (impediment[0], pos_impediment_01, Quaternion.identity);
		Instantiate (impediment[1], pos_impediment_02, Quaternion.identity);
		Instantiate (impediment[2], pos_impediment_03, Quaternion.identity);

		gridPosNotFood.Add (pos_gateExit);
		gridPosNotFood.Add (pos_doorLock);
		//gridPosNotFood.Add (pos_btnOpen);
		gridPosNotFood.Add (pos_impediment_01);
		gridPosNotFood.Add (pos_impediment_02);
		gridPosNotFood.Add (pos_impediment_03);
	}
}
