using System.Collections.Generic;
using UnityEngine;

public class Explosioner : MonoBehaviour
{
    [SerializeField] private ExplosionerAfterReplication _explosionerAfterReplication;
    private IExplodable _explosionerWithoutReplication;

    private void FixedUpdate()
    {
        if (_explosionerAfterReplication.ExplodeThisFrame == true)
        {
            _explosionerAfterReplication.Explode();
        }
        if(_explosionerWithoutReplication != null)
        {
            _explosionerWithoutReplication.Explode();
            _explosionerWithoutReplication = null;
        }
    }

    public void Explode(Cube cube)
    {
        _explosionerWithoutReplication = new ExplosionerWithoutReplication(cube);
        Destroy(cube.gameObject);
    }

    public void Explode(Vector3 centerExplosion, List<Rigidbody> replicatedCubes)
    {
        _explosionerAfterReplication.Init(centerExplosion, replicatedCubes);
    }
}