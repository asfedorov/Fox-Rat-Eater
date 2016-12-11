using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreSet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int score = PlayerPrefs.GetInt("Player Score");

		Text score_text = gameObject.GetComponent<Text> ();
		score_text.text = score.ToString ();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
