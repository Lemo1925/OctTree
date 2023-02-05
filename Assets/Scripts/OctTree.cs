using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctTree
{
    public readonly OctTreeNode rootNode;

    public OctTree(GameObject[] worldGameObjects, float minNodeSize)
    {
        Bounds bounds = new Bounds();

        foreach (var go in worldGameObjects)
        {
            bounds.Encapsulate(go.GetComponent<Collider>().bounds);
        }

        float maxSize = Mathf.Max(new float[] { bounds.size.x, bounds.size.y, bounds.size.z });
        Vector3 sizeVector = new Vector3(maxSize, maxSize, maxSize) * 0.5f;
        bounds.SetMinMax(bounds.center - sizeVector, bounds.center + sizeVector);
        rootNode = new OctTreeNode(bounds, minNodeSize);
        AddObjects(worldGameObjects);
    }

    private void AddObjects(GameObject[] worldGameObjects)
    {
        foreach (var go in worldGameObjects)
        {
            rootNode.AddObject(go);
        }
    }
}
