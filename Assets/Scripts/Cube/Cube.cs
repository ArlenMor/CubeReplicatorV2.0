using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    private ColorChanger _colorChanger = new ColorChanger();
    private MeshRenderer _meshRenderer;
    private List<Rigidbody> _rigidbodyReplicatedCubes = new List<Rigidbody>();
    private bool _willExplosive = false;
    private float _reduceMultiplyChanceCoef = 2f;
    private float _increaseExplosiveForce = 1.5f;
    private float _increaseExplosiveRadius = 1.2f;

    public float ExplosionForce { get; private set; }
    public float ExplosionRadius { get; private set; }
    public CubeReplicator Replicator { get; private set; }
    public Explosioner Explosioner { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public float MultiplyChance { get; private set; }

    private void Awake()
    {
        MultiplyChance = 1f;

        ExplosionForce = 250f;
        ExplosionRadius = 2f;

        Rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material.color = _colorChanger.GetRandomColor();
    }

    private void Start()
    {
        if (Replicator == null)
        {
            Replicator = FindFirstObjectByType<CubeReplicator>();

            if (Replicator == null)
                Debug.LogError($"Объект {this.name} не нашёл важный компонент {typeof(CubeReplicator)}!");
        }

        if (Explosioner == null)
        {
            Explosioner = FindFirstObjectByType<Explosioner>();

            if (Explosioner == null)
                Debug.LogError($"Объект {this.name} не нашёл важный компонент {typeof(Explosioner)}!");
        }
    }

    public void Init(Cube cube)
    {
        MultiplyChance = cube.MultiplyChance / _reduceMultiplyChanceCoef;
        ExplosionForce = cube.ExplosionForce * _increaseExplosiveForce;
        ExplosionRadius = cube.ExplosionRadius * _increaseExplosiveRadius;

        Explosioner = cube.Explosioner;
        Replicator = cube.Replicator;
    }

    public void Replicate()
    {
        System.Random _random = new System.Random();

        if (MultiplyChance >= _random.NextDouble())
            _rigidbodyReplicatedCubes = Replicator.Replicate(this);
        else
            _willExplosive = true;
    }

    public void Explode()
    {
        if (_willExplosive)
            Explosioner.Explode(this);
        else if(_rigidbodyReplicatedCubes.Count != 0)
            Explosioner.Explode(transform.position, _rigidbodyReplicatedCubes);
    }
}