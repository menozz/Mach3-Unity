using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Menu : MonoBehaviour {

	// Скрипт, управляющий диалогом для выхода из игры,на стартом экране

	GameObject panel1;


	void Awake () {
		panel1 = GameObject.Find ("PanelConfirmExit");
		panel1.SetActive (false);

		// для игрового экрана - устанавливает что при запуске игра активна
		GameManager.Instance.activeGame = true;
	}

	void Update() {

	}



	// показывает диалог с предложением выйти из игры
	public void onExitClick(){
		panel1.SetActive (true);
	}


	// закрывает диалог с предложением выйти

	/// <summary>
	/// Closes the exit confirm panel.
	/// </summary>
	public void closeExitConfirmPanel(){
		panel1.SetActive (false);
	}

/// <summary>
/// Confirms the exit from game.
/// </summary>
	public void confirmExit(){
		Application.Quit();
	}
}
