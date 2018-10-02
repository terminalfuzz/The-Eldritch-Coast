using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPlayerScript : PlayerScript {

    public int tileX;
    public int tileY;

    public List<Node> currentPath = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (currentPath != null)
        {
            Debug.Log("Player path is no longer null");
            int currNode = 0;
            while (currNode < currentPath.Count - 1)
            {
                Vector3 start = GameManager.Instance.TileCoordToWorldCoord(currentPath[currNode].x, currentPath[currNode].y) + new Vector3(0, 0.3f, 0);
                Vector3 end = GameManager.Instance.TileCoordToWorldCoord(currentPath[currNode + 1].x, currentPath[currNode + 1].y) + new Vector3(0, 0.3f, 0);

                Debug.DrawLine(start, end, Color.red);

                currNode++;
            }
        }
	}

    private void OnMouseUp()
    {
        Debug.Log("Player Clicked, Also: " + currentPath.Count);

    }

}
