using System.Collections.Generic;
using UnityEngine;

public class Explosioner : MonoBehaviour
{
    [SerializeField] private ExplosionerAfterReplication _explosionerAfterReplication;
    [SerializeField] private ExplosionerWithoutReplication _explosionerWithoutReplication;

    public void Explode(Cube cube)
    {
        _explosionerWithoutReplication.Init(cube);
        _explosionerWithoutReplication.Explode();
        Destroy(cube.gameObject);
    }

    public void Explode(Vector3 centerExplosion, List<Rigidbody> replicatedCubes)
    {
        _explosionerAfterReplication.Init(centerExplosion, replicatedCubes);
        _explosionerAfterReplication.Explode();
    }
}