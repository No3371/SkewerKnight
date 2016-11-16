using UnityEngine;
using System.Collections;

public class BloodParticle : MonoBehaviour
{
    // Use this for initialization
    ParticleSystem ps;
    Quaternion iniRot;
    bool check;
    void Awake()
    {
        iniRot = transform.rotation;
        ps = GetComponent<ParticleSystem>();
        check = false;
    }
    void Start()
    {
        var em = ps.emission;
        em.rate = 1;
        var sh = ps.shape;
        sh.angle = 50;
        ps.startLifetime = 0.28f;
        ps.startSize = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled == true && !check)
        {
            check = !check;
            transform.rotation = iniRot;
            StartCoroutine(PS());
        }
    }

    IEnumerator PS()
    {
        int rate = 1;
        var em = ps.emission;
        while (true)
        {
            for (float i = 0; i < 1.5; i += Time.deltaTime)
            {
                yield return 0;
            }
            em.rate = ++rate;
        }
    }
}
