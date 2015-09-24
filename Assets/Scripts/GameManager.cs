using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public Snake componentSnake;
	public BoardManage boardScript;
	public GameObject food;
	public bool newFood=false;

	void Awake(){
		boardScript = GetComponent<BoardManage> ();
		componentSnake=GameObject.FindGameObjectWithTag("Snake").GetComponent<Snake> ();
		InitGame ();
	}

	void InitGame ()
	{
		boardScript.SetupScene ();
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
	}


}
