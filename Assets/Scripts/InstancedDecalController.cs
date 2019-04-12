using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstancedDecalController : MonoBehaviour
{
    public static InstancedDecalController SharedInstance;

    [SerializeField]
    Mesh decalMesh;
    [SerializeField]
    Material decalMaterial;

    public float offset = 0.1f;


    //[SerializeField]
    //[Tooltip("The prefab for the bullet hole")]
    //private GameObject bulletHoleDecalPrefab;

    [SerializeField]
    [Tooltip("The number of decals to keep alive at a time.  After this number are around, old ones will be replaced.")]
    private int maxConcurrentDecals = 10;

    //private Queue<GameObject> decalsInPool;
    //private Queue<GameObject> decalsActiveInWorld;

    private Queue<Matrix4x4> decaltransformList;
    private Queue<Matrix4x4> activeDecaltransformList;

    private void Awake()
    {
        InitializeDecals();
        SharedInstance = this;
    }

    private void InitializeDecals()
    {
        //decalsInPool = new Queue<GameObject>();
        //decalsActiveInWorld = new Queue<GameObject>();

        decaltransformList = new Queue<Matrix4x4>();
        activeDecaltransformList = new Queue<Matrix4x4>();

        for (int i = 0; i < maxConcurrentDecals; i++)
        {
            InstantiateDecal();
        }
    }

    private void InstantiateDecal()
    {
        //var spawned = GameObject.Instantiate(bulletHoleDecalPrefab);
        //spawned.transform.SetParent(this.transform);

        //decalsInPool.Enqueue(spawned);
        //spawned.SetActive(false);

        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetTRS(Vector3.zero, Quaternion.Euler(Vector3.zero), Vector3.one);
        decaltransformList.Enqueue(matrix);
    }

    public void SpawnDecal(Vector3 rayDirection, RaycastHit hit)
    {
        Matrix4x4 matrix = GetNextAvailableMatrix();

        //GameObject decal = GetNextAvailableDecal();
        if (matrix != null)
        {
            Vector3 offsetPoint = hit.point - rayDirection.normalized * offset;

            matrix.SetTRS(offsetPoint, Quaternion.FromToRotation(-Vector3.forward, hit.normal), Vector3.one);

            decaltransformList.Enqueue(matrix);

            //decal.transform.position = offsetPoint;
            //decal.transform.rotation = Quaternion.FromToRotation(-Vector3.forward, hit.normal);

            //decal.SetActive(true);

            //decalsActiveInWorld.Enqueue(decal);
        }
    }

    //private GameObject GetNextAvailableDecal()
    //{

    //    if (decalsInPool.Count > 0)
    //        return decalsInPool.Dequeue();

    //    var oldestActiveDecal = decalsActiveInWorld.Dequeue();
    //    return oldestActiveDecal;
    //}

    private Matrix4x4 GetNextAvailableMatrix ()
    {

        if (decaltransformList.Count > 0)
            return decaltransformList.Dequeue();

        var oldestActiveMatrix = activeDecaltransformList.Dequeue();
        return oldestActiveMatrix;
    }


    private void Update()
    {
        Graphics.DrawMeshInstanced(decalMesh, 0, decalMaterial, decaltransformList.ToArray());

        //if (transform.childCount < maxConcurrentDecals)
        //    InstantiateDecal();
        //else if (ShoudlRemoveDecal())
        //    DestroyExtraDecal();
    }

    #if UNITY_EDITOR

    //private bool ShoudlRemoveDecal()
    //{
    //    return transform.childCount > maxConcurrentDecals;
    //}

    private void DestroyExtraDecal()
    {
        //if (decaltransformList.Count > 0)
        //    Destroy(decalsInPool.Dequeue());
        //else if (ShoudlRemoveDecal() && decalsActiveInWorld.Count > 0)
        //    Destroy(decalsActiveInWorld.Dequeue());
    }

    #endif
    }