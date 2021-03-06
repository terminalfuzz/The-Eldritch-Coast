﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public GameManager gameManager;
 
    private Vector3 Origin;
    private Vector3 Diference;
    private bool Drag = false;

    private void Start()
    {
        Camera.main.transform.position = gameManager.currentPlayer.transform.position + (new Vector3(8, 4 , -8));
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            Diference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (Drag == false)
            {
                Drag = true;
                Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            Drag = false;
        }
        if (Drag == true)
        {
            Camera.main.transform.position = Origin - Diference;
        }

        if (Input.GetMouseButton(2))
        {
            Camera.main.transform.position = gameManager.currentPlayer.transform.position + (new Vector3(8, 4, -8));
        }
    }
}

