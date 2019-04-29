using System.Collections.Generic;
using UnityEngine;

public class InstancedDecalController : MonoBehaviour
{
    // Shared instance that other objects can use call decalcontroller functions
    public static InstancedDecalController SharedInstance;

    //Offset for decal placement
    public float offset = 0.1f;

    // The decal mesh and material
    [SerializeField]
    Mesh decalMesh;
    [SerializeField]
    Material decalMaterial;

    [SerializeField]
    [Tooltip("Maximum number of decals before they will start being reused")]
    private int maxNumberOfDecals = 10;

    // Queue of matrixes for determining decal transforms
    private Queue<Matrix4x4> matrixQueue;

    private void Awake()
    {
        InitializeDecals();
        SharedInstance = this;
    }

    // Initalize our Queue 
    private void InitializeDecals()
    {
        matrixQueue = new Queue<Matrix4x4>();

        for (int i = 0; i < maxNumberOfDecals; i++)
        {
            InstantiateDecal();
        }
    }

    // Fill Queue with matrixes at level origin
    private void InstantiateDecal()
    {
        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetTRS(Vector3.zero, Quaternion.Euler(Vector3.zero), Vector3.one);
        matrixQueue.Enqueue(matrix);
    }

    // Spawn decals or rather change matrix transform
    public void SpawnDecal(Vector3 rayDirection, RaycastHit hit)
    {
        // Get oldest available matrix
        Matrix4x4 matrix = GetNextAvailableMatrix();

        if (matrix != null)
        {
            //Offset spawnpoint in order to avoid z-fighting
            Vector3 offsetPoint = hit.point - rayDirection.normalized * offset;

            // Set matrix transform
            matrix.SetTRS(offsetPoint, Quaternion.FromToRotation(-Vector3.forward, hit.normal), Vector3.one);

            // Place in Queue
            matrixQueue.Enqueue(matrix);
        }
    }

    //Function to fetch oldest matrix to use in drawing
    private Matrix4x4 GetNextAvailableMatrix ()
    {
        return matrixQueue.Dequeue();
    }

    private void Update()
    {
        // Draw decals using DrawMeshInstanced based on our matrix queue
        Graphics.DrawMeshInstanced(decalMesh, 0, decalMaterial, matrixQueue.ToArray());

        if (matrixQueue.Count < maxNumberOfDecals)
            InstantiateDecal();

        else if (ShoudlRemoveDecal())
                 RemoveExtraDecal();
    }

#if UNITY_EDITOR

    private bool ShoudlRemoveDecal()
    {
        return matrixQueue.Count > maxNumberOfDecals;
    }

    private void RemoveExtraDecal()
    {
         matrixQueue.Dequeue();      
    }

#endif
}