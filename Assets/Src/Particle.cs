using System;
using UnityEngine;
using MyMath;

public class Particle : MonoBehaviour
{
    private Vector3 velocity;
    private Vector3 acceleration = new Vector3(0, -9.8f, 0);

    private float mass = 1;

    private Mesh mesh;

    void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void Start()
    {
    }

    private void FixedUpdate()
    {
        /*Debug.Log($"elapsed: {Time.fixedDeltaTime}");

        Debug.Log($"vel: {-velocity}");
        Debug.Log($"pos: {-transform.position}");*/

        Grapher.Log(-transform.position.y, "pos");
        Grapher.Log(-velocity.y, "vel");

        // Change in position calculated through trapezoid square (mathematically is equivalent the previous approach)
        transform.position += (velocity + (velocity + acceleration * Time.fixedDeltaTime)) * Time.fixedDeltaTime / 2;

        // Change in position calculated through average velocity
        /*Vector3 instantVel = velocity + acceleration * Time.fixedDeltaTime;
        Vector3 avgVel = (velocity + instantVel) / 2;
        position += avgVel * Time.fixedDeltaTime;*/


        // Change in position calculated geometrically (see my notes)
        /*Vector3 capSquare = acceleration * Time.fixedDeltaTime * Time.fixedDeltaTime / 2;
        Vector3 restSquare = velocity * Time.fixedDeltaTime;
        position += capSquare + restSquare;*/

        velocity += acceleration * Time.fixedDeltaTime;
    }

    void Update()
    {
        if (transform.position.y < -10)
            Destroy(gameObject);

        mesh.Clear();

        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(-0.1f, -0.1f);
        vertices[1] = new Vector3(-0.1f, 0.1f);
        vertices[2] = new Vector3(0.1f, 0.1f);
        vertices[3] = new Vector3(0.1f, -0.1f);

        mesh.vertices = vertices;
        mesh.triangles = new[]
        {
            0, 1, 3,
            1, 2, 3
        };
        mesh.normals = new[]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
        };
    }
}