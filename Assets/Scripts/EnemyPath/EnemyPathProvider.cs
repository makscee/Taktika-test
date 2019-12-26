using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathProvider : MonoBehaviour
{
    public GameObject[] Points;
    public LineRenderer LineRenderer;
    private List<Vector2> _vPoints;

    private void Awake()
    {
        _vPoints = new List<Vector2>(Points.Length);
        float minX = float.MaxValue, minY = float.MaxValue, maxX = float.MinValue, maxY = float.MinValue;
        var lineInd = 0;
        LineRenderer.positionCount = Points.Length;
        foreach (var point in Points)
        {
            var position = point.transform.position;
            _vPoints.Add(position);
            minX = Math.Min(minX, position.x);
            minY = Math.Min(minY, position.y);
            maxX = Math.Max(maxX, position.x);
            maxY = Math.Max(maxY, position.y);
            LineRenderer.SetPosition(lineInd, position);
            lineInd++;
        }

        const float gridWidth = 2f; // should be around the same as average tower radius
        minX = (float)Math.Floor(minX / 2) * 2;
        minY = (float)Math.Floor(minY / 2) * 2;
        maxX = (float) Math.Ceiling(maxX / 2) * 2;
        maxY = (float) Math.Ceiling(maxY / 2) * 2;
        var sizeX = (int) (maxX - minX) / 2;
        var sizeY = (int) (maxY - minY) / 2;
        EnemyGrid.Init(gridWidth, sizeX, sizeY, new Vector2(minX, minY));
    }

    public EnemyPath GetPath()
    {
        return new EnemyPath(_vPoints);
    }
}