using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An index to quickly find any <c>Enemy</c> by position 
/// </summary>
public class EnemyGrid
{
    private static EnemyGrid Instance;

    private Vector2 _start;
    private float _width;
    private List<Enemy>[][] _grid;
    private EnemyGrid(float width, int sizeX, int sizeY, Vector2 start)
    {
        _grid = new List<Enemy>[sizeX][];
        for (var i = 0; i < sizeX; i++)
        {
            _grid[i] = new List<Enemy>[sizeY];
            for (var j = 0; j < sizeY; j++)
            {
                _grid[i][j] = new List<Enemy>();
            }
        }

        _width = width;
        _start = start;
    }

    public static void Init(float width, int sizeX, int sizeY, Vector2 start)
    {
        Instance = new EnemyGrid(width, sizeX, sizeY, start);
    }

    public static List<Enemy> GetEnemiesAroundPoint(Vector2 point, float radius)
    {
        var fromCell = Instance.GetIndicesByPosition(point - new Vector2(radius, radius));
        var toCell = Instance.GetIndicesByPosition(point + new Vector2(radius, radius));
        var result = new List<Enemy>();
        var pointv3 = new Vector3(point.x, point.y);
        for (var i = fromCell[0]; i <= toCell[0]; i++)
        {
            for (var j = fromCell[1]; j <= toCell[1]; j++)
            {
                foreach (var enemy in Instance._grid[i][j])
                {
                    if ((enemy.transform.position - pointv3).magnitude <= radius)
                    {
                        result.Add(enemy);
                    }
                }
            }            
        }
        return result;
    }

    public static void InitPosition(Enemy enemy)
    {
        var indices = Instance.GetIndicesByPosition(enemy.transform.position);
        Instance.GetListByIndices(indices).Add(enemy);
    }

    public static void ClearPosition(Enemy enemy)
    {
        var indices = Instance.GetIndicesByPosition(enemy.transform.position);
        Instance.GetListByIndices(indices).Remove(enemy);
    }

    public static void UpdatePosition(Enemy enemy, Vector2 newPosition)
    {
        var oldPos = Instance.GetIndicesByPosition(enemy.transform.position);
        var newPos = Instance.GetIndicesByPosition(newPosition);
        if (oldPos[0] == newPos[0] && oldPos[1] == newPos[1]) return;
        
        Instance.GetListByIndices(oldPos).Remove(enemy);
        Instance.GetListByIndices(newPos).Add(enemy);
    }

    private List<Enemy> GetListByIndices(IReadOnlyList<int> indices)
    {
        return _grid[indices[0]][indices[1]];
    }

    private int[] GetIndicesByPosition(Vector2 p)
    {
        p -= _start;
        var x = (int) Math.Floor(p.x / _width);
        var y = (int) Math.Floor(p.y / _width);

        x = Math.Max(x, 0);
        y = Math.Max(y, 0);
        x = Math.Min(x, _grid.Length - 1);
        y = Math.Min(y, _grid.Length - 1);

        return new[] {x, y};
    }

    private List<Enemy> GetListByPosition(Vector2 p)
    {
        var i = GetIndicesByPosition(p);
        return GetListByIndices(i);
    }
}