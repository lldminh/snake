using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public BoardManage boardScript;
	public bool newFood=false;

	void Awake(){
		boardScript = GetComponent<BoardManage> ();
		InitGame ();
	}

	void InitGame ()
	{
		boardScript.SetupScene ();

	}
	void Update () {
		if (newFood) {
			boardScript.SpawnFood ();
			newFood=false;
		}
	}
}
