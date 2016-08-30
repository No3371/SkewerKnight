using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobManager : MonoBehaviour {
	public static MobManager Instance;
    public int MobCount = 0;
    public float BaseSpawnTime = 1f;
    public float SpawnOffsetX = 5f;
    public float SpawnX;

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
        MobCount = 0;
		if (Instance == null) Instance = this;
		if (Instance != this) Destroy(this);
		DontDestroyOnLoad(this);
        SpawnX = SpawnOffsetX + GameManager.Instance.Character.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {

    }

	void FixedUpdate () {
        if (MobCount < 10)
        {
            GenerateMob();
        }
	}

	void GenerateMob () {
        Collider2D[] col = Physics2D.OverlapCircleAll (new Vector2 (SpawnX, WorldManager.Instance.GroundY + 0.5f), 0.5f, 1 << 10);
        if (col.Length == 0) {
		MobCount += 1;
		GameObject temp = (GameObject)GameObject.Instantiate(MobList[Random.Range(0, MobList.Count)], new Vector2(SpawnX, WorldManager.Instance.GroundY), new Quaternion());
		}
        SpawnX += Random.Range(2f, 7f);

    }
}
