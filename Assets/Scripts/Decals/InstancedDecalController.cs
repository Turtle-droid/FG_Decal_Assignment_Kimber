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

    private int currentIndex = 0;

    // Queue of matrixes for determining decal transforms
    private Matrix4x4[] matrixArray;    

    private void Awake()
    {
        InitializeDecals();
        SharedInstance = this;
    }

    // Initalize our Queue 
    private void InitializeDecals()
    {
        matrixArray = new Matrix4x4[maxNumberOfDecals];

        for (int i = 0; i < maxNumberOfDecals; i++)
        {
            InstantiateDecal(i);
        }
    }

    // Fill Queue with matrixes at level origin
    private void InstantiateDecal(int index)
    {
        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetTRS(Vector3.zero, Quaternion.Euler(Vector3.zero), Vector3.one);
        matrixArray[index] = matrix;
    }

    // Spawn decals or rather change matrix transform
    public void SpawnDecal(Vector3 rayDirection, RaycastHit hit)
    {
        if (matrixArray[currentIndex] != null)
        {
            //Offset spawnpoint in order to avoid z-fighting
            Vector3 offsetPoint = hit.point - rayDirection.normalized * offset;

            // Set matrix transform
            matrixArray[currentIndex].SetTRS(offsetPoint, Quaternion.FromToRotation(-Vector3.forward, hit.normal), Vector3.one);

            Debug.Log("Current Index: " + currentIndex);

            currentIndex += 1;

            if (currentIndex >= maxNumberOfDecals)
            {
                currentIndex = 0;
            }
        }
    }

    private void Update()
    {
        // Draw decals using DrawMeshInstanced based on our matrix queue
        Graphics.DrawMeshInstanced(decalMesh, 0, decalMaterial, matrixArray);

        if (matrixArray.Length < maxNumberOfDecals)
        {
            for (int i = matrixArray.Length; i < maxNumberOfDecals; i++)
            {
                InstantiateDecal(i);
            }
        }            
    }
}