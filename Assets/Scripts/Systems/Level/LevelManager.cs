using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

namespace Game.Systems.Level
{
    public class LevelManager : MonoBehaviour
    {
        public Camera bloodCamera;
        public RenderTexture[] bloodRenderTextures;

        public List<ILevelEndingCondition> levelEndingConditions = new();

        public static LevelManager Instance;
        private void Awake()
        {
            Instance = this;
            levelEndingConditions = new();
        }

        private async void Start()
        {
            while (true)
            {
                levelEndingConditions.ForEach(c => c.Check());
                await UniTask.WaitForSeconds(.1f);
            }
        }

        private void Update()
        {
            print(string.Join('|', levelEndingConditions.Select(l => string.Join(',', l.CompletionList.Select(b => b.ToString())))));
        }


        public static T GetLevelEndingCondition<T>() where T : ILevelEndingCondition, new()
        {
            if (!Instance.levelEndingConditions.Any(l => l is T))
                Instance.levelEndingConditions.Add(new T());

            return (T) Instance.levelEndingConditions.First(l => l is T);
        }

    }
}