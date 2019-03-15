using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pipe : MonoBehaviour {
	public int[] values;
	public bool rotateAble=false,isGrey,isBlue,isWater;
	float realRotation;
	// Use this for initialization
	public GameManager gm;

	void Start () {
		gm = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.root.eulerAngles.z != realRotation&&rotateAble) {
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, realRotation), 0.3f);
		}
	}
	void OnMouseDown(){
		
			int difference = -gm.QuickSweep ((int)transform.position.x, (int)transform.position.y);
			RotatePipe ();
			difference += gm.QuickSweep ((int)transform.position.x, (int)transform.position.y);
			gm.puzzle.curValue += difference;

		if (gm.puzzle.curValue == gm.puzzle.winValue)
			gm.Win ();
		else
			gm.Kalah ();
		//gm.puzzle.curValue= gm.Sweep ();
	}

	public void RotatePipe(){
		if (rotateAble) {
			realRotation += 90;
			if (realRotation == 360) {
				realRotation = 0;
			}
			RotateValue ();
		}
	}

	void RotateValue (){
		int tempValues = values [0];
		for (int i = 0; i < values.Length-1; i++) {
			values [i] = values [i + 1];
		}
		values [3] = tempValues;
	}
}
