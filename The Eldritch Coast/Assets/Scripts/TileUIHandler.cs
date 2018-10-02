using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileUIHandler : MonoBehaviour {

    public int tileX;
    public int tileY;

    private MeshRenderer meshRenderer;
    private bool isSelected;
    private Color myColor;


	// Use this for initialization
	void Start () {
        meshRenderer = GetComponent<MeshRenderer>();
        myColor = meshRenderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseEnter()
    {
        meshRenderer.material.color = Color.red;
        Debug.Log("Current Position: (" + tileX + ", " + tileY + ")");
        GameManager.Instance.findBestRouteTo(tileX, tileY);
    }

    private void OnMouseExit()
    {
        meshRenderer.material.color = myColor;        
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked: (" + tileX + ", " + tileY + ")");
        
    }

}
