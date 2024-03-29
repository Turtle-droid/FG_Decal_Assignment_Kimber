﻿using System.Collections.Generic;
using UnityEngine;

public class DecalController : MonoBehaviour
{
    // Shared instance that other objects can use call decalcontroller functions
    public static DecalController SharedInstance;

    //Offset for decal placement
    [SerializeField]
    private float offset = 0.1f;

    [SerializeField]
    [Tooltip("The prefab for the bullet hole")]
    private GameObject bulletHoleDecalPrefab;

    [SerializeField]
    [Tooltip("Maximum number of decals before they will start being reused")]
    [Range(0, 1000)]
    private int maxNumberOfDecals = 10;

    // Pool Queue
    private Queue<GameObject> decalPoolQueue;

    private void Awake()
    {
        InitializeDecals();
        SharedInstance = this;
    }

    // Initalize our Queues
    private void InitializeDecals()
    {
        decalPoolQueue = new Queue<GameObject>();

        // Call function to populate que with decals
        for (int i = 0; i < maxNumberOfDecals; i++)
        {
            InstantiateDecal();
        }
    }

    // Instansiates decals and places them in the decal pool deactivated
    private void InstantiateDecal()
    {
        var spawnedDecal = GameObject.Instantiate(bulletHoleDecalPrefab);
        spawnedDecal.transform.SetParent(this.transform);

        decalPoolQueue.Enqueue(spawnedDecal);
        spawnedDecal.SetActive(false);
    }

    // Spawn decals in the world based on raycast 
    public void SpawnDecal(Vector3 rayDirection, RaycastHit hit)
    {
        // Get a decal to use for spawning
        GameObject decal = GetNextAvailableDecal();

        if (decal != null)
        {
            //Offset spawnpoint in order to avoid z-fighting
            Vector3 offsetPoint = hit.point - rayDirection.normalized * offset;

            //Set decal position and rotation
            decal.transform.position = offsetPoint;
            decal.transform.rotation = Quaternion.FromToRotation(-Vector3.forward, hit.normal);

            //Activate decal and place it in the active queue
            decal.SetActive(true);
            decalPoolQueue.Enqueue(decal);
        }
    }

    //Function to fetch a decal to use in spawning
    private GameObject GetNextAvailableDecal()
    {   
          return decalPoolQueue.Dequeue();
    }

    private void Update()
    {
        if (transform.childCount < maxNumberOfDecals)
            InstantiateDecal();
        else if (ShoudlRemoveDecal())
            DestroyExtraDecal();
    }

    private bool ShoudlRemoveDecal()
    {
        return transform.childCount > maxNumberOfDecals;
    }

    private void DestroyExtraDecal()
    {
          Destroy(decalPoolQueue.Dequeue());
    }
}