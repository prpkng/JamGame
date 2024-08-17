using System.Collections;
using UnityEngine;

namespace Game.Systems.Noise
{
    public class NoiseSystem : MonoBehaviour
    {
        public static NoiseSystem Instance;

        [SerializeField] private float noiseReducePerSecond = 150f;


        public const float MAX_NOISE = 1000F;

        private float noiseCounter;


        public float CurrentNoise => noiseCounter;


        private void Awake()
        {
            Instance = this;
        }

        public void EmitNoise(float amount)
        {
            Debug.Assert(Instance != null);

            noiseCounter = Mathf.Min(MAX_NOISE, noiseCounter + amount);
        }

        private void FixedUpdate()
        {
            if (noiseCounter >= 0)
                noiseCounter -= noiseReducePerSecond * Time.fixedDeltaTime;
            else
                noiseCounter = 0;
        }

    }
}