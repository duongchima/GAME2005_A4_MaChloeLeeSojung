using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class CollisionManager : MonoBehaviour
{
    public CubeBehaviour[] actors;
    public BulletBehaviour[] bullets;

    // Start is called before the first frame update
    void Start()
    {
        actors = FindObjectsOfType<CubeBehaviour>();
        bullets = FindObjectsOfType<BulletBehaviour>();
    }
    
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < actors.Length; i++)
        {
            for (int j = 0; j < actors.Length; j++)
            {
                if (i != j)
                {
                    CheckAABBs(actors[i], actors[j]);
                }
            }
        }
    }

    public static void CheckAABBs(CubeBehaviour a, CubeBehaviour b)
    {
        if ((a.min.x <= b.max.x && a.max.x >= b.min.x) &&
            (a.min.y <= b.max.y && a.max.y >= b.min.y) &&
            (a.min.z <= b.max.z && a.max.z >= b.min.z))
        {
            if (!a.contacts.Contains(b))
            {
                a.contacts.Add(b);
                a.isColliding = true;
            }
        }
        else
        {
            if (a.contacts.Contains(b))
            {
                a.contacts.Remove(b);
                a.isColliding = false;
            }
           
        }
    }
    public static void CheckAABBSphere(BulletBehaviour a, CubeBehaviour b)
    {
        var x = Math.Max(b.min.x, Math.Min(a.gameObject.transform.position.x, b.max.x));
        var y = Math.Max(b.min.y, Math.Min(a.gameObject.transform.position.y, b.max.y));
        var z = Math.Max(b.min.z, Math.Min(a.gameObject.transform.position.z, b.max.z));
        var distance = Math.Sqrt((x - a.gameObject.transform.position.x) * (x - a.gameObject.transform.position.x) +
                                 (y - a.gameObject.transform.position.y) * (y - a.gameObject.transform.position.y) +
                                 (z - a.gameObject.transform.position.z) * (z - a.gameObject.transform.position.z));
        Vector3 norm;
        norm.x = x;
        norm.y = y;
        norm.z = z;
        norm.Normalize();
        if(distance < a.radius)
        {
                a.isColliding = true;
                //a.newVelocity = a.velocity - b.velocity;
                //a.newVelocity = ((a.mass - b.mass) / (a.mass + b.mass)) * a.velocity + ((2 * b.mass) / (a.mass + b.mass)) * b.velocity;
                a.newVelocity = a.velocity - Vector3.Scale(((0.5f * a.mass * Vector3.Scale(a.velocity, a.velocity)) / a.mass), norm);
                b.newVelocity = b.velocity - Vector3.Scale(((0.5f * b.mass * Vector3.Scale(b.velocity, b.velocity)) / b.mass), norm);
                Vector3 newBulletPosition = a.transform.position + (a.newVelocity * Time.deltaTime);
                Vector3 newCubePosition = b.transform.position + (b.newVelocity * Time.deltaTime);
                a.transform.position = newBulletPosition;
                b.transform.position = newCubePosition;
        }
    }
}
