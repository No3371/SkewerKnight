using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour {
	public MobManager.MobType Type;
	public bool isCaught;
	public int PostionOnSpear;
	public int Volume;
	public GameObject Spear;

	// Use this for initialization
	void Start () {
		isCaught = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (isCaught) {

		}
	}
}
