using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOctTree : MonoBehaviour
{
    public GameObject[] WorldGameObjects;
    public float minNodeSize = 5;
    public OctTree otree;
    
    // Start is called before the first frame update
    void Start()
    {
        otree = new OctTree(WorldGameObjects, minNodeSize);
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            otree.rootNode.Draw();
        }
    }
}
