using UnityEngine;
using System.Collections;

public class LoadCSV : MonoBehaviour {

	// Файл, который запускается при запуске игры, чтобы заполнить список рекордов из файла, если список рекордов пуст

	// Use this for initialization
	void Start () {
		LoadRecords lrec = new LoadRecords ();
		lrec.initRecordList ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
