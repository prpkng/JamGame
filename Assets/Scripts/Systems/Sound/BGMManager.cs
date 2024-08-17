using System.Collections;
using UnityEngine;

namespace Game.Systems.Sound
{
    public class BGMManager : MonoBehaviour
    {


        public static void SetTension(bool value) =>
            BGMEventInstance.setParameterByName("tensao", value ? 1 : 0, true);

        private static FMOD.Studio.EventInstance BGMEventInstance;

        [SerializeField] private FMODUnity.EventReference bgmEvent;


        void Awake()
        {
            if (BGMEventInstance.isValid())
                BGMEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            BGMEventInstance = FMODUnity.RuntimeManager.CreateInstance(bgmEvent);
            BGMEventInstance.start();
        }
    }
}