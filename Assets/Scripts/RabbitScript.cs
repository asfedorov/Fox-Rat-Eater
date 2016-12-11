using UnityEngine;
using System.Collections;

public class RabbitScript : MonoBehaviour {
	private Vector2 movement = new Vector2(0,0);
	private Vector2 current_movement;
	private int rotate_to;
	public Vector2 speed = new Vector2(3.0f,3.0f);

	public Vector2 last_movement = new Vector2(0,0);

	private int global_rotation = 0;

	public float time_to_live = 5f;

	private float time_to_jump = 1f;

	private float air_time = 0.3f;

	private bool i_am_jumping = false;

	public Sprite jump;
	public Sprite stand;


	// Use this for initialization
	void Start () {
		MoveRabbit ();
		//InvokeRepeating ("MoveRabbit", 0, time_to_jump);
		//print (time_to_jump);
	}

	// Update is called once per frame
	void Update () {
		time_to_live -= Time.deltaTime;
		if (time_to_live < 0) {
			Destroy (gameObject);
		}

		if (i_am_jumping == false) {
			time_to_jump -= Time.deltaTime;
			//print (time_to_jump); 
			if (time_to_jump < 0) {
				MoveRabbit();
			}		
		}


		if (i_am_jumping) {
			air_time -= Time.deltaTime;
			if (air_time < 0) {
				StopRabbit();

			}	
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

	private int StopRabbit() {
		gameObject.GetComponent<SpriteRenderer> ().sprite = stand;
		i_am_jumping = false;
		current_movement.x = 0;
		current_movement.y = 0;
		time_to_jump = Random.Range(0.2f, 0.5f);
		air_time = 0.5f;

		Light rabbit_light = gameObject.GetComponent<Light> ();

		rabbit_light.intensity = 1.6f;
		rabbit_light.range = 8;

		AudioSource audio = gameObject.GetComponent<AudioSource> ();
		audio.Play ();

		return 0;
	}

	private int MoveRabbit () {

		gameObject.GetComponent<SpriteRenderer> ().sprite = jump;
		gameObject.transform.localScale = new Vector3 (1f, 1f, 1);

		i_am_jumping = true;

		int greate_random_vector = 0;

		bool go_go = false;

		while(go_go==false){
			greate_random_vector = Random.Range (0, 4);
			go_go = is_move_possible(greate_random_vector);
		}

		if (greate_random_vector == 0) {

			movement.y = -1;
			movement.x = 0;
		}
		if (greate_random_vector == 1) {
			movement.y = 1;
			movement.x = 0;
		}
		if (greate_random_vector == 2) {
			movement.x = -1;
			movement.y = 0;
		}
		if (greate_random_vector == 3) {
			movement.x = 1;
			movement.y = 0;
		}

		last_movement = current_movement;
		current_movement = movement;

		return 0;
	}

	private bool is_move_possible(int move_vector){
		float distance = 0;
		if (move_vector == 0) {
			//down
			distance = gameObject.transform.position.y - GameObject.Find("hence_bottom").transform.position.y;
		}
		if (move_vector == 1) {
			//up
			distance = GameObject.Find("hence_top").transform.position.y - gameObject.transform.position.y;
		}
		if (move_vector == 2) {
			//left
			distance = gameObject.transform.position.x - GameObject.Find("hence_left").transform.position.x;
		}
		if (move_vector == 3) {
			//right
			distance = GameObject.Find("hence_right").transform.position.x - gameObject.transform.position.x;
		}
		//		print (distance);

		if (distance < 0.30f) {
			return false;		
		} else {
			return true;
		}
	}

	IEnumerator RabbitHighlight(){
		yield return new WaitForSeconds(0.5f);

		Light rabbit_light = gameObject.GetComponent<Light> ();

		rabbit_light.intensity = 0f;
		rabbit_light.range = 0;
	}
}
