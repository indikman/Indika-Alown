using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshManipulator : MonoBehaviour
{
    public GameObject vertexPoint;
    public Transform touchPointRef;
    public bool isPush = true;
    public float brushThreshold = 0.1f;
    public float moveSpeed = 1f;

    MeshFilter meshFilter;
    Mesh originalMesh;

    Mesh clonedMesh;

    List<Vector3> vertices = new List<Vector3>();
    List<ModifiedVertex> vertexPoints = new List<ModifiedVertex>();
    List<int> triangles = new List<int>();

    ModifiedVertex tempVertexPoint;
    Vector3 tempPosition;
    float tempDistance;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        originalMesh = meshFilter.sharedMesh;

        clonedMesh = new Mesh();

        clonedMesh.vertices = originalMesh.vertices;
        clonedMesh.triangles = originalMesh.triangles;
        clonedMesh.normals = originalMesh.normals;
        clonedMesh.uv = originalMesh.uv;

        meshFilter.mesh = clonedMesh;

        foreach(Vector3 vertex in clonedMesh.vertices)
        {
            vertices.Add(vertex);
            tempPosition = transform.TransformPoint(vertex);


            tempVertexPoint = new ModifiedVertex();
            tempVertexPoint.position = tempPosition;
            vertexPoints.Add(tempVertexPoint);
        }

        foreach(int triangle in clonedMesh.triangles)
        {
            triangles.Add(triangle);
        }

    }

    int indexer;

    void LateUpdate()
    {
        vertices.Clear();
        indexer = 0;


        foreach (Vector3 vertex in clonedMesh.vertices)
        {
            vertices.Add(vertex);
            tempPosition = transform.TransformPoint(vertex);

            vertexPoints[indexer].position = tempPosition; 
            indexer++;
        }
        
        
        
        for (int i = 0; i < vertexPoints.Count; i++)
        {
            //calculate the distance for the touch point
            vertexPoints[i].distanceToBursh = Vector3.Distance(vertexPoints[i].position, touchPointRef.position);

            tempPosition = vertexPoints[i].position;
            //Debug.Log(i + " - " + vertexPoints[i].distanceToBursh);

            
            if(vertexPoints[i].distanceToBursh < brushThreshold)
            {
                if (isPush)
                {
                    tempPosition = Vector3.MoveTowards(tempPosition, transform.TransformPoint(touchPointRef.position), Time.deltaTime * moveSpeed * -1);
                }
                else
                {
                    //Debug.Log("Pulling");
                    //smooth move the vertex towards the point
                    tempPosition = Vector3.MoveTowards(tempPosition, transform.TransformPoint(touchPointRef.position), Time.deltaTime * moveSpeed);
                }
                vertexPoints[i].position = tempPosition;
                vertices[i] = vertexPoints[i].position;
            }
            
             
        }


        clonedMesh.vertices = vertices.ToArray();
        clonedMesh.RecalculateNormals();

        meshFilter.mesh = clonedMesh;
    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < vertexPoints.Count; i++)
        {

          //  Gizmos.DrawSphere(vertexPoints[i].position, 0.01f);
        }

        
    }

}
