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
        ps.startSize = 0.15f;
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
        float rate = 1;
        var em = ps.emission;
        for(int i = 0;i < 10; i++)
        {
            for (float j = 0; j < 0.2; j += Time.deltaTime)
            {
                yield return 0;
            }
            rate += 1.5f;
            em.rate = rate;
        }
        while (true)
        {
            for (float i = 0; i < 2; i += Time.deltaTime)
            {
                yield return 0;
            }
            em.rate = ++rate;
        }
    }
}
