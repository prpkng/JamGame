using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class AutoLayerChildren : MonoBehaviour
{
    [Layer] public int targetLayer;

    public bool recursive = false;

    private void Awake()
    {
        var childQueue = new Queue<Transform>();
        foreach (Transform c in transform) childQueue.Enqueue(c);

        while (true)
        {
            if (childQueue.Count <= 0) break;

            var child = childQueue.Dequeue();

            child.gameObject.layer = targetLayer;

            if (recursive)
                foreach (Transform c in child) childQueue.Enqueue(c);
        }
    }
}
