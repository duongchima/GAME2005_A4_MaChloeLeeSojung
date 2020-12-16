using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Color = UnityEngine.Color;


[System.Serializable]
public class CubeBehaviour : MonoBehaviour
{
    private Vector3 gravity;
    public Vector3 acceleration;
    public Vector3 size;
    public Vector3 max;
    public Vector3 min;
    public float speed;
    public Vector3 direction;
    public Vector3 velocity;
    public Vector3 newVelocity;
    public float mass = 5.0f;
    public float friction;
    public bool isColliding;
    public bool debug;
    public List<CubeBehaviour> contacts;
    public List<SphereBehaviour> bulletContact;
    public bool isGround;

    private MeshFilter meshFilter;
    private Bounds bounds;

    // Start is called before the first frame update
    void Start()
    {
        debug = false;
        meshFilter = GetComponent<MeshFilter>();
        acceleration.Set(0.0f, 0.0f, 0.0f);
        velocity.Set(0.0f, 0.0f, 0.0f);
        bounds = meshFilter.mesh.bounds;
        size = bounds.size;
        gravity.Set(0.0f, -9.8f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
        max = Vector3.Scale(bounds.max, transform.localScale) + transform.position;
        min = Vector3.Scale(bounds.min, transform.localScale) + transform.position;
    }

    void FixedUpdate()
    {
        // physics related calculations
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.magenta;

            Gizmos.DrawWireCube(transform.position, Vector3.Scale(new Vector3(1.0f, 1.0f, 1.0f), transform.localScale));
        }
    }
}