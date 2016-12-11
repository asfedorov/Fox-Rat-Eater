using UnityEngine;
using System.Collections;

public class rat : MonoBehaviour {
	public Vector2 speed = new Vector2(1.5f,1.5f);
	private Vector2 movement = new Vector2(0,0);
	private Vector2 current_movement;
	private int rotate_to;

	public Vector2 last_movement = new Vector2(0,0);

	private int global_rotation = 0;

	public bool panic = false;

	private Vector2 from_fox = new Vector2 (0, 0);

	private float change_direction_rate = 0.3f;

	private Vector2 fence_direction = new Vector2(0,0);

	// Use this for initialization
	void Start () {
		InvokeRepeating ("MoveRat", 0, change_direction_rate);
		StartCoroutine(MouseSquee());
	}
	
	// Update is called once per frame
	void Update () {
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

	private int MoveRat(){


		if (panic == false) {

			bool go_go = false;

			int greate_random_vector = 0;

			while(go_go==false){
				greate_random_vector = Random.Range (0, 4);
				go_go = is_move_possible(greate_random_vector);
			}


			//			if(fence_direction.x==1 && greate_random_vector == 3){
			//				greate_random_vector = 2;
			//			}
			//			if(fence_direction.x==-1 && greate_random_vector == 2){
			//				greate_random_vector = 3;
			//			}
			//			if(fence_direction.y==1 && greate_random_vector == 0){
			//				greate_random_vector = 1;
			//			}
			//			if(fence_direction.y==1 && greate_random_vector == 1){
			//				greate_random_vector = 0;
			//			}


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

		} 

		if(panic==true){

			//			bool go_go = false;
			//			
			//			int greate_random_vector = 0;
			//			
			//			while(go_go==false){
			//				greate_random_vector = Random.Range (0, 4);
			//				go_go = is_move_possible(greate_random_vector);
			//			}

			CheckFoxPosition();

			int greate_random_vector = Random.Range (0, 2);
			int check_vector = 0;

			if(greate_random_vector == 0){
				if(from_fox.y==1){
					check_vector = 1;
				}
				if(from_fox.y==-1){
					check_vector = 0;
				}
				if(!is_move_possible(check_vector)){
					greate_random_vector = 1;
				}
			} else if(greate_random_vector == 1){
				if(from_fox.x==1){
					check_vector = 3;
				}
				if(from_fox.x==-1){
					check_vector = 2;
				}
				if(!is_move_possible(check_vector)){
					greate_random_vector = 0;
				}
			}

			//			if(!is_move_possible(check_vector)){
			//				pass_near_fox();
			//			} else {

			if(greate_random_vector == 1){
				movement.x = from_fox.x;
				movement.y = 0;
			}
			if(greate_random_vector == 0){
				movement.y = from_fox.y;
				movement.x = 0;
			}
			//			}

		}


		last_movement = current_movement;
		current_movement = movement;

		return 0;
	}

	void OnTriggerEnter2D(Collider2D collider){
		player fox = collider.gameObject.GetComponent<player>();
		if (fox != null) {
			if(collider.isTrigger){
				panic = true;
				change_direction_rate = 0.05f;
				speed = new Vector2(3.5f,3.5f);
				MoveRat();
				//print ("FOX IS NEAR!");
			}		
		}

		if (collider.gameObject.CompareTag ("FENCE")) {
			//print ("fence here");
			if( collider.gameObject.name == "hence_right" ){
				fence_direction.x = 1;
				//print ("fence right");
			}
			if( collider.gameObject.name == "hence_left" ){
				fence_direction.x = -1;
			}
			if( collider.gameObject.name == "hence_top" ){
				fence_direction.y = 1;
			}
			if( collider.gameObject.name == "hence_bottom" ){
				fence_direction.y = -1;
			}
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		player fox = collider.gameObject.GetComponent<player>();
		if (fox != null) {
			if(collider.isTrigger){
				panic = false;
				change_direction_rate = 0.3f;
				speed = new Vector2(1.5f,1.5f);
				//print ("FOX IS GONE!");
			}		
		}

		if (collider.gameObject.CompareTag ("FENCE")) {

			fence_direction = new Vector2(0,0);
		}

	}

	void CheckFoxPosition(){
		GameObject fox = GameObject.Find ("Player");
		Vector2 fox_pos = new Vector2(0f,0f);
		fox_pos.x = fox.gameObject.GetComponent<Rigidbody2D>().transform.position.x;
		fox_pos.y = fox.gameObject.GetComponent<Rigidbody2D>().transform.position.y;

		Vector2 rat_pos = new Vector2(
			gameObject.transform.position.x,
			gameObject.transform.position.y
		);


		if(rat_pos.x > fox_pos.x){
			from_fox.x = 1;
			//message1 = "FOX ON THE LEFT!";
		} else {
			from_fox.x = -1;
			//message1 = "FOX ON THE RIGHT!";
		}

		if(rat_pos.y > fox_pos.y){
			from_fox.y = 1; 
			//message2 = "FOX IS DOWNWAY!";
		} else {
			from_fox.y = -1;
			//message2 = "FOX IS UPWAY!";
		}

		//print (message1);
		//print (message2);
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

	IEnumerator MouseSquee(){
		float squee_wait = Random.Range (3, 7);

		yield return new WaitForSeconds(squee_wait);

		AudioSource audio = gameObject.GetComponent<AudioSource> ();
		audio.Play ();
		//GameObject fox_obj = GameObject.Find("Rabbit");

		StartCoroutine(MouseSquee());

		Light mouse_light = gameObject.GetComponent<Light> ();

		mouse_light.intensity = 1.2f;
		mouse_light.range = 8;
		StartCoroutine (MouseHighlight());
	}

	IEnumerator MouseHighlight(){
		yield return new WaitForSeconds(0.5f);

		Light mouse_light = gameObject.GetComponent<Light> ();

		mouse_light.intensity = 0f;
		mouse_light.range = 0;
	}
}

