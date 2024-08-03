using UnityEngine;
using UnityEngine.AI;
using UnityEditor;
using System.Collections.Generic;

public class NavMeshToMeshConverter : MonoBehaviour
{
    public string assetName = "NavMeshMesh";

    [ContextMenu("Convert NavMesh To Mesh")]
    void ConvertNavMeshToMesh()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        Mesh mesh = new Mesh();
        mesh.vertices = navMeshData.vertices;
        mesh.triangles = navMeshData.indices;
        mesh.RecalculateNormals();

        SaveMesh(mesh, assetName);
    }

    void SaveMesh(Mesh mesh, string name)
    {
        string path = "Assets/Models/BloodFloors/" + name + ".asset";

        AssetDatabase.CreateAsset(mesh, path);
        AssetDatabase.SaveAssets();

        Debug.Log("Mesh saved at: " + path);
    }
}