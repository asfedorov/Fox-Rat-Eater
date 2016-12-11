using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

	public float timer = 13f;


	static int score;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer > 0) {
//			gameObject.GetComponent<TextMesh>().text = timer.ToString ("F0");

			Text timer_text = GameObject.Find("TimeLeft").GetComponent<Text>();
			timer_text.text = timer.ToString("F0");

		} else {
			//			gameObject.GetComponent<TextMesh>().text = "Time is UP! Press X to restart";
			//			if(Input.GetKeyDown("x")){
			//
			//			}

//			score = GameObject.Find("Player").GetComponent<player>().kill_count;
//
//			PlayerPrefs.SetInt("Score", score);
//
//			Application.LoadLevel("endgame");
		}
	}
}
