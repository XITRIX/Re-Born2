using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(MeshCollider))]
public sealed class RoundedBoxCollider3D : MonoBehaviour
{
    private const float MinimumDimension = 0.001f;

    [SerializeField]
    private Vector3 center = new(0f, 0.38f, -0.13f);

    [SerializeField]
    private Vector3 size = new(0.66f, 0.75f, 0.27f);

    [SerializeField, Min(0f)]
    private float cornerRadius = 0.14f;

    [SerializeField, Range(1, 8)]
    private int cornerSegments = 3;

    private MeshCollider _meshCollider;
    private Mesh _generatedMesh;

    private void Awake()
    {
        RebuildCollider();
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            RebuildCollider();
        }
    }

    private void OnDestroy()
    {
        if (_generatedMesh == null)
        {
            return;
        }

        if (_meshCollider != null && _meshCollider.sharedMesh == _generatedMesh)
        {
            _meshCollider.sharedMesh = null;
        }

        if (Application.isPlaying)
        {
            Destroy(_generatedMesh);
        }
        else
        {
            DestroyImmediate(_generatedMesh);
        }
    }

    private void RebuildCollider()
    {
        _meshCollider ??= GetComponent<MeshCollider>();

        if (_generatedMesh == null)
        {
            _generatedMesh = new Mesh
            {
                name = $"{name} Rounded Box Collider",
                hideFlags = HideFlags.HideAndDontSave
            };
        }
        else
        {
            _generatedMesh.Clear();
        }

        var outline = CreateOutline();
        var vertices = CreateVertices(outline);
        var triangles = CreateTriangles(outline.Count);

        _generatedMesh.SetVertices(vertices);
        _generatedMesh.SetTriangles(triangles, 0);
        _generatedMesh.RecalculateNormals();
        _generatedMesh.RecalculateBounds();

        // Clearing the reference forces PhysX to recook the updated convex mesh.
        _meshCollider.sharedMesh = null;
        _meshCollider.convex = true;
        _meshCollider.sharedMesh = _generatedMesh;
    }

    private List<Vector2> CreateOutline()
    {
        var width = Mathf.Max(MinimumDimension, Mathf.Abs(size.x));
        var height = Mathf.Max(MinimumDimension, Mathf.Abs(size.y));
        var halfWidth = width * 0.5f;
        var halfHeight = height * 0.5f;
        var radius = Mathf.Clamp(cornerRadius, 0f, Mathf.Min(halfWidth, halfHeight));

        if (radius <= Mathf.Epsilon)
        {
            return new List<Vector2>
            {
                new(halfWidth, -halfHeight),
                new(halfWidth, halfHeight),
                new(-halfWidth, halfHeight),
                new(-halfWidth, -halfHeight)
            };
        }

        var segments = Mathf.Clamp(cornerSegments, 1, 8);
        var outline = new List<Vector2>(4 * (segments + 1));
        var cornerCenters = new[]
        {
            new Vector2(halfWidth - radius, halfHeight - radius),
            new Vector2(-halfWidth + radius, halfHeight - radius),
            new Vector2(-halfWidth + radius, -halfHeight + radius),
            new Vector2(halfWidth - radius, -halfHeight + radius)
        };

        for (var corner = 0; corner < cornerCenters.Length; corner++)
        {
            for (var segment = 0; segment <= segments; segment++)
            {
                var angle = (corner * 90f + segment * 90f / segments) * Mathf.Deg2Rad;
                var offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                outline.Add(cornerCenters[corner] + offset);
            }
        }

        return outline;
    }

    private List<Vector3> CreateVertices(IReadOnlyList<Vector2> outline)
    {
        var depth = Mathf.Max(MinimumDimension, Mathf.Abs(size.z));
        var halfDepth = depth * 0.5f;
        var vertices = new List<Vector3>(outline.Count * 2);

        for (var i = 0; i < outline.Count; i++)
        {
            vertices.Add(center + new Vector3(outline[i].x, outline[i].y, halfDepth));
        }

        for (var i = 0; i < outline.Count; i++)
        {
            vertices.Add(center + new Vector3(outline[i].x, outline[i].y, -halfDepth));
        }

        return vertices;
    }

    private static List<int> CreateTriangles(int outlineCount)
    {
        var triangles = new List<int>((outlineCount - 2) * 6 + outlineCount * 6);

        // Front and back faces.
        for (var i = 1; i < outlineCount - 1; i++)
        {
            triangles.Add(0);
            triangles.Add(i);
            triangles.Add(i + 1);

            triangles.Add(outlineCount);
            triangles.Add(outlineCount + i + 1);
            triangles.Add(outlineCount + i);
        }

        // Thin side wall joining both rounded faces.
        for (var i = 0; i < outlineCount; i++)
        {
            var next = (i + 1) % outlineCount;
            var front = i;
            var frontNext = next;
            var back = outlineCount + i;
            var backNext = outlineCount + next;

            triangles.Add(front);
            triangles.Add(backNext);
            triangles.Add(frontNext);

            triangles.Add(front);
            triangles.Add(back);
            triangles.Add(backNext);
        }

        return triangles;
    }

    private void OnDrawGizmosSelected()
    {
        var outline = CreateOutline();
        var depth = Mathf.Max(MinimumDimension, Mathf.Abs(size.z));
        var halfDepth = depth * 0.5f;
        var previousMatrix = Gizmos.matrix;
        var previousColor = Gizmos.color;

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = new Color(0.35f, 1f, 0.45f);

        for (var i = 0; i < outline.Count; i++)
        {
            var next = (i + 1) % outline.Count;
            var front = center + new Vector3(outline[i].x, outline[i].y, halfDepth);
            var frontNext = center + new Vector3(outline[next].x, outline[next].y, halfDepth);
            var back = center + new Vector3(outline[i].x, outline[i].y, -halfDepth);
            var backNext = center + new Vector3(outline[next].x, outline[next].y, -halfDepth);

            Gizmos.DrawLine(front, frontNext);
            Gizmos.DrawLine(back, backNext);
            Gizmos.DrawLine(front, back);
        }

        Gizmos.matrix = previousMatrix;
        Gizmos.color = previousColor;
    }
}
