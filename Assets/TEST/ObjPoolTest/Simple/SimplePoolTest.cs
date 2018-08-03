using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePoolTest : MonoBehaviour
{
    [SerializeField]
    private SimpleObjectPool pool;

    public float delta = 1.2f;
    public float livingTime = 2.0f;

    private float nextSpawn = 1f;

    public List<GameObject> projectiles = new List<GameObject>();
    public Dictionary<GameObject, float> projectilesTime = new Dictionary<GameObject, float>();

    private void Update()
    {
        foreach (var item in projectiles)
        {
            projectilesTime[item] -= Time.deltaTime;
        }

        if (nextSpawn <= Time.time)
        {
            if (projectiles.Count > 0 && projectilesTime[projectiles[0]] <= 0f)
            {
                pool.ReturnObject(projectiles[0]);
                projectilesTime.Remove(projectiles[0]);
                projectiles.Remove(projectiles[0]);
            }

            GameObject projectile;
            projectile = pool.GetObject();
            //pool.ReturnObject(projectile);

            projectiles.Add(projectile);

            nextSpawn = Time.time + delta;

            projectilesTime.Add(projectile, livingTime);
        }
    }
}
