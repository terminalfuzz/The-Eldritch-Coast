﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    protected static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public GameObject userPlayerPrefab;
    public GameObject currentPlayer;
    public List<GameObject> turnOrder = new List<GameObject>();

    public int currentPlayerIndex;

    List<Node> currentPath = null;

    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        turnOrder.Add(currentPlayer);

    }

    // Update is called once per frame
    void Update()
    {
        if (turnOrder.Count > 0)
        {
            currentPlayer = turnOrder[currentPlayerIndex];
        }
    }

    public void nextTurn()
    {
        if (turnOrder.Count > 0)
        {
            if (currentPlayerIndex + 1 < turnOrder.Count)
            {
                currentPlayerIndex++;
            }
            else
            {
                currentPlayerIndex = 0;
            }
        }
    }

    public Vector3 TileCoordToWorldCoord(int x, int y)
    {
        return new Vector3(x, 0, y);
    }

    public void findBestRouteTo(int x, int y)
    {
        Debug.Log("Current Player: " + currentPlayer);
        //Tile x,y will not always be the transforms actual x,y position in the world

        currentPlayer.GetComponent<UserPlayerScript>().tileX = (int)currentPlayer.transform.position.x;
        currentPlayer.GetComponent<UserPlayerScript>().tileY = (int)currentPlayer.transform.position.y;

        currentPath = null;

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        List<Node> unvisited = new List<Node>();

        Node source = LevelManager.Instance.graph[currentPlayer.GetComponent<UserPlayerScript>().tileX, currentPlayer.GetComponent<UserPlayerScript>().tileY];
        Node target = LevelManager.Instance.graph[x, y];

        //Pathfinding via Dijkstra
        dist[source] = 0;
        prev[source] = null;

        foreach (Node vertex in LevelManager.Instance.graph)
        {
            if (vertex != source)
            {
                dist[vertex] = Mathf.Infinity;
                prev[vertex] = null;
            }

            unvisited.Add(vertex);

        }

        while (unvisited.Count > 0)
        {
            //u is the unvisited node with the smallest distance
            Node u = null;

            foreach (Node possibleU in unvisited)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if (u == target)
            {
                break;
            }

            unvisited.Remove(u);

            foreach (Node v in u.edges)
            {
                float alt = dist[u] + u.DistanceTo(v);
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }
        
        if (dist[target] == Mathf.Infinity)
        {
            return;
        }

        currentPath = new List<Node>();
        Node curr = target;

        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        currentPath.Reverse();
 
        currentPlayer.GetComponent<UserPlayerScript>().currentPath = currentPath;
    }
}
