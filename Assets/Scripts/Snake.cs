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
	public bool turned;

	public GameObject edge_tail;

	public AudioClip eat_sound;
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


	
	bool eat = false;
	bool vertical = false;
	bool horizontal = true;
		
	void Start () {
		Vector2 start_pos = transform.position;
		obj_gamger = GameObject.FindGameObjectWithTag("GameManager");
		clGamger = obj_gamger.GetComponent<GameManager> ();
		boardScript=GameObject.FindGameObjectWithTag("GameManager").GetComponent<BoardManage> ();
		InvokeRepeating("Movement", 0.3f, speed);
		edge_tail =(GameObject)Instantiate(edge_tailPrefab, start_pos, Quaternion.identity);
		turned = true;

	}
	
	void Update () {
		if (turned) {
			if (Input.GetKey (KeyCode.RightArrow) && horizontal) {
				
				if((int)transform.eulerAngles.z==(int)180){
					turn_to_cornner=cornner[3];

				}else if((int)transform.eulerAngles.z==(int)0){
					turn_to_cornner=cornner[0];//cornner.topleft;

				}
				transform.rotation= Quaternion.Euler(0,0,-90);
				horizontal = false;
				vertical = true;
				vector = Vector2.up;
				turned=false;
			} else if (Input.GetKey (KeyCode.UpArrow) && vertical) {
				
				if((int)transform.eulerAngles.z==(int)90){
					turn_to_cornner=cornner[3];//(int)cornner.botleft;

				}else if((int)transform.eulerAngles.z==(int)270){
					turn_to_cornner=cornner[2];//(int)cornner.botright;

				}
				transform.rotation= Quaternion.Euler(0,0,0);
				horizontal = true;
				vertical = false;
				vector = Vector2.up;
				turned=false;
			} else if (Input.GetKey (KeyCode.DownArrow) && vertical) {
				
				if((int)transform.eulerAngles.z==(int)90){
					turn_to_cornner=cornner[0];

				}else if((int)transform.eulerAngles.z==(int)270){
					turn_to_cornner=cornner[1];//(int)cornner.topright;

				}
				transform.rotation= Quaternion.Euler(0,0,180);
				horizontal = true;
				vertical = false;
				vector = Vector2.up;
				turned=false;
			} else if (Input.GetKey (KeyCode.LeftArrow) && horizontal) {
				
				if((int)transform.eulerAngles.z==(int)180){
					turn_to_cornner=cornner[2];//(int)cornner.botright;

				}else if((int)transform.eulerAngles.z==(int)0){
					turn_to_cornner=cornner[1];//(int)cornner.topright;

				}
				transform.rotation= Quaternion.Euler(0,0,90);
				horizontal = false;
				vertical = true;
				vector = Vector2.up;
				turned=false;
			}
			if (tail.Count == 0) {
				turn_to_cornner=0;

			}
			moveVector = vector / 3f;

		}

		
	}
	

	
	void Movement() {
		
		Vector2 ta = transform.position;
		if (eat) {

			if (speed > 0.002){
				speed = speed - 0.002f;
			}
			GameObject g;
			if(tail.Count > 0){
				if(transform.eulerAngles.z!=tail.First().eulerAngles.z && tail.First().gameObject.tag!="edge_body"){


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
		turned = true;

	}

	public bool PosFoodAccepted(Vector2 posFood_created){
		string x_temp_food=posFood_created.x.ToString("F1");
		string y_temp_food=posFood_created.y.ToString("F1");

		foreach (var item_vector in boardScript.gridPosNotFood) {
			string x_temp_item_vector= item_vector.x.ToString("F1");
			string y_temp_item_vector= item_vector.y.ToString("F1");

			string x_temp_right= (item_vector.x+1/3f).ToString("F1");
			string y_temp_right= item_vector.y.ToString("F1");
			string x_temp_right_up= (item_vector.x+1/3f).ToString("F1");
			string y_temp_right_up= (item_vector.y+1/3f).ToString("F1");

			string x_temp_up= item_vector.x.ToString("F1");
			string y_temp_up= (item_vector.y+1/3f).ToString("F1");
			string x_temp_up_left= (item_vector.x-1/3f).ToString("F1");
			string y_temp_up_left= (item_vector.y+1/3f).ToString("F1");

			string x_temp_left= (item_vector.x-1/3f).ToString("F1");
			string y_temp_left= item_vector.y.ToString("F1");
			string x_temp_left_down= (item_vector.x-1/3f).ToString("F1");
			string y_temp_left_down= (item_vector.y-1/3f).ToString("F1");

			string x_temp_down= item_vector.x.ToString("F1");
			string y_temp_down= (item_vector.y-1/3f).ToString("F1");
			string x_temp_down_right= (item_vector.x+1/3f).ToString("F1");
			string y_temp_down_right= (item_vector.y-1/3f).ToString("F1");

			if((x_temp_item_vector==x_temp_food && y_temp_item_vector  ==y_temp_food) 
			   || (x_temp_right==x_temp_food && y_temp_right  ==y_temp_food)
			   || (x_temp_up==x_temp_food && y_temp_up  ==y_temp_food)
			   || (x_temp_left==x_temp_food && y_temp_left  ==y_temp_food)
			   || (x_temp_down==x_temp_food && y_temp_down  ==y_temp_food)
			   || (x_temp_right_up==x_temp_food && y_temp_right_up  ==y_temp_food)
			   || (x_temp_up_left==x_temp_food && y_temp_up_left  ==y_temp_food)
			   || (x_temp_left_down==x_temp_food && y_temp_left_down  ==y_temp_food)
			   || (x_temp_down_right==x_temp_food && y_temp_down_right  ==y_temp_food)){
				return true;
			}
		}
		foreach (var item in tail) {
				string x_temp_item=item.position.x.ToString("F1");
				
				string y_temp_item=item.position.y.ToString("F1");
				
				if(x_temp_item==x_temp_food && y_temp_item  ==y_temp_food){
					return true;
				}
				
		}
		return false;


	} 

	
	void OnTriggerEnter2D(Collider2D c) {
		
		if (c.name.StartsWith("food")) {
			AudioSource.PlayClipAtPoint(eat_sound,transform.position);
			eat = true;

			Destroy(c.gameObject);
			clGamger.newFood=true;
		}
		else if(c.name.StartsWith("btnOpenGate")) {
			clGamger.openDoor=1;
			//Application.LoadLevel(1);
		}
	}

}
