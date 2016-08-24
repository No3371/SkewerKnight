using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldManager : MonoBehaviour {

    public static WorldManager Instance;

    public GameObject World;

    public GameObject Prefab_BGTile;
    public GameObject Prefab_GTile;
    string BGSpritePath = "Tiles/BG";
    string GSpritePath = "Tiles/G";
    public List<GameObject> Prefab_Objects;

    float LastObjectSpawnTime;
    public float SpawnPosX = 0, DespawnPosX, GroundY;
    public float TileWidth = 2.73f, TileHeight = 0.57f;

    public int SpawnBurst = 5;
    public float ObjectSpawnModifer = 1f; //唯正
    public float ObjectSpawnThreshold = 3f;
    public float SpawnThreshold = 5f;

    // Use this for initialization
    void Start () {
        if (Instance == null) Instance = this;
        if (Instance != this) Destroy(this);
        DontDestroyOnLoad(this);
        
        BGList = GList = new List<Sprite>();
        LoadResources();
        World = this.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(GameManager.Instance.MainCamera.transform.position.x - SpawnPosX) < SpawnThreshold) SpawnTile();
	}

    List<Sprite> BGList, GList;

    void LoadResources()
    {
        BGList.AddRange(Resources.LoadAll<Sprite>(BGSpritePath));
        GList.AddRange(Resources.LoadAll<Sprite>(GSpritePath));
    }


    void SpawnTile()
    {
        Debug.Log("Spawning tiles.");
        for (int i = 0; i < SpawnBurst; i++)
        {
            GameObject temp = (GameObject)GameObject.Instantiate(Prefab_BGTile, new Vector2(SpawnPosX, Prefab_BGTile.transform.position.y), new Quaternion());
            temp.GetComponent<SpriteRenderer>().sprite = BGList[Random.Range(0, BGList.Count)];
            temp.transform.SetParent(World.transform);
            temp = (GameObject)GameObject.Instantiate(Prefab_GTile, new Vector2(SpawnPosX, Prefab_GTile.transform.position.y), new Quaternion());
            temp.GetComponent<SpriteRenderer>().sprite = GList[Random.Range(0, GList.Count)];
            temp.transform.SetParent(World.transform);

            SpawnPosX += TileWidth;
        }
    }

    void SpawnObject(int type)
    {
        if(Time.time - LastObjectSpawnTime > ObjectSpawnThreshold)
        {
            if(Random.Range(0f, 100f) < Mathf.Pow(Mathf.Abs(Time.time - LastObjectSpawnTime), 2) * ObjectSpawnModifer)
            {
                int tempID = Random.Range(0, Prefab_Objects.Count);
                GameObject temp = GameObject.Instantiate(Prefab_Objects[tempID]);
                temp.transform.position = new Vector2(SpawnPosX, Prefab_Objects[tempID].transform.position.y);
            }
        }
    }
}
