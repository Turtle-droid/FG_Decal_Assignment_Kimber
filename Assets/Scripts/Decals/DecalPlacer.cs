using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalPlacer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Maximum number of decals before they will start being reused")]
    private bool useInstancedDecals = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Use mouse to do raycast then call spawndecal function
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo))
            {
                SpawnDecal(ray.direction, hitInfo);
            }
        }
    }

    void SpawnDecal(Vector3 rayDirection, RaycastHit hitInfo)
    {

        // Depending on bool try call either of the DecalController variants
        if (!useInstancedDecals)
        {
            if (DecalController.SharedInstance)
            {
                //Ordinary gameobject decalcontroller
                DecalController.SharedInstance.SpawnDecal(rayDirection, hitInfo);
            }
        }
         
        else
        {
            if (InstancedDecalController.SharedInstance)
            {
                //Controller that attempts to use Graphics.DrawMeshInstanced
                InstancedDecalController.SharedInstance.SpawnDecal(rayDirection, hitInfo);
            }
        } 
    }
}
