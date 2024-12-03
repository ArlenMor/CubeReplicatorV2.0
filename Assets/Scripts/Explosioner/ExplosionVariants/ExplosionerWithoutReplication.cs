using System.Collections.Generic;
using UnityEngine;

public class ExplosionerWithoutReplication : IExplodable
{
    private float _explosionForce;
    private float _explosionRadius;
    private Vector3 _explosionCenter;
    private List<Rigidbody> _repulsiveCubes;
    private LayerMask _cubesLayerMask;

    public ExplosionerWithoutReplication(Cube cube)
    {
        _cubesLayerMask = 1 << 3;
        _repulsiveCubes = new List<Rigidbody>();

        _explosionCenter = cube.transform.position;
        _explosionForce = cube.ExplosionForce;
        _explosionRadius = cube.ExplosionRadius;

        Collider[] cubes = Physics.OverlapSphere(_explosionCenter, _explosionRadius, _cubesLayerMask);

        foreach (Collider collider in cubes)
            if (collider.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                _repulsiveCubes.Add(rigidbody);

    }

    public void Explode()
    {
        for (int i = 0; i < _repulsiveCubes.Count; i++)
        {
            if (_repulsiveCubes[i] != null)
                _repulsiveCubes[i].AddExplosionForce(_explosionForce, _explosionCenter, _explosionRadius);
        }
    }
}