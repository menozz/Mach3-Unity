using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlashText : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void flash(){
		gameObject.transform.GetChild (0).GetComponent<Text> ().color = Color.yellow;
		gameObject.transform.GetChild (1).GetComponent<Text> ().color = Color.yellow;
	}
}
