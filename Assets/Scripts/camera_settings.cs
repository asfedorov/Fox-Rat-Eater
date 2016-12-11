using UnityEngine;
using System.Collections;

public class camera_settings : MonoBehaviour {

	// Use this for initialization
	void Start () {
		RenderSettings.ambientLight = new Color(0.1f,0.1f,0.1f,0.9f);
		RenderSettings.fog = true;
		RenderSettings.fogColor = Color.black;
		RenderSettings.fogDensity = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
