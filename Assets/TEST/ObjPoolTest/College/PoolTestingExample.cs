using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolTestingExample : MonoBehaviour
{
    [SerializeField]
    private Projectile prefab;

    public float delta = 1.2f;
    private float nextSpawn = 1f;

    private void OnEnable()
    {
        //var projectile = prefab.Get<Projectile>();
        //Debug.Log("Got " + projectile.name);

        //projectile = prefab.Get<Projectile>(false);
        //Debug.Log("Got " + projectile.name + " as disabled");

        //projectile = prefab.Get<Projectile>(transform);
        //Debug.Log("Got " + projectile.name + " as child");
        //Debug.Log(projectile.transform.parent.name);

        //projectile = prefab.Get<Projectile>(transform, true);
        //Debug.Log("Got " + projectile.name + " as child and reset");

        //projectile = prefab.Get<Projectile>(transform, Vector3.up, Quaternion.Euler(Vector3.up));
        //Debug.Log("Got " + projectile.name + " as child offset and rotated");
    }

    private void Update()
    {
        if (nextSpawn <= Time.time)
        {
            Projectile projectile;
            //projectile = prefab.Get<Projectile>();
            //Debug.Log("Got " + projectile.name);

            //projectile = prefab.Get<Projectile>(false);
            //Debug.Log("Got " + projectile.name + " as disabled");

            //projectile = prefab.Get<Projectile>(transform);
            //Debug.Log("Got " + projectile.name + " as child");
            //Debug.Log(projectile.transform.parent.name);

            //projectile = prefab.Get<Projectile>(transform, true);
            //Debug.Log("Got " + projectile.name + " as child and reset");

            projectile = prefab.Get<Projectile>(transform, Vector3.up * -5, Quaternion.Euler(Vector3.up * -100));
            Debug.Log("Got " + projectile.name + " as child offset and rotated");

            nextSpawn = Time.time + delta;
        }
    }
}
