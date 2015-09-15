using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Snake : MonoBehaviour {
	
	public GameObject tailPrefab;
	public GameObject edge_tailPrefab;
	public GameObject edge_bodyPrefab;
	public BoardManage boardScript;
	public GameObject obj_gamger;
	public GameManager clGamger;


	public GameObject edge_tail;
	
	private float speed = 0.1f;
	private int[] cornner = {1,2,3,4};
//		topleft,
//		topright,
//		botright,
//		botleft};
	private int turn_to_cornner=0;
	Vector2 vector = Vector2.up;
	Vector2 moveVector;
	
	List<Transform> tail = new List<Transform>();

	List<Vector3> rotation_edge_lst = new List<Vector3>();
	
	bool eat = false;
	bool vertical = false;
	bool horizontal = true;
		
	void Start () {
		Vector2 start_pos = transform.position;
		obj_gamger = GameObject.FindGameObjectWithTag("GameManager");
		clGamger = obj_gamger.GetComponent<GameManager> ();
		InvokeRepeating("Movement", 0.3f, speed);
		edge_tail =(GameObject)Instantiate(edge_tailPrefab, start_pos, Quaternion.identity);


	}
	
	void Update () {
		if (Input.GetKey (KeyCode.RightArrow) && horizontal) {

			if((int)transform.eulerAngles.z==(int)180){
				turn_to_cornner=cornner[3];
				rotation_edge_lst.Insert(0,Vector3.down/3);
			}else if((int)transform.eulerAngles.z==(int)0){
				turn_to_cornner=cornner[0];//cornner.topleft;
				rotation_edge_lst.Insert(0,Vector3.up/3);
			}
			transform.rotation= Quaternion.Euler(0,0,-90);
			horizontal = false;
			vertical = true;
			vector = Vector2.up;
		} else if (Input.GetKey (KeyCode.UpArrow) && vertical) {
			
			if((int)transform.eulerAngles.z==(int)90){
				turn_to_cornner=cornner[3];//(int)cornner.botleft;
				rotation_edge_lst.Insert(0,Vector3.left/3);
			}else if((int)transform.eulerAngles.z==(int)270){
				turn_to_cornner=cornner[2];//(int)cornner.botright;
				rotation_edge_lst.Insert(0,Vector3.right/3);
			}
			transform.rotation= Quaternion.Euler(0,0,0);
			horizontal = true;
			vertical = false;
			vector = Vector2.up;

		} else if (Input.GetKey (KeyCode.DownArrow) && vertical) {
			
			if((int)transform.eulerAngles.z==(int)90){
				turn_to_cornner=cornner[0];
				rotation_edge_lst.Insert(0,Vector3.left/3);
			}else if((int)transform.eulerAngles.z==(int)270){
				turn_to_cornner=cornner[1];//(int)cornner.topright;
				rotation_edge_lst.Insert(0,Vector3.right/3);
			}
			transform.rotation= Quaternion.Euler(0,0,180);
			horizontal = true;
			vertical = false;
			vector = Vector2.up;

		} else if (Input.GetKey (KeyCode.LeftArrow) && horizontal) {
			
			if((int)transform.eulerAngles.z==(int)180){
				turn_to_cornner=cornner[2];//(int)cornner.botright;
				rotation_edge_lst.Insert(0,Vector3.down/3);
			}else if((int)transform.eulerAngles.z==(int)0){
				turn_to_cornner=cornner[1];//(int)cornner.topright;
				rotation_edge_lst.Insert(0,Vector3.up/3);
			}
			transform.rotation= Quaternion.Euler(0,0,90);
			horizontal = false;
			vertical = true;
			vector = Vector2.up;

		}
		if (tail.Count == 0) {
			turn_to_cornner=0;
			rotation_edge_lst.Clear();
		}
		moveVector = vector / 3f;
		
	}
	

	
	void Movement() {
		
		Vector2 ta = transform.position;
		if (eat) {
			if (speed > 0.002){
				speed = speed - 0.002f;
			}
			GameObject g;
			if(tail.Count > 0){
				if(transform.eulerAngles.z!=tail.First().eulerAngles.z){
					Debug.Log("Thang dau tien:"+tail.First().eulerAngles.z);
					Debug.Log(turn_to_cornner);
					Debug.Log("Cai dau ran:"+ transform.eulerAngles.z);
					g =(GameObject)Instantiate(edge_bodyPrefab, ta, Quaternion.identity);
					switch(turn_to_cornner){
					case 1:g.transform.rotation = Quaternion.Euler(0,0,0); //topleft
						break;
					case 2:g.transform.rotation = Quaternion.Euler(0,0,-90); //topright
						break;
					case 3:g.transform.rotation = Quaternion.Euler(0,0,180); //botright
						break;
					case 4:g.transform.rotation = Quaternion.Euler(0,0,90); //botleft
						break;
					default: break;
					}
					turn_to_cornner=0;
				}else{
					g =(GameObject)Instantiate(tailPrefab, ta, Quaternion.identity);
					g.transform.rotation=transform.rotation;
				}
			}else{
				g =(GameObject)Instantiate(tailPrefab, ta, Quaternion.identity);
				g.transform.rotation=transform.rotation;
			}


			tail.Insert(0, g.transform);
			Debug.Log(speed);
			eat = false;
		}
		else if (tail.Count > 0) {

			if(turn_to_cornner==0){
				Vector2 edge_tail_pos = tail.Last().position;

				GameObject g =(GameObject)Instantiate(tailPrefab, ta, Quaternion.identity);
				g.transform.rotation=transform.rotation;
				tail.Insert(0, g.transform);
										
				//set position and rotation of tail

				Destroy(tail.Last().gameObject);
				tail.RemoveAt(tail.Count-1);

				edge_tail.transform.position=edge_tail_pos;
				edge_tail.transform.rotation=tail.Last().rotation;

			}else{
				Vector2 edge_tail_pos = tail.Last().position;

				GameObject edgeBody_obj =(GameObject)Instantiate(edge_bodyPrefab, ta, Quaternion.identity);
				switch(turn_to_cornner){
				case 1:edgeBody_obj.transform.rotation = Quaternion.Euler(0,0,0); //topleft
					break;
				case 2:edgeBody_obj.transform.rotation = Quaternion.Euler(0,0,-90); //topright
					break;
				case 3:edgeBody_obj.transform.rotation = Quaternion.Euler(0,0,180); //botright
					break;
				case 4:edgeBody_obj.transform.rotation = Quaternion.Euler(0,0,90); //botleft
					break;
				default: break;
				}
				tail.Insert(0, edgeBody_obj.transform);

				Destroy(tail.Last().gameObject);
				tail.RemoveAt(tail.Count-1);

				edge_tail.transform.position=edge_tail_pos;
				edge_tail.transform.rotation=tail.Last().rotation;

				turn_to_cornner=0;
			
			}


		}
		else if(tail.Count==0){
			edge_tail.transform.position=ta;
			edge_tail.transform.rotation=transform.rotation;
		}
		transform.Translate(moveVector);
	}

	
	void OnTriggerEnter2D(Collider2D c) {
		
		if (c.name.StartsWith("food")) {
			eat = true;

			Destroy(c.gameObject);
			clGamger.newFood=true;
		}
		else {
			//Application.LoadLevel(1);
		}
	}
}
