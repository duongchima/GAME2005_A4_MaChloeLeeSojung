using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class CollisionManager : MonoBehaviour
{
    public CubeBehaviour[] actors;
    public SphereBehaviour[] bullet_actor;

    // Start is called before the first frame update
    void Start()
    {
        actors = FindObjectsOfType<CubeBehaviour>();
        bullet_actor = FindObjectsOfType<SphereBehaviour>();
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
        for (int i = 0; i < bullet_actor.Length; i++)
        {
            for (int j = 0; j < actors.Length; j++)
            {
                CheckAABBSphere(bullet_actor[i], actors[j]);
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

    public static void CheckAABBSphere(SphereBehaviour a, CubeBehaviour b)
    {
        var x = Math.Max(b.min.x, Math.Min(a.transform.position.x, b.max.x));
        var y = Math.Max(b.min.y, Math.Min(a.transform.position.y, b.max.y));
        var z = Math.Max(b.min.z, Math.Min(a.transform.position.z, b.max.z));
        var distance = Math.Sqrt((x - a.transform.position.x) * (x - a.transform.position.x) +
                                 (y - a.transform.position.y) * (y - a.transform.position.y) +
                                 (z - a.transform.position.z) * (z - a.transform.position.z));
        Vector3 norm;
        norm.x = x;
        norm.y = y;
        norm.z = z;
        norm.Normalize();
        if (distance < a.size.x / 2)
        {
            if (!a.contacts.Contains(b))
            {
                a.contacts.Add(b);
                a.isColliding = true;
                a.newVelocity = a.velocity - Vector3.Scale(((0.5f * a.mass * Vector3.Scale(a.velocity, a.velocity)) / a.mass), norm);

                Vector3 newBulletPosition = a.transform.position + (a.newVelocity * Time.deltaTime);

                a.transform.position = newBulletPosition;
            }
            else
            {
                a.contacts.Remove(b);
                a.isColliding = false;
            }
            if (!b.bulletContact.Contains(a))
            {
                b.bulletContact.Add(a);
                b.isColliding = true;
                b.newVelocity = b.velocity - Vector3.Scale(((0.5f * b.mass * Vector3.Scale(b.velocity, b.velocity)) / b.mass), norm);
                Vector3 newCubePosition = b.transform.position + (b.newVelocity * Time.deltaTime);
                b.transform.position = newCubePosition;
            }
            else
            {
                b.bulletContact.Remove(a);
                b.isColliding = false;
            }
        }

    }
}