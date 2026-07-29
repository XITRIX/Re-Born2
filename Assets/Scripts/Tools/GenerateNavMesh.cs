using System.Collections;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class GenerateNavMesh : MonoBehaviour
{
    public static GenerateNavMesh Shared { get; private set; }

    private NavMeshSurface _navMeshSurface;
    private bool _isRebuilding;

    private void Awake()
    {
        if (Shared != null && Shared != this)
            Debug.LogWarning("Multiple active GenerateNavMesh components were found. The newest one will be used.", this);

        Shared = this;
        _navMeshSurface = GetComponent<NavMeshSurface>();
    }

    private void Start()
    {
        StartCoroutine(RebuildNavMeshInternal());
    }

    private void OnDestroy()
    {
        if (Shared == this)
            Shared = null;
    }

    /// <summary>
    /// Starts a delayed NavMesh rebuild. This public entry point can be called
    /// directly from a Visual Scripting graph.
    /// </summary>
    public static void RebuildNavMesh()
    {
        if (Shared == null)
        {
            Debug.LogWarning("Cannot rebuild the NavMesh because there is no active GenerateNavMesh component.");
            return;
        }

        if (!Shared.isActiveAndEnabled)
        {
            Debug.LogWarning("Cannot rebuild the NavMesh while GenerateNavMesh is disabled.", Shared);
            return;
        }

        Shared.StartCoroutine(Shared.RebuildNavMeshInternal());
    }

    /// <summary>
    /// Rebuilds the NavMesh after one frame and waits until it is ready.
    /// Use this with RunAndWaitForCoroutineUnit when later graph nodes depend
    /// on the rebuilt NavMesh.
    /// </summary>
    public static IEnumerator RebuildNavMeshCoroutine()
    {
        var instance = Shared;

        if (instance == null)
        {
            Debug.LogWarning("Cannot rebuild the NavMesh because there is no active GenerateNavMesh component.");
            yield break;
        }

        if (!instance.isActiveAndEnabled)
        {
            Debug.LogWarning("Cannot rebuild the NavMesh while GenerateNavMesh is disabled.", instance);
            yield break;
        }

        yield return instance.RebuildNavMeshInternal();
    }

    private IEnumerator RebuildNavMeshInternal()
    {
        if (_isRebuilding)
        {
            while (_isRebuilding)
                yield return null;

            yield break;
        }

        _isRebuilding = true;

        try
        {
            // Allow graph-driven geometry changes to be applied before sources
            // are collected by NavMeshSurface.
            yield return null;

            if (_navMeshSurface == null || !_navMeshSurface.isActiveAndEnabled)
            {
                Debug.LogWarning("Cannot rebuild the NavMesh because its NavMeshSurface is unavailable.", this);
                yield break;
            }

            _navMeshSurface.BuildNavMesh();

            // Give NavMeshAgents one frame to observe the replacement data.
            yield return null;
        }
        finally
        {
            _isRebuilding = false;
        }
    }
}
