#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

public static class NavMeshToMeshConverter
{
    public const string DEFAULT_ASSET_NAME = "NavMeshMesh";

    [MenuItem("Tools/Convert NavMesh To Mesh")]
    public static void ConvertNavMeshToMesh()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        Mesh mesh = new()
        {
            vertices = navMeshData.vertices,
            triangles = navMeshData.indices,
        };
        mesh.RecalculateNormals();

        SaveMesh(mesh, DEFAULT_ASSET_NAME);
    }

    private static void SaveMesh(Mesh mesh, string name)
    {
        string path = "Assets/Models/BloodFloors/" + name + ".asset";

        AssetDatabase.CreateAsset(mesh, path);
        AssetDatabase.SaveAssets();
        EditorGUIUtility.PingObject(mesh);

        Debug.Log("Mesh saved at: " + path);
    }
}

#endif