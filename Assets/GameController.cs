using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	static public bool isStarted;

	public Transform player;
	
	public GameObject columnPrefab;
	private ArrayList columns = new ArrayList();
	public float columnDistance;
	private float checkPoint;

	// Use this for initialization
	void Start () 
	{
		Time.timeScale = 0f;
		isStarted = false;
		checkPoint = player.transform.position.x + 5;
		createColumns();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnGUI()
	{
		GUI.Label(new Rect(10, 10, 40, 40), PlayerController.score.ToString());
		if(isStarted == false)
		{
			GUI.Label(new Rect(Screen.width/2, Screen.height/2-60, 150, 50), "Click Mouse to Start!");
			if(Input.anyKeyDown)
			{
				isStarted = true;
				Time.timeScale = 1f;
			}
		}
	}

	void createColumns()
	{
		for(int i=0; i<5; ++i)
		{
			Vector3 pos = new Vector3(checkPoint+columnDistance, Random.Range(-2.8f, 2.8f), 0);
			GameObject column = (GameObject)Instantiate(columnPrefab, pos, Quaternion.identity);
			column.transform.parent = GameObject.Find("Columns").transform;
			columns.Add(column);
			checkPoint = ((GameObject)columns[columns.Count-1]).transform.position.x;
		}
	}

	static public void restartGame()
	{
		Application.LoadLevel("game");
	}
}
