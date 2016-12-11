using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player : MonoBehaviour {

	public Vector2 speed = new Vector2(3f,3f);
	private Vector2 movement = new Vector2(0,0);
	private Vector2 current_movement;
	private int rotate_to;

	public Vector2 last_movement = new Vector2(0,0);

	private int global_rotation = 0;

	public Transform ratPrefab;
	public Transform bloodPrefab;
	public Transform rabbitPrefab;

	public bool turning = true;

	public int kill_count = 0;

	public int score = 0;

	public Vector2 start_position = new Vector2 (0, 0);

	// Use this for initialization
	void Start () {
		SpawnRat ();
		SpawnRat();
		SpawnRat();
//		SpawnRat();
//		SpawnRat();
		InvokeRepeating ("MoveFoxy", 0, 0.1f);
		InvokeRepeating ("SpawnRat", 0, 10.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.DownArrow)) {

			movement.y = -1;
			movement.x = 0;
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			movement.y = 1;
			movement.x = 0;
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			movement.x = -1;
			movement.y = 0;
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			movement.x = 1;
			movement.y = 0;
		}

		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			speed = new Vector2(4.5f,4.5f);
			turning = false;
		}
		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			speed = new Vector2(3f,3f);
			turning = true;
		}

		if (current_movement.x == 0 && current_movement.y == 1) {
			if (global_rotation == 90) {
				global_rotation = 0;
				rotate_to = -90;

			} else if (global_rotation == 270) {
				global_rotation = 0;
				rotate_to = -270;

			} else if (global_rotation == 180) {
				global_rotation = 0;
				rotate_to = -180;

			} else if (global_rotation == 0) {
				rotate_to = 0;
			}
		} else if (current_movement.x == 0 && current_movement.y == -1) {
			if (global_rotation == 90) {
				global_rotation = 180;
				rotate_to = 90;

			} else if (global_rotation == 270) {
				global_rotation = 180;
				rotate_to = -90;

			} else if (global_rotation == 0) {
				global_rotation = 180;
				rotate_to = 180;

			} else if (global_rotation == 180) {
				rotate_to = 0;
			}
		} else if (current_movement.x == 1 && current_movement.y == 0) {
			if (global_rotation == 90) {
				global_rotation = 270;
				rotate_to = 180;

			} else if (global_rotation == 180) {
				global_rotation = 270;
				rotate_to = 90;

			} else if (global_rotation == 0) {
				global_rotation = 270;
				rotate_to = 270;
			} else if (global_rotation == 270) {
				rotate_to = 0;
			}
		} else if (current_movement.x == -1 && current_movement.y == 0) {
			if(global_rotation==180){
				global_rotation = 90;
				rotate_to = -90;

			} else if(global_rotation==270){
				global_rotation = 90;
				rotate_to = -180;

			} else if(global_rotation==0){
				global_rotation = 90;
				rotate_to = 90;

			} else if(global_rotation==90){
				rotate_to = 0;
			}
		}

		Vector3 rotation_vector = new Vector3 (0, 0, rotate_to);
		transform.Rotate(rotation_vector);
	}

	void FixedUpdate(){

		GetComponent<Rigidbody2D>().velocity = new Vector2 (
			current_movement.x * speed.x,
			current_movement.y * speed.y);



	}

	private int MoveFoxy(){
		if (turning) {
			last_movement = current_movement;
			current_movement = movement;

			if (last_movement != current_movement) {

				transform.position = new Vector3 (
					transform.position.x + last_movement.x / 5,
					transform.position.y + last_movement.y / 5,
					transform.position.z
				);
			}
		}

		return 0;
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (!collider.isTrigger) {
			rat rat = collider.gameObject.GetComponent<rat> ();
			if (rat != null) {
				Vector3 rat_pos = rat.gameObject.transform.position;
				Destroy (rat.gameObject);

				GameObject background = GameObject.FindGameObjectWithTag ("FOREGROUND");
				var bloodTransform = Instantiate (bloodPrefab) as Transform;

				bloodTransform.parent = background.transform;
				bloodTransform.localScale = new Vector3(0.5f, 0.5f, 1);
				bloodTransform.position = new Vector3( rat_pos.x, rat_pos.y, -6);

				kill_count++;

				if (kill_count % 5 == 0){
					SpawnRabbit();
				}


				int timer_bonus = 10;
				int score_bonus = 10;

				if(Mathf.Abs(rat_pos.x)>2.8f && Mathf.Abs(rat_pos.y)>2.8f){
					timer_bonus = 3;
					score_bonus = 1;

				} else if(Mathf.Abs(rat_pos.x)>2.8f || Mathf.Abs(rat_pos.y)>2.8f){
					timer_bonus = 5;
					score_bonus = 3;
				}

				GameObject timer = GameObject.Find("TimeLeft");
				timer.GetComponent<TimerScript>().timer += timer_bonus;

				score += score_bonus;

				Text score_text = GameObject.Find("Score").GetComponent<Text>();;
				score_text.text = score.ToString();

			}
			RabbitScript rabbit = collider.gameObject.GetComponent<RabbitScript> ();
			if (rabbit != null) {
				Vector3 rabbit_pos = rabbit.gameObject.transform.position;
				Destroy (rabbit.gameObject);

				GameObject background = GameObject.FindGameObjectWithTag ("FOREGROUND");
				var bloodTransform = Instantiate (bloodPrefab) as Transform;

				bloodTransform.parent = background.transform;
				bloodTransform.localScale = new Vector3(1.1f, 1.1f, 1);
				bloodTransform.position = new Vector3( rabbit_pos.x, rabbit_pos.y, -6);

				//kill_count++;

				score += 15;

				Text score_text = GameObject.Find("Score").GetComponent<Text>();;
				score_text.text = score.ToString();

				int timer_bonus = 15;

				GameObject timer = GameObject.Find("TimeLeft");
				timer.GetComponent<TimerScript>().timer += timer_bonus;
			}
		}
	}

	private int SpawnRat(){
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag ("RAT");
		GameObject background = GameObject.FindGameObjectWithTag ("FOREGROUND");

		if (gos.Length < 5) {

			float random_x = Random.Range (-8.0f, 4.0f);
			float random_y = Random.Range (-4.0f, 4.0f);

			var ratTransform = Instantiate (ratPrefab) as Transform;


			ratTransform.position = new Vector3 (random_x, random_y, -6);
			ratTransform.parent = background.transform;
		}


		return 0;
	}

	private int SpawnRabbit(){
		float random_x = Random.Range (-5.0f, 5.0f);
		float random_y = Random.Range (-3.0f, 3.0f);

		GameObject background = GameObject.FindGameObjectWithTag ("FOREGROUND");

		var rabbitTransform = Instantiate (rabbitPrefab) as Transform;

		rabbitTransform.position = new Vector3 (random_x, random_y, -6);
		rabbitTransform.parent = background.transform;

		return 0;
	}
}
