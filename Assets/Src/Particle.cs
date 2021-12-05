using System;
using UnityEngine;

public class Particle : MonoBehaviour
{
    private Vector3 velocity;
    private Vector3 gravityAcceleration = new Vector3(0, -9.8f, 0);

    private float mass = 1;

    private Mesh mesh;

    private Vector3 resultantForce;

    private ulong frameCount;

    void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void Start()
    {
    }

    protected virtual void FixedUpdate()
    {
        #if UNITY_EDITOR_WIN
        Grapher.Log(transform.position, "pos");
        Grapher.Log(velocity, "vel");
        #endif

        Vector3 resultantAcceleration = (resultantForce / mass) + gravityAcceleration;

        // Change in position calculated through trapezoid square (is mathematically equivalent the previous approach)
        transform.position += (velocity + (velocity + resultantAcceleration * Time.fixedDeltaTime)) * Time.fixedDeltaTime / 2;

        // Change in position calculated through average velocity
        /*Vector3 instantVel = velocity + acceleration * Time.fixedDeltaTime;
        Vector3 avgVel = (velocity + instantVel) / 2;
        position += avgVel * Time.fixedDeltaTime;*/


        // Change in position calculated geometrically (see my notes)
        /*Vector3 capSquare = acceleration * Time.fixedDeltaTime * Time.fixedDeltaTime / 2;
        Vector3 restSquare = velocity * Time.fixedDeltaTime;
        position += capSquare + restSquare;*/

        velocity += resultantAcceleration * Time.fixedDeltaTime;
        
        resultantForce = Vector3.zero;
        
        Debug.Log($"frames: {frameCount}\nvel: {velocity.ToString("F5")}\npos: {transform.position.ToString("F5")}");
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

    public void ApplyForce(Vector3 force)
    {
        resultantForce += force;
    }

    public Vector3 Velocity
    {
        get => velocity;
        set => velocity = value;
    }
}