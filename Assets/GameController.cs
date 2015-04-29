using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	static public bool isStarted;

	public Transform player;
	
	public GameObject columnPrefab;
	private ArrayList columns = new ArrayList();
	public float columnDistance;
	private float checkPoint;
	public float range;

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
		if(player.transform.position.x >= PlayerController.controlPointF)
		{
			createColumns();
			removeOffScreenColumns();
		}
	}

	void OnGUI()
	{
		GUIStyle style = new GUIStyle();
		style.fontSize = 40;
		style.normal.textColor = Color.yellow;

		GUI.Label(new Rect(10, 60, 40, 40), PlayerController.score.ToString(), style);
		if(isStarted == false)
		{
			GUI.Label(new Rect(Screen.width/2, Screen.height/2-60, 150, 50), "Click Mouse to Start!");
            if (Input.GetButtonDown("Fire1"))
			{
				isStarted = true;
				Time.timeScale = 1f;
			}
		}
	}

	//----------------------------------------------------
	// creates next 5 columns
	void createColumns()
	{
		for(int i=0; i<5; ++i)
		{
			// create columns
			Vector3 pos = new Vector3(checkPoint+columnDistance, Random.Range(-range, range), 0);
			GameObject column = (GameObject)Instantiate(columnPrefab, pos, Quaternion.identity);
			column.transform.parent = GameObject.Find("Columns").transform;
			columns.Add(column);
			checkPoint = ((GameObject)columns[columns.Count-1]).transform.position.x;

			// set a checkpoint column so that when player passes through it, new columns are created.
			if(i==3)	PlayerController.controlPointF = checkPoint;
		}
	}

	//---------------------------------------------------
	// removes columns that are off screen and behind
	void removeOffScreenColumns()
	{
		// find border x coordinate that is visible to camera 
		float spriteSize = player.transform.localScale.x/2;
		float borderX = player.transform.position.x - spriteSize - Camera.main.orthographicSize * Screen.width/Screen.height;

		// remove columns that are outside and behind the frustum
		for(int i = columns.Count-1; i >= 0; --i)
		{
			GameObject column = (GameObject)columns[i];
			if(column.transform.position.x < borderX)
			{
				columns.Remove(column);
				Destroy(column);
			}
		}

	}

	static public void restartGame()
	{
		Application.LoadLevel("game");
	}
}
