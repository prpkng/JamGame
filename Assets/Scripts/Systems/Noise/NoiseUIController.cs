using System.Collections;
using System.Linq;
using Game.Player;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Systems.Noise
{
    public class NoiseUIController : MonoBehaviour
    {
        [SerializeField] private UIDocument document;

        [SerializeField] private float powStrength = .4f;

        private VisualElement progressBarElement;

        private void Awake()
        {
            progressBarElement = document.rootVisualElement.Q("progress_bar");
        }

        private void Update()
        {
            float noiseValue = NoiseSystem.Instance.CurrentNoise / NoiseSystem.MAX_NOISE;

            noiseValue = Mathf.Pow(noiseValue, powStrength);

            progressBarElement.style.height = new StyleLength(new Length(noiseValue * 100f, LengthUnit.Percent));

            progressBarElement.EnableInClassList("danger", noiseValue > .65f);
            progressBarElement.parent.transform.position = noiseValue > .65f
                ? new Vector2(Mathf.Sin(Time.time * 1000f), Mathf.Cos(Time.time * 300f)) * 2f
                : Vector2.zero;
        }

    }
}