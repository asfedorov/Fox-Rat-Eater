using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Serialization;
using SimpleJSON;

public class SubmitScript : MonoBehaviour {

	private bool can_submit = true;
	private string submit_url = "http://85.143.218.168:8000/set_score/";
	private string highscore_url = "http://85.143.218.168:8000/get_scores/";
	private string secret = "my_super_secret_key";


	// Use this for initialization
	void Start () {
		StartCoroutine(get_scores() );
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		if (name == "SubmitButton") {
			if (can_submit) {
				StartCoroutine(post_score() );
			}
		}
	}

	IEnumerator post_score()
	{
		//This connects to a server side php script that will add the name and score to a MySQL DB.
		// Supply it with a string representing the players name and the players score.

		// Post the URL to the site and create a download object to get the result.

		WWWForm form = new WWWForm();
//		Hashtable data = new Hashtable();

//		Hashtable headers = form.headers;
//		headers["Content-Type"] = "application/x-www-form-urlencoded";

//		data["secret"] = secret;

//		byte[] bytes = Encoding.UTF8.GetBytes(data);

		GameObject input_field = GameObject.Find ("InputField");
		Text player_name = input_field.GetComponent<Text> ();

		int score = PlayerPrefs.GetInt("Player Score");

		form.AddField ("secret", secret);
		form.AddField ("player_name", player_name.text);
		form.AddField ("highscore", score);

		WWW www = new WWW(submit_url, form);

		yield return www; // Wait until the download is done
//
		if (www.error != null) {
			print ("There was an error posting the high score: " + www.error);
		} else {
			can_submit = false;
			StartCoroutine(get_scores() );
			Button button = gameObject.GetComponent<Button> ();
			button.interactable = false;
			button.name = "SubmitedButton";

			Text btext = button.GetComponentInChildren<Text> ();
			btext.text = "Submited";
		}
	}

	IEnumerator get_scores(){
		WWW www = new WWW (highscore_url);
		yield return www;
		if (www.error != null){
			print("There was an error posting the high score: " + www.error);
		} else {
			var N = JSON.Parse(www.text);
			int i = 0;
			while (N [i] != null) {
				string zz = (N [i] ["i"].Value).ToString();
				GameObject line = GameObject.Find ("line"+zz);
				Text[] cells = line.GetComponentsInChildren<Text> ();
				foreach (Text cell in cells) {
					if (cell.name == "player_name") {
						cell.text = N [i] ["player_name"];
					} else if (cell.name == "player_score") {
						cell.text = N [i] ["highscore"];
					}
				}

				i++;
			}

//			ArrayList scores_array = JsonUtility.FromJson<ArrayList>(www.text);
//			print (N [0]);
		}
	}
}
