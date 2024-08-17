using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Game.Utilities;
using UnityEngine;
using UnityEngine.Rendering;

namespace Game.Systems.Level
{

    public class BloodLevelEndingCondition : ILevelEndingCondition
    {
        public bool IsCompleted => throw new System.NotImplementedException();

        public bool[] CompletionList => cachedCompletionList;

        private bool[] cachedCompletionList = new bool[0];


        private Texture2D cachedReadTexture;

        public BloodLevelEndingCondition()
        {
            var rt = LevelManager.Instance.bloodRenderTextures[0];
            cachedReadTexture = new Texture2D(rt.width, rt.height, TextureFormat.RFloat, false);
        }

        public void Check()
        {
            for (int i = 0; i < cachedCompletionList.Length; i++)
            {
                //if (cachedCompletionList[i]) continue;

                var (pixelPos, pixelSize, floor) = splashLocations[i];

                var rt = LevelManager.Instance.bloodRenderTextures[floor];

                RenderTexture.active = rt;

                cachedReadTexture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
                RenderTexture.active = null;

                float value = cachedReadTexture.GetPixels(pixelPos.x, pixelPos.y, pixelSize.x, pixelSize.y).Average(p => p.r);

                //Debug.Log($"{i}:{value}");

                cachedCompletionList[i] = value <= Mathf.Epsilon;
            }
        }

        private List<(Vector2Int pos, Vector2Int size, int floor)> splashLocations = new();

        public void AddBloodSplash(Vector2 normalizedPos, float size, int floor)
        {
            var rt = LevelManager.Instance.bloodRenderTextures[0];
            Vector2 normalizedSize = Vector2.one * size / (LevelManager.Instance.bloodCamera.orthographicSize * 2);
            Vector2Int pixelSize = Vector2Int.RoundToInt(normalizedSize * new Vector2(rt.width, rt.height));


            Vector2Int pixelPos = Vector2Int.RoundToInt(normalizedPos * new Vector2(rt.width, rt.height)) - pixelSize / 2;

            splashLocations.Add((pixelPos, pixelSize, floor));
            cachedCompletionList = cachedCompletionList.Append(false).ToArray();
        }
    }

    public class BloodCondition : MonoBehaviour
    {
        private const int BLOOD_LAYER = 13;
        [SerializeField] private Texture[] bloodSplashes;
        [SerializeField] private GameObject bloodSplashPrefab;
        [SerializeField] private float splashCheckRadius = 1.5f;

        private static readonly int BaseMapIndex = Shader.PropertyToID("_BaseMap");

        [SerializeField] private int floor;

        private void Start()
        {
            var bloodSplash = Instantiate(bloodSplashPrefab, transform);
            bloodSplash.transform.eulerAngles += Vector3.up * Random.Range(0, 360f);
            bloodSplash.layer = BLOOD_LAYER;
            bloodSplash.GetComponent<MeshRenderer>().material.SetTexture(BaseMapIndex, bloodSplashes.PickRandom());

            Vector2 normalizedPos = LevelManager.Instance.bloodCamera.WorldToViewportPoint(transform.position);
            LevelManager.GetLevelEndingCondition<BloodLevelEndingCondition>().AddBloodSplash(normalizedPos, splashCheckRadius, floor);

        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, splashCheckRadius);
        }


        private void OnTriggerEnter(Collider other)
        {

        }

    }
}