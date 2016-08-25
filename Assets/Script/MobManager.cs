using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobManager : MonoBehaviour {
	public static MobManager Instance;
    public int MobCount = 0;
    public float BaseSpawnTime = 1f;
    public float SpawnOffsetX = 10f;

	public enum MobType {
		Loser,
		Elder,
		Kid,
		BalloonKid,
		Beauty,
		Fat,
		BalloonFat
	}

    public List<GameObject> MobList;
	// Use this for initialization
	void Start () {
		if (Instance == null) Instance = this;
		if (Instance != this) Destroy(this);
		DontDestroyOnLoad(this);
		for(int i = 0 ; i < Random.Range(0, 5) ; i++) GenerateMob ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		if (MobCount < 5) GenerateMob();
	}

	void GenerateMob () {
        float TargetX = GameManager.Instance.Character.transform.position.x + SpawnOffsetX + Random.Range(-8f, 8f);
        Collider2D[] col = Physics2D.OverlapCircleAll (new Vector2 (TargetX, WorldManager.Instance.GroundY + 0.5f), 0.5f, 1 << 10);
        Debug.Log(col.Length);
        if (col.Length == 0) {
		MobCount += 1;
		GameObject temp = (GameObject)GameObject.Instantiate(MobList[Random.Range(0, MobList.Count)], new Vector2(TargetX, WorldManager.Instance.GroundY), new Quaternion());
		}
	}
}
