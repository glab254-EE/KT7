using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSort : MonoBehaviour
{
    [SerializeField] public List<Transform> transforms{ get; private set; }
    [SerializeField] private string sortingTag;
    [SerializeField] private float gapammount = 1f;
    [SerializeField] private float timetosort = 0.5f;
    [SerializeField] private bool Sorting = false;

    void Start()
    {
        foreach (GameObject t in GameObject.FindGameObjectsWithTag(sortingTag))
        {
            if (transforms == null) transforms = new List<Transform>();
                transforms.Add(t.transform);
        }
    }
    private void Moveobjects()
    {

        for (int i = 0; i < transforms.Count; i++)
        {
            Transform t = transforms[i];
            float xpos = (i-transforms.Count/2)* gapammount;
            t.position = new Vector3(xpos,0,0);
        }
    }
    private void Sort()
    {
        for (int i=0; i<transforms.Count; i++)
        {
            Transform Ctransform = transforms[i];
            float Volume = Ctransform.localScale.x * Ctransform.localScale.y * Ctransform.localScale.z;
            Debug.Log(Volume);
            if (i + 1 < transforms.Count) 
            { 
                Transform nexttransofmr = transforms[i + 1];
                float NextVolume = nexttransofmr.localScale.x * nexttransofmr.localScale.y * nexttransofmr.localScale.z;
                Debug.Log(NextVolume);
                if (Volume > NextVolume)
                {
                    transforms[i + 1] = Ctransform;
                    transforms[i] = nexttransofmr;
                }
            }
            Moveobjects();
        }
    }

    float timer = 0;
    void Update()
    {
        if (Sorting)
        {
            timer += Time.deltaTime;
            if (timer > timetosort)
            {
                timer = 0;
                Sort();
            }
        }
    }
}
