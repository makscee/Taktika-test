using System.Collections.Generic;
using UnityEngine;

public class EnemyPath
{
    private Vector2[] _points;
    private float[] _lengths;
    private float _totalLength;

    public EnemyPath(IReadOnlyList<Vector2> points, float randomOffset = 0.5f)
    {
        _lengths = new float[points.Count - 1];
        _points = new Vector2[points.Count];

        var vectorOffset = new Vector2(Random.Range(-randomOffset / 2, randomOffset / 2), Random.Range(-randomOffset / 2, randomOffset / 2));
        for (var i = 0; i < points.Count; i++)
        {
            _points[i] = new Vector2(points[i].x + vectorOffset.x, points[i].y + vectorOffset.y);
        }
        for (var i = 0; i < _points.Length - 1; i++)
        {
            var l = (_points[i + 1] - _points[i]).magnitude;
            _totalLength += l;
            _lengths[i] = l;
        }
    }

    public float Passed { get; private set; } = 0f;
    public Vector2 Move(float dt, float speed)
    {
        Passed = Passed + dt * speed;
        if (Passed >= 1f)
        {
            Passed = 1f;
            return _points[_points.Length - 1];
        }

        var pointA = Vector2.zero;
        var pointB = Vector2.zero;
        var passedLength = Passed * _totalLength;
        var tLength = 0f;
        var localPassed = 0f;
        for (var i = 0; i < _lengths.Length; i++)
        {
            tLength += _lengths[i];
            if (tLength > passedLength)
            {
                pointA = _points[i];
                pointB = _points[i + 1];
                localPassed = (passedLength - tLength + _lengths[i]) / (pointB - pointA).magnitude;
                break;
            }
        }

        var result = pointA + (pointB - pointA) * localPassed;
        return result;
    }
}