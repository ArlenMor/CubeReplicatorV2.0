using System.Collections.Generic;
using UnityEngine;

public class ExplosionerAfterReplication : MonoBehaviour, IExplodable
{
    [SerializeField, Range(5, 15)] private float _radius = 5f;
    [SerializeField, Range(200, 500)] private float _explosionForce = 250f;

    private Vector3 _centerExplosion;
    private List<Rigidbody> _repulsiveCubes = new List<Rigidbody>();

    public void Init(Vector3 centerExplosion, List<Rigidbody> repulsiveCubes)
    {
        _centerExplosion = centerExplosion;
        _repulsiveCubes = repulsiveCubes;
    }

    public void Explode()
    {
        if (_centerExplosion != null)
        {
            for (int i = 0; i < _repulsiveCubes.Count; i++)
            {
                if (_repulsiveCubes[i] != null)
                    _repulsiveCubes[i].AddExplosionForce(_explosionForce, _centerExplosion, _radius);
            }
        }
    }
}