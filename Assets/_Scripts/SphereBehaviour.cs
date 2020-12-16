using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class SphereBehaviour : MonoBehaviour
{
    public float speed;
    public Vector3 size;
    public Vector3 direction;
    public float range;
    public Vector3 velocity;
    public Vector3 acceleration;
    public Vector3 center;
    public float mass;
    private float radius;
    public bool isColliding;
    public Vector3 newVelocity;
    private Vector3 min;
    private Vector3 max;
    private float startingTime;
    public List<SphereBehaviour> bulletContact;
    public List<CubeBehaviour> contacts;
    // Start is called before the first frame update
    void Start()
    {
        acceleration.Set(0.0f, 0.0f, 0.0f);
        velocity.Set(0.0f, 0.0f, 0.0f);
        startingTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _InactiveBullet();

    }

    private void _Move()
    {
        //velocity = Vector3.Scale(velocity, acceleration) * Time.deltaTime;
        velocity = speed * direction + acceleration;
        transform.position += velocity * Time.deltaTime;
    }
    private void _InactiveBullet()
    {
        if (velocity.x == 0.0f && velocity.y == 0.0f && velocity.z == 0.0f)
        {
            gameObject.SetActive(false);
            startingTime = Time.time;
            speed = 10.0f;
            direction.Set(0.0f, 0.0f, 0.0f);
            acceleration.Set(0.0f, 0.0f, 0.0f);
            velocity.Set(0.0f, 0.0f, 0.0f);
        }
    }
}