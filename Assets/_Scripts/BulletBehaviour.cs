using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletBehaviour : MonoBehaviour
{
    public float speed;
    public Vector3 size;
    public Vector3 direction;
    public float range;
    public Vector3 velocity;
    public Vector3 acceleration;
    public Vector3 center;
    public float mass = 50.0f;
    public float radius;
    public bool isColliding;
    public Vector3 newVelocity;
    private MeshFilter meshFilter;
    private Bounds bounds;
    public List<BulletBehaviour> contacts;
    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();

        bounds = meshFilter.mesh.bounds;
        size = bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        velocity.x = 1.0f;
        velocity.y = 1.0f;
        velocity.z = 1.0f;
        radius = size.x * 0.5f;
        acceleration.x = 0.0f;
        acceleration.y = 9.8f * mass;
        acceleration.z = 9.8f * mass;
        //velocity = mult(velocity, acceleration) * Time.deltaTime;
        //transform.position = transform.position + velocity * Time.deltaTime;
        _Move();
        if (velocity.x == 0.0f && velocity.y == 0.0f && velocity.z == 0.0f)
        {
            Destroy(gameObject);
        }

    }

    private void _Move()
    {
        velocity = Vector3.Scale(velocity, acceleration) * Time.deltaTime;
        transform.position += Vector3.Scale(direction,velocity) * Time.deltaTime;
    }

    private void _CheckBounds()
    {
       
    }
}
