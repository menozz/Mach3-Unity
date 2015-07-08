using UnityEngine;
using System.Collections;

public class GameMenu : MonoBehaviour {

	// Скрипт, который управляет меню и активностью игры, на основном игровом экране

	GameObject panel1;
	
	
	void Awake () {

		// Получаем доступ к панели на подтверждение выхода, и выключаем, чтобы она не занимала экран
		panel1 = GameObject.Find ("PanelConfirmExit");
		panel1.SetActive (false);
	}
	
	void Update() {
		
	}

	// При нажатии кнопки "назад" появляется меню с предложением подтвердить выход, приостанавливает игру
	public void onExitClick(){
		panel1.SetActive (true);
		GameManager.Instance.activeGame = false;
	}

	// При нажатии "остаться" на диалоге выхода, убирает панел и запускает дальше игру
	public void closeExitConfirmPanel(){
		panel1.SetActive (false);
		GameManager.Instance.activeGame = true;
	}

	// При нажатии "выход" в диалоге выхода, переводит игру на стартовый экран игры
	public void confirmExit(){
		Application.LoadLevel(0);
	}
}
