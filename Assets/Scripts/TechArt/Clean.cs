using System;
using System.Collections.Generic;
using UnityEngine;

public class Clean : MonoBehaviour
{

    [SerializeField] private Camera _camera;

    [SerializeField] private Texture2D _dirtMaskBase;
    [SerializeField] private Texture2D _brush;
    [SerializeField] private Material _material;

    Texture2D _dirtMaskTexture;



    private void Start()
    {
        CreateTexture();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                //if (hit.collider)
                //{
                //    _material.SetVector("_Position", new Vector4(hit.point.x, hit.point.y, hit.point.z, 1f));
                //}

                Vector2 textureCoord = hit.textureCoord;

                int pixelX = (int)(textureCoord.x * _dirtMaskTexture.width);
                int pixelY = (int)(textureCoord.y * _dirtMaskTexture.height);
                int pixelXOffset = pixelX - (_brush.width / 2);
                int pixelYOffset = pixelY - (_brush.height / 2);

                for (int x = 0; x < _brush.width; x++)
                {
                    for (int y = 0; y < _brush.height; y++)
                    {
                        Color pixelDirt = _brush.GetPixel(x, y);
                        Color pixelDirtMask = _dirtMaskTexture.GetPixel(pixelXOffset + x, pixelYOffset + y);

                        _dirtMaskTexture.SetPixel(pixelXOffset + x, pixelYOffset + y, new Color(0, pixelDirtMask.g * pixelDirt.g, 0));
                    }
                }

                _dirtMaskTexture.Apply();
            }
        }
    }

    private void CreateTexture()
    {
        _dirtMaskTexture = new Texture2D(_dirtMaskBase.width, _dirtMaskBase.height);
        _dirtMaskTexture.SetPixels(_dirtMaskBase.GetPixels());
        _dirtMaskTexture.Apply();

        _material.SetTexture("_Mask", _dirtMaskTexture);
    }
}