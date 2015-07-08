using UnityEngine;
using System.Collections;

public class firsStartGame : MonoBehaviour {

	// стартовые координаты для случайного выбора обучающего шарика из сетки
	public float startX = 0.5f;
	public float startY = -2.2f;

	// таймер чтобы отложить запуск обучающего шарика после создания игрового поля
	public float firstDelay = 1.9f;

	// Use this for initialization
	void Start () {
		StartCoroutine(selectRndCircle());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// метод который выбирает случайный шарик
	IEnumerator selectRndCircle() {

		// устанавливаем случайный координаты по которым игра выбирает шарик и записывем их в глобальные переменные
		int rndX = UnityEngine.Random.Range(0, 5);
		int rndY = UnityEngine.Random.Range(0, 5);	
		GameManager.Instance.fX = startX + rndX;
		GameManager.Instance.fY = startY + rndY;
		yield return new WaitForSeconds(firstDelay);

		// получаем шарик по выбранным координатам и заставляем его мигать
		RaycastHit2D hit = Physics2D.Raycast( new Vector2(startX+rndX,startY+rndY), Vector2.zero );
		hit.transform.GetComponent<circle_controller> ().firsStartOn ();
	}


}
