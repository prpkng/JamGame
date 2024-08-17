using System.Collections;
using UnityEngine;

namespace Game.Systems.Sound
{
    public class BGMManager : MonoBehaviour
    {

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