using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSegment : MonoBehaviour
{
    public float Width => width;

    [SerializeField]
    private ObstacleWall topWall;
    [SerializeField]
    private ObstacleWall bottomWall;

    [SerializeField]
    private int gapSize = 3;
    [SerializeField]
    private int minTopWallSize = 2;
    [SerializeField]
    private int wallHeight = 10;
    [SerializeField]
    private float width = 4.64f;

    private void OnEnable()
    {
        GenerateWalls();
    }

    [ContextMenu("Test wall generation")]
    public void GenerateWalls()
    {
        int topWallHeight = Random.Range(minTopWallSize, wallHeight - gapSize);
        int bottomWallHeight = wallHeight - topWallHeight - gapSize;
        topWall.SetHeight(topWallHeight, true);
        bottomWall.SetHeight(bottomWallHeight, false);
    }
}