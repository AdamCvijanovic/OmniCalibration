using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class NavigationBaker : MonoBehaviour
{
    
    public List<NavMeshSurface> surfaces = new List<NavMeshSurface>();
    public Transform[] objectsToRotate;

    // Use this for initialization
    void Start()
    {

        for (int j = 0; j < objectsToRotate.Length; j++)
        {
            objectsToRotate[j].localRotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
        }

        for (int i = 0; i < surfaces.Count; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }

    public void AddSurface(GameObject obj)
    {
        if(obj.GetComponent<NavMeshSurface>()!=null)
            surfaces.Add(obj.GetComponent<NavMeshSurface>());
    }

    //void Update()
    //{
    //    for (int j = 0; j < objectsToRotate.Length; j++)
    //    {
    //        objectsToRotate[j].localRotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
    //    }

    //    for (int i = 0; i < surfaces.Count; i++)
    //    {
    //        surfaces[i].BuildNavMesh();
    //    }
    //}

    public void UpdateMesh()
    {
        for (int j = 0; j < objectsToRotate.Length; j++)
        {
            objectsToRotate[j].localRotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
        }
    
        for (int i = 0; i < surfaces.Count; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }

}
