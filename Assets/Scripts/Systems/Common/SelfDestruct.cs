using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Systems.Common
{
    public class SelfDestruct : MonoBehaviour
    {
        public float seconds = 1f;
        public bool ignoreTimeScale;
        public bool destroyOnlyScript;

        private async void Start()
        {
            await UniTask.WaitForSeconds(seconds, ignoreTimeScale);
            if (destroyOnlyScript)
            {
                Destroy(this);
                return;
            }
            Destroy(gameObject);
        }
    }
}