using UnityEngine;

namespace Game
{
    public static class MathUtils
    {
        public static float DeltaRelativize(float value) =>
            value == 0 ? 1 : Time.deltaTime / value;
        public static float FixedDeltaRelativize(float value) =>
            value == 0 ? 1 : Time.fixedDeltaTime / value;
    }
}