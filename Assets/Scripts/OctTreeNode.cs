using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctTreeNode
{
    private Bounds nodeBounds;
    private float minSize;
    private Bounds[] childBounds;
    private OctTreeNode[] children = null;
    public OctTreeNode(Bounds b, float minSize)
    {
        nodeBounds = b;
        this.minSize = minSize;

        float quarter = nodeBounds.size.x / 4.0f;
        float childLength = nodeBounds.size.x / 2;
        Vector3 childSize = new Vector3(childLength, childLength, childLength);
        childBounds = new Bounds[8];
        childBounds[0] = new Bounds(nodeBounds.center + new Vector3(-quarter, quarter, -quarter), childSize);
        childBounds[1] = new Bounds(nodeBounds.center + new Vector3(-quarter, quarter, quarter), childSize);
        childBounds[2] = new Bounds(nodeBounds.center + new Vector3(quarter, quarter, -quarter), childSize);
        childBounds[3] = new Bounds(nodeBounds.center + new Vector3(quarter, quarter, quarter), childSize);
        childBounds[4] = new Bounds(nodeBounds.center + new Vector3(-quarter, -quarter, -quarter), childSize);
        childBounds[5] = new Bounds(nodeBounds.center + new Vector3(quarter, -quarter, -quarter), childSize);
        childBounds[6] = new Bounds(nodeBounds.center + new Vector3(-quarter, -quarter, quarter), childSize);
        childBounds[7] = new Bounds(nodeBounds.center + new Vector3(quarter, -quarter, quarter), childSize);
    }

    public void AddObject(GameObject go)
    {
        DivideAndAdd(go);
    }

    private void DivideAndAdd(GameObject go)
    {
        if (nodeBounds.size.x <= minSize) return;
        if (children == null)
            children = new OctTreeNode[8];
        bool dividing = false;
        for (int i = 0; i < 8; i++)
        {
            if (children[i] == null)
            {
                children[i] = new OctTreeNode(childBounds[i], minSize);
            }

            if (childBounds[i].Intersects(go.GetComponent<Collider>().bounds))
            {
                dividing = true;
                children[i].DivideAndAdd(go);
            }
        }

        if (dividing == false)
        {
            children = null;
        }
    }

    public void Draw()
    {
        Gizmos.color = new Color(0, 1, 0);
        Gizmos.DrawWireCube(nodeBounds.center, nodeBounds.size);
        if (children != null)
        {
            for (int i = 0; i < 8; i++)
            {
                if (children[i] != null)
                {
                    children[i].Draw();
                }
            }
        }
    }
}
