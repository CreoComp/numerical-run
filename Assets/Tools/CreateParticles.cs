using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateParticles : MonoBehaviour
{
    public GameObject particles;
    public Transform pos;
    public Transform parent;

    [Space]
    public Color color = Color.white;
    public float emission = 200f;
    public float delay;
    //public float coolDown;

    [Space]
    public bool changeColor = false;
    public bool changeEmission = false;

    const int maxParticlesPerSecond = 15;
    private static int particlesCount = 0;
    private static float timer = 0;

    private void Start()
    {
        if (pos == null)
            pos = transform;
    }

    public void Create()
    {
        if (delay != 0)
            StartCoroutine(Delay());
        else
            InstantiateParticles(pos.position);
    }

    public void Create(Vector2 pos)
    {
        InstantiateParticles(pos);
    }

    public void Create(Vector2 pos, float emission)
    {
        var p = InstantiateParticles(pos);
        if (p != null)
            ChangeEmminsion(p, emission);
    }

    public void Create(float emission)
    {
        var p = InstantiateParticles(pos.position);
        if (p != null)
            ChangeEmminsion(p, emission);
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);

        InstantiateParticles(pos.position);
    }

    private ParticleSystem InstantiateParticles(Vector2 pos)
    {
        //print(timer + " " + particlesCount);
        if (particlesCount > maxParticlesPerSecond && Time.timeSinceLevelLoad < timer)
        {
            return null;
        }
        else if (Time.timeSinceLevelLoad > timer)
        {
            timer = Time.timeSinceLevelLoad + 1;
            particlesCount = 0;
        }

        GameObject gb = Instantiate(particles, parent);
        gb.transform.position = pos;
        Destroy(gb, 5f);

        var particlesSystem = gb.GetComponent<ParticleSystem>();
        if (particlesSystem == null)
            return null;

        gb.AddComponent<DestroyAfterTime>().time = particlesSystem.main.duration + particlesSystem.main.startLifetime.constantMax; ;

        if (changeColor)
        {
            var main = particlesSystem.main;
            main.startColor = color;
        }
        if (changeEmission)
        {
            var e = particlesSystem.emission;
            e.rateOverTime = new ParticleSystem.MinMaxCurve(emission);
        }

        particlesSystem.Play();
        particlesCount += 1;

        return particlesSystem;
    }

    private void ChangeEmminsion(ParticleSystem particlesSystem, float emission)
    {
        var e = particlesSystem.emission;
        e.rateOverTime = new ParticleSystem.MinMaxCurve(emission);
    }

    private void ChangeColorn(ParticleSystem particlesSystem, Color color)
    {
        var main = particlesSystem.main;
        main.startColor = color;
    }
}
