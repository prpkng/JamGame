using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
namespace Game.Systems.Level
{

    public class FloorsManager : MonoBehaviour
    {
        public static FloorsManager Instance;
        [SerializeField] private RenderTexture[] floorRenderTextures;
        [SerializeField] private Camera[] floorCameras;

        [SerializeField] private Material floorBlendMaterial;

        public int currentFloor = 0;


        public void ChangeFloor(int nextFloor)
        {
            if (nextFloor == currentFloor) return;

            floorBlendMaterial.SetFloat("_T", 0f);
            floorBlendMaterial.SetTexture("_A", floorRenderTextures[currentFloor]);
            floorBlendMaterial.SetTexture("_B", floorRenderTextures[nextFloor]);
            int c = currentFloor;
            floorCameras[nextFloor].gameObject.SetActive(true);
            floorBlendMaterial.DOFloat(1f, "_T", .5f).onComplete += () =>
            {
                floorCameras[c].gameObject.SetActive(false);
            };
            currentFloor = nextFloor;
        }


        private void Awake()
        {
            floorBlendMaterial.SetFloat("_T", 1f);
            floorBlendMaterial.SetTexture("_B", floorRenderTextures[currentFloor]);
            Instance = this;
        }

        private async void Start()
        {
            foreach (var rt in floorRenderTextures)
            {
                if (rt.width == Screen.width && rt.height == Screen.height) continue;

                rt.Release();
                rt.width = Screen.width;
                rt.height = Screen.height;
                rt.Create();
            }

            await UniTask.WaitForSeconds(.1f);
            Start();
        }
    }

}