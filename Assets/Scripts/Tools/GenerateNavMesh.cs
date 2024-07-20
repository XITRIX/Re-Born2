using System.Collections;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class GenerateNavMesh : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DelayBuildNavMesh());
    }

    IEnumerator DelayBuildNavMesh()
    {
        yield return new WaitForSeconds(0);
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
