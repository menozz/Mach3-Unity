using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Menu : MonoBehaviour {

	GameObject panel1;


	void Awake () {
		panel1 = GameObject.Find ("PanelConfirmExit");
		panel1.SetActive (false);
		ScoresManager.Instance.activeGame = true;
	}

	void Update() {

	}

	public void onExitClick(){
		panel1.SetActive (true);
	}

	public void closeExitConfirmPanel(){
		panel1.SetActive (false);
	}

	public void confirmExit(){
		Application.Quit();
	}
}
