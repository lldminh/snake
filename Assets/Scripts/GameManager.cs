using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public Snake componentSnake;
	public BoardManage boardScript;
	public GameObject doorLock;
	public GameObject food;
	public bool newFood=false;
	public int openDoor=0;

	void Awake(){

		boardScript = GetComponent<BoardManage> ();
		InitGame ();



	}

	void InitGame ()
	{
		boardScript.SetupScene ();
		componentSnake=GameObject.FindGameObjectWithTag("Snake").GetComponent<Snake> ();
		doorLock=GameObject.FindGameObjectWithTag("DoorLock");
		newFood = true; //create first food;
	}
	void Update () {
		if (newFood) {
			bool acceptPos=true;
			Vector2 temp_pos= new Vector2(0,0);
			while (acceptPos) {
				temp_pos= boardScript.SpawnFood ();
				acceptPos=componentSnake.PosFoodAccepted(temp_pos);
			}
			Instantiate (food,new Vector3(temp_pos.x,temp_pos.y,0f), Quaternion.identity);
			newFood=false;
		}
		if (openDoor==1) {
			doorLock.GetComponent<Animator> ().SetFloat ("setSpeed", 10f);
			doorLock.GetComponent<Animator> ().SetBool ("isOpen", true);

			Debug.Log("OPEN DE");
			openDoor=0;
		} else if (openDoor==-1) {
			//doorLock.GetComponent<Animator> ().SetFloat ("setSpeed", -10f);
			doorLock.GetComponent<Animator> ().SetBool ("isOpen", false);
			Debug.Log("close DE");
			openDoor=0;


		}
	}


}
