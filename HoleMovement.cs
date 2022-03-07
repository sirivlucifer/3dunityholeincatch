using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HoleMovement : MonoBehaviour
{

    [Header("Hole mesh")]
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] MeshCollider meshCollider;

    [Header("Hole vertices radius")]
    [SerializeField] Vector2 moveLimits;
    [SerializeField] float radius;
    [SerializeField] Transform holeCenter;

    [Space]
    [SerializeField] float moveSpeed;


    Mesh mesh;
    List<int> holeVertices;
    List<Vector3> offsets;
    int holeVeritecesCount;

    float x, y;
    Vector3 touch, targetPos;

    void Start()
    {

        Game.isMoving = false;
        Game.isGameOver = false;


        holeVertices = new List<int>();
        offsets = new List<Vector3>();

        mesh = meshFilter.mesh;

        //find hole vertices on the mesh
        FindHoleVertices();
        
    }

    // Update is called once per frame
    void Update()
    {
        Game.isMoving = Input.GetMouseButton(0);

        if (!Game.isGameOver && Game.isMoving)
        {   
            //movehole center
            MoveHole();
            //update hole vertices
            UpdateHoleVerticesPosition();
        }
       
    }
    void MoveHole()
    {
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        touch = Vector3.Lerp
            (holeCenter.position,
            holeCenter.position + new Vector3(x, 0f, y),
            moveSpeed * Time.deltaTime
            );

        targetPos = new Vector3(

            Mathf.Clamp(touch.x, -moveLimits.x, moveLimits.x),
            touch.y,
            Mathf.Clamp(touch.z, -moveLimits.y, moveLimits.y)

            );



        holeCenter.position = targetPos;
;
    }

    void UpdateHoleVerticesPosition()
    {
        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < holeVeritecesCount; i++) 
        {
            vertices[holeVertices[i]] = holeCenter.position + offsets[i];


        }
        mesh.vertices = vertices;
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;

    }



    void FindHoleVertices()
    {
        for(int i = 0; i<mesh.vertices.Length; i++)
        {
            float distance = Vector3.Distance(holeCenter.position, mesh.vertices[i]);

            if (distance < radius)
            {
                holeVertices.Add(i);
                offsets.Add(mesh.vertices[i] - holeCenter.position);


            }
        }

        holeVeritecesCount = holeVertices.Count;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(holeCenter.position, radius);

    }
}
