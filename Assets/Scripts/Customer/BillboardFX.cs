﻿  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardFX : MonoBehaviour
{
	private Transform camTransform;

	Quaternion originalRotation;

    void Start()
    {
        camTransform = GameObject.Find("Camera Rig").transform.GetChild(0).transform;
        originalRotation = transform.rotation;
    }

    void Update()
    {
     	transform.rotation = camTransform.rotation * originalRotation;   
    }
}