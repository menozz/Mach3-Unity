using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class RecordsScreen : MonoBehaviour {

	// Скрипт, управляющий выводом записей рекордов на экран


	// устанавливаем стартовую точку для отображения записей
	public int startX =20;
	public int startY = 230;

	//шаг, с которым будет спускаться вниз каждая следующая точка
	public int shagY = -30;

	//здесь хранится положение последней записи по оси У
	int y=0;

	// Список для хранения записей
	List<GoalStr> recList;

	// Префаб, который и выводит строчку рекорда
	public GameObject hScoreField;

	//Получаем текущий канвас, на котором будут отображаться записи
    GameObject canv;

	// Получаем доступ к скрипту, который загрузит записи
	LoadRecords lrec;

	// Use this for initialization
	void Start () {


		// создаем объект, который будет использоваться для создания клонов префаба строки рекорда
		GameObject hRecord;

		// Получаем текущий канвас
		canv = GameObject.Find ("Canvas1");


		// Инциализируем скрипт и получаем список рекордов
		lrec = new LoadRecords();
		lrec.initRecScreen();
		recList = lrec.getList ();

		// переменная, при помощий которой перебираются все записи по порядку из userprefs, начиная с нулевой
		int j = 0;

		// цикл,который создает префаб строки рекорда на каждую запись из списка рекордов, и выводит на экран
		while(PlayerPrefs.HasKey("game_" + j))
		{
			hRecord =(GameObject) Instantiate(hScoreField, new Vector2(startX, startY+y), Quaternion.identity); 
			hRecord.transform.SetParent(canv.transform,false);

			// заполнение текстом префаба
			hRecord.transform.GetChild(0).GetComponent<Text>().text = recList[j].Date_;
			hRecord.transform.GetChild(1).GetComponent<Text>().text = recList[j].Goals_+"";

			// если перед этим игрок установил новый рекорд, то игра выделяет строчку с новым рекрдом
			if(j==0 && GameManager.Instance.showNewScrorec ){
				hRecord.transform.GetComponent<FlashText>().flash();
				GameManager.Instance.showNewScrorec = false;
			}

			y=y+shagY;
			j++;
		}

	}
	
}
