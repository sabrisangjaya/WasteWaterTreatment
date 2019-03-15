using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject wincanvas;
	[System.Serializable]
	public class Puzzle
	{
		public int winValue;
		public int curValue;
		public int width;
		public int height;
		public Pipe[,] pieces;

	}


	public Puzzle puzzle;


	// Use this for initialization
	void Start () {
	

		Vector2 dimensions = CheckDimensions ();

		puzzle.width = (int)dimensions.x;
		puzzle.height = (int)dimensions.y;

		puzzle.pieces = new Pipe[puzzle.width, puzzle.height];

		foreach (var piece in GameObject.FindGameObjectsWithTag("Piece")) {

			puzzle.pieces [(int)Mathf.Round(piece.transform.position.x), (int)Mathf.Round(piece.transform.position.y)] = piece.GetComponent<Pipe> ();

		}

		foreach (var item in puzzle.pieces) {
			
			Debug.Log(item.gameObject.name);
		}
		puzzle.winValue = GetWinValue ();
		Shuffle ();
		puzzle.curValue=Sweep ();
		while (puzzle.curValue != 1) {
			Shuffle ();
			puzzle.curValue=Sweep ();
		}

	}

	public int Sweep()
	{
		int value = 0;

		for (int h = 0; h < puzzle.height; h++) {
			for (int w = 0; w < puzzle.width; w++) {

				//compares top
				if(h!=puzzle.height-1)
					if (puzzle.pieces [w, h].values [0] == 1 && puzzle.pieces [w, h + 1].values [2] == 1)
						value++;
				//compare right
				if(w!=puzzle.width-1)
					if (puzzle.pieces [w, h].values [1] == 1 && puzzle.pieces [w + 1, h].values [3] == 1)
						value++;
			}
		}

		return value;

	}

	public int QuickSweep(int w,int h)
	{
		int value = 0;

		//compares top
		if(h!=puzzle.height-1)
			if (puzzle.pieces [w, h].values [0] == 1 && puzzle.pieces [w, h + 1].values [2] == 1)
				value++;
		//compare right
		if(w!=puzzle.width-1)
			if (puzzle.pieces [w, h].values [1] == 1 && puzzle.pieces [w + 1, h].values [3] == 1)
				value++;
		//compare left
		if (w != 0)
			if (puzzle.pieces [w, h].values [3] == 1 && puzzle.pieces [w - 1, h].values [1] == 1)
				value++;

		//compare bottom
		if (h != 0)
			if (puzzle.pieces [w, h].values [2] == 1 && puzzle.pieces [w, h-1].values [0] == 1)
				value++;


		return value;

	}

	int GetWinValue()
	{

		int winValue = 0;
		foreach (var piece in puzzle.pieces) {
			foreach (var j in piece.values) {
				winValue += j;
			}
		}
		winValue /= 2;
		return winValue;
	}

	public void Win(){
		Debug.Log ("Win Menang");
		wincanvas.SetActive (true);
		foreach (var item in puzzle.pieces) {
			
			if (item.isBlue)
				item.gameObject.GetComponent<SpriteRenderer> ().color = Color.blue;
			else if(item.isGrey)
				item.gameObject.GetComponent<SpriteRenderer> ().color = Color.grey;
			else if(item.isWater)
				item.gameObject.GetComponent<SpriteRenderer> ().color = Color.cyan;
		}

	}
	public void Kalah(){
		Debug.Log ("kalah");
		wincanvas.SetActive (false);
		//wincanvas.SetActive (true);
		foreach (var item in puzzle.pieces) {
				item.gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
		}

	}

	void Shuffle()
	{
		foreach (var piece in puzzle.pieces) {
			int k = Random.Range (0, 4);
			for (int i = 0; i < k; i++) {
				if(piece.rotateAble)
					piece.RotatePipe ();
			}
				
		}
	}


	Vector2 CheckDimensions()
	{
		Vector2 aux = Vector2.zero;

		GameObject[] pieces = GameObject.FindGameObjectsWithTag ("Piece");

		foreach (var p in pieces) {
			if (p.transform.position.x > aux.x)
				aux.x = p.transform.position.x;

			if (p.transform.position.y > aux.y)
				aux.y= p.transform.position.y;
		}

		aux.x++;
		aux.y++;

		return aux;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
