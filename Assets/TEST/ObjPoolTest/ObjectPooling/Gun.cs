using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    [SerializeField]
    private Transform spawnPoint;
    //public GameObject bullet;
    //[SerializeField]
    //private int bulletAmount = 11;

    //List<GameObject> bullets;

    private void Start()
    {
        //bullets = new List<GameObject>(bulletAmount);
        //for (int i = 0; i < bulletAmount; i++)
        //{
        //    GameObject obj = Instantiate(bullet);
        //    obj.SetActive(false);
        //    bullets.Add(obj);
        //}
    }

    public void Shoot()
    {
        Ray ray = new Ray(spawnPoint.position, spawnPoint.forward);
        RaycastHit hit;

        float shotDistance = 20.0f;

        if (Physics.Raycast(ray, out hit))
        {
            shotDistance = hit.distance;
        }

        Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red, 1.0f);
        Quaternion rot = new Quaternion();
        rot.eulerAngles = ray.direction;

        ////Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);

        //for (int i = 0; i < bullets.Count; i++)
        //{
        //    if (!bullets[i].activeInHierarchy)
        //    {
        //        bullets[i].transform.position = spawnPoint.position;
        //        bullets[i].transform.rotation = spawnPoint.rotation;
        //        bullets[i].SetActive(true);
        //        break;
        //    }
        //}

        GameObject obj = ObjectPoolerScript.current.GetPooledObject(); //get bullet or null
        
        if (obj == null)
        {
            return;
        }

        obj.transform.position = spawnPoint.position;
        obj.transform.rotation = spawnPoint.rotation;
        obj.SetActive(true);
    }
}
