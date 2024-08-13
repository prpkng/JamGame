using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBloodSys : MonoBehaviour
{

    [SerializeField] private RenderTexture bloodRT;
    [SerializeField] private Mesh bloodMesh;

    private void Start()
    {

        GetComponentInChildren<Camera>().targetTexture = bloodRT;
        GetComponentInChildren<MeshFilter>().mesh = bloodMesh;
        GetComponentInChildren<MeshRenderer>().material.SetTexture("_Mask", bloodRT);
    }
}
