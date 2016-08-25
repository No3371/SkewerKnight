using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobManager : MonoBehaviour {
	public static MobManager Instance;
	public static int MobCount;
	public float BasicRespaw = 2f;
	public float RanRange = 2f;
	public enum MobType {
		Loser,
		Elder,
		Kid,
		BalloonKid,
		Beauty,
		Fat,
		BalloonFat
	}
	// Use this for initialization
	void Start () {
		if (Instance == null) Instance = this;
		if (Instance != this) Destroy(this);
		DontDestroyOnLoad(this);
		for(int i = 0 ; i<5 ;i++) GenerateMob ();
		MobCount = 5;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		if (MobCount < 5) GenerateMob ();
	}

	void GenerateMob () {
		float SpawnFar = GameManager.Instance.Character.transform.position.x + BasicRespaw + Random.Range (0f, RanRange);
		Collider2D[] col = Physics2D.OverlapCircleAll (new Vector2 (SpawnFar, WorldManager.Instance.GroundY + 0.5f), 0.5f, 1 << 10);
		if (col == null) {
			MobCount += 1;
			GameObject temp;
			int ran = (int)Mathf.Floor(Random.Range (0f, 7f));
			Debug.Log (ran);
			switch (ran)
			{
			case 0:
				temp = (GameObject)GameObject.Instantiate(Resources.Load("Prefab_Mob0") , new Vector2(SpawnFar, WorldManager.Instance.GroundY), new Quaternion());
				break;
			case 1:
				temp = (GameObject)GameObject.Instantiate(Resources.Load("Prefab_Mob1") , new Vector2(SpawnFar, WorldManager.Instance.GroundY), new Quaternion());
				break;
			case 2:
				temp = (GameObject)GameObject.Instantiate(Resources.Load("Prefab_Mob2"), new Vector2(SpawnFar, WorldManager.Instance.GroundY), new Quaternion());
				break;
			case 3:
				temp = (GameObject)GameObject.Instantiate(Resources.Load("Prefab_Mob3"), new Vector2(SpawnFar, WorldManager.Instance.GroundY), new Quaternion());
				break;
			case 4:
				temp = (GameObject)GameObject.Instantiate(Resources.Load("Prefab_Mob4"), new Vector2(SpawnFar, WorldManager.Instance.GroundY), new Quaternion());
				break;
			case 5:
				temp = (GameObject)GameObject.Instantiate(Resources.Load("Prefab_Mob5"), new Vector2(SpawnFar, WorldManager.Instance.GroundY), new Quaternion());
				break;
			case 6:
				temp = (GameObject)GameObject.Instantiate(Resources.Load("Prefab_Mob6"), new Vector2(SpawnFar, WorldManager.Instance.GroundY), new Quaternion());
				break;
			default:
				break;
			}
		}
	}
}
