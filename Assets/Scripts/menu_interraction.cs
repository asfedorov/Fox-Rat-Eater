using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class menu_interraction : MonoBehaviour {

	public Text text;

	void OnMouseEnter () {

		text = gameObject.GetComponentInChildren<Text> ();
		text.color = new Color (255/255f, 79/255f, 0/255f, 1f);

		if (name == "NewGameButton") {
			GameObject fox_obj = GameObject.Find("Fox");

			Vector3 fox_scale = fox_obj.transform.localScale;
			fox_obj.transform.localScale = new Vector3(fox_scale.x*-1, fox_scale.y, fox_scale.z);
		}
		if (name == "AboutNCreditsButton") {
			GameObject fox_obj = GameObject.Find("Rat");

			Vector3 fox_scale = fox_obj.transform.localScale;
			fox_obj.transform.localScale = new Vector3(fox_scale.x*-1, fox_scale.y, fox_scale.z);
		}
		if (name == "HighscoresButton") {
			GameObject fox_obj = GameObject.Find("Rabbit");

			fox_obj.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load ("rabbit_jump", typeof(Sprite));;

			StartCoroutine(GetRabbitBack());
			//Vector3 fox_scale = fox_obj.transform.localScale;
			//fox_obj.transform.localScale = new Vector3(fox_scale.x*-1, fox_scale.y, fox_scale.z);
		}


	}

	IEnumerator GetRabbitBack(){
		yield return new WaitForSeconds(0.2f);
		GameObject fox_obj = GameObject.Find("Rabbit");

		fox_obj.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load ("rabbit", typeof(Sprite));;
	}

	void OnMouseExit () {
		// transform.localScale = new Vector3(1, 1, 1);
		text = gameObject.GetComponentInChildren<Text> ();
		text.color = new Color (1f, 1f, 1f, 1);
	}

	void OnMouseDown(){
		if (name == "NewGameButton") {
			Application.LoadLevel("pregame");
		}
		if (name == "AboutNCreditsButton") {
			Application.LoadLevel("about_n_credits");
		}
		if (name == "HighscoresButton") {
			Application.LoadLevel("highscores");
		}	
		if (name == "ExitButton") {
			Application.Quit ();
		}	
		if (name == "BackButton") {
			Application.LoadLevel("main");
		}	
		if (name == "StartButton") {
			Application.LoadLevel("gameplay");
		}	
	}
}
