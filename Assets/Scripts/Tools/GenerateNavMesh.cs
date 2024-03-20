using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class GenerateNavMesh : MonoBehaviour
{
    void Start()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
