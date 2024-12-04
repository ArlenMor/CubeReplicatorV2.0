using System.Collections.Generic;
using UnityEngine;

public class ExplosionerWithoutReplication : MonoBehaviour, IExplodable
{
    [SerializeField]private LayerMask _cubeLayerMask;

    private float _explosionRadius;
    private float _explosionForce;
    private List<Rigidbody> _repulsiveCubes = new List<Rigidbody>();
    private Vector3 _explosionCenter;

    public void Init(Cube cube)
    {
        _repulsiveCubes = new List<Rigidbody>();

        _explosionCenter = cube.transform.position;
        _explosionForce = cube.ExplosionForce;
        _explosionRadius = cube.ExplosionRadius;

        Collider[] cubes = Physics.OverlapSphere(_explosionCenter, _explosionRadius, _cubeLayerMask);

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