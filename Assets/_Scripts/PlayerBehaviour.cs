using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bullet;
    public int fireRate;
    ObjectPooler objectPooler;

    public BulletManager bulletManager;

    //private void start()
    //{
    //    objectPooler = ObjectPooler.Instance;
    //}

    // Update is called once per frame
    void Update()
    {
        _Fire();
    }

    private void _Fire()
    {
        if (Input.GetAxisRaw("Fire1") > 0.0f)
        {
            // delays firing
            if (Time.frameCount % fireRate == 0)
            {
                GameObject bullet = ObjectPooler.Instance.GetObject();
                if (bullet != null)
                {
                    bullet.transform.position = bulletSpawn.position;
                    bullet.transform.rotation = Quaternion.identity;
                    bullet.transform.SetParent(bulletManager.gameObject.transform);
                    bullet.GetComponent<SphereBehaviour>().direction = bulletSpawn.forward;
                    bullet.SetActive(true);
                }
            }

        }
    }
}
