using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldManager : MonoBehaviour {

    public static WorldManager Instance;

    public GameObject World;
    
    List<Sprite> GList = new List<Sprite>();
    List<Sprite> CList = new List<Sprite>();
    List<Sprite> DList = new List<Sprite>();
    List<Sprite> MList = new List<Sprite>();
    public GameObject Prefab_GTile, Prefab_Cloud, Prefab_Deco, Prefab_Mountain;
    string GSpritePath = "Tiles/G";
    string CSpritePath = "Tiles/Cloud";
    string DSpritePath = "Tiles/Deco";
    string MSpritePath = "Tiles/Mountain";
    public List<GameObject> Prefab_Objects;

    float LastObjectSpawnTime;
    public float SpawnPosX = 0, GroundY = 0;
    public float TileWidth = 2.73f, MountainTileWidth = 2.46f;

    public int SpawnBurst = 5;
    public float ObjectSpawnModifer = 0.9f; //唯正
    public float ObjectSpawnThreshold = 5f;
    public float SpawnThreshold = 10f;

    // Use this for initialization
    void Start () {
        if (Instance == null) Instance = this;
        if (Instance != this) Destroy(this);
        DontDestroyOnLoad(this);

        LoadResources();
        World = this.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(GameManager.Instance.MainCamera.transform.position.x - SpawnPosX) < SpawnThreshold) SpawnTile();
	}


    void LoadResources()
    {
        GList.AddRange(Resources.LoadAll<Sprite>(GSpritePath));
        CList.AddRange(Resources.LoadAll<Sprite>(CSpritePath));
        DList.AddRange(Resources.LoadAll<Sprite>(DSpritePath));
        MList.AddRange(Resources.LoadAll<Sprite>(MSpritePath));
    }


    void SpawnTile()
    {
        for (int i = 0; i < SpawnBurst; i++)
        {
            GameObject temp = (GameObject)GameObject.Instantiate(Prefab_GTile, new Vector2(SpawnPosX, GroundY), new Quaternion());
            temp.GetComponent<SpriteRenderer>().sprite = GList[Random.Range(0, GList.Count)];
            temp.transform.SetParent(World.transform);
            temp = (GameObject)GameObject.Instantiate(Prefab_Mountain, new Vector2(SpawnPosX, GroundY), new Quaternion());
            temp.GetComponent<SpriteRenderer>().sprite = MList[Random.Range(0, MList.Count)];
            temp.transform.SetParent(World.transform);

            SpawnPosX += TileWidth;
            for(int j = 0; j < Random.Range(0, 3); j++)
                SpawnCloud();
            for (int j = 0; j < Random.Range(0, 3); j++)
                SpawnDeco();
            SpawnObject();
        }
    }

    float LastCloudSpawnTime;
    float CloudSpawnRate = 0.4f;
    void SpawnCloud()
    {
        if (Time.time - LastCloudSpawnTime > 0.5f && Random.Range(0f, 1f) < CloudSpawnRate)
        {
            GameObject temp = (GameObject)GameObject.Instantiate(Prefab_Cloud, new Vector2(SpawnPosX + Random.Range(-4f, 4f), GroundY + Random.Range(3f, 5f)), new Quaternion());
            temp.GetComponent<SpriteRenderer>().sprite = CList[Random.Range(0, CList.Count)];
            temp.transform.SetParent(World.transform);
            LastCloudSpawnTime = Time.time;
        }
    }

    float LastDecoSpawnTime;
    float DecoSpawnRate = 1f;
    void SpawnDeco()
    {
        if (Time.time - LastDecoSpawnTime > 0.5f && Random.Range(0f, 1f) < DecoSpawnRate)
        {
            GameObject temp = (GameObject)GameObject.Instantiate(Prefab_Deco, new Vector2(SpawnPosX + Random.Range(-2f, 2f), GroundY), new Quaternion());
            temp.GetComponent<SpriteRenderer>().sprite = DList[Random.Range(0, DList.Count)];
            temp.transform.SetParent(World.transform);
            LastDecoSpawnTime = Time.time;
        }
    }
    

    void SpawnObject()
    {
        if(Time.time - LastObjectSpawnTime > ObjectSpawnThreshold)
        {
            if(Random.Range(0f, 100f) < Mathf.Pow(Mathf.Abs(Time.time - LastObjectSpawnTime), 2) * ObjectSpawnModifer)
            {
                int id = Random.Range(0, 4);
                GameObject temp;
                if(id == 3)
                {
                    for(int i = 0; i < Random.Range(1, 5); i++)
                    {
                        temp = GameObject.Instantiate(Prefab_Objects[id]);
                        temp.transform.position = new Vector2(SpawnPosX + i * 1.5f, GroundY);
                        temp.transform.SetParent(World.transform);
                    }
                }
                else
                {
                    temp = GameObject.Instantiate(Prefab_Objects[id]);
                    temp.transform.position = new Vector2(SpawnPosX, GroundY);
                    temp.transform.SetParent(World.transform);
                }
                
            }
            LastObjectSpawnTime = Time.time;
        }
    }
}
