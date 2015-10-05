using UnityEngine;
using System.Collections;

public class TailSnake : MonoBehaviour {
	public GameObject obj_gamger;
	public GameManager clGamger;
	// Use this for initialization
	void Start () {
		obj_gamger = GameObject.FindGameObjectWithTag("GameManager");
		clGamger = obj_gamger.GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerExit2D(Collider2D c) {
		if(c.name.StartsWith("btnOpenGate")) {
			clGamger.openDoor=-1;
			//Application.LoadLevel(1);
		}
	}
}
