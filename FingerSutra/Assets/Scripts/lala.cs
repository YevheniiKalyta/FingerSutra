﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lala : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(this.GetComponent<Mask>().mask.transform.localScale);
    }
}
