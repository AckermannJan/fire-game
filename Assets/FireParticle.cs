using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireParticle : MonoBehaviour {
    [Range(0.0f, 1.0f)]
    public float progress;
    public ParticleSystem []fire;
    public ParticleSystem []smoke;
    public ParticleSystem []whiteSmoke;

    private float maxAlpha = 1f;
    private float fireAlpha = 0f;
    private float fireTime = 0f;
    private float smokeAlpha = 0f;
    private float smokeTime = 0f;
    private float whiteSmokeAlpha = 0f;
    private float whiteSmokeTime = 0f;

    // Start is called before the first frame update
    void Start() {
        ParticleSystem ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update() {
        // White smoke
        if((progress > 0f && progress < .2f) || (progress > .8f && progress < .9f)) {
            whiteSmokeAlpha = Mathf.Lerp(0f, 1f, whiteSmokeTime);
            whiteSmokeTime = Mathf.Clamp(whiteSmokeTime + Time.deltaTime, 0f, 1f);
        } else {
            whiteSmokeAlpha = Mathf.Lerp(1f, 0f, 1);
            whiteSmokeTime = Mathf.Clamp(whiteSmokeTime - Time.deltaTime, 0f, 1f);
        }

        // Smoke
        if((progress > .2f && progress < 0.4f)) {
            smokeAlpha = Mathf.Lerp(0f, 1f, smokeTime);
            smokeTime = Mathf.Clamp(smokeTime + Time.deltaTime, 0f, 1f);
        } else {
            smokeAlpha = Mathf.Lerp(1f, 0f, 1);
            smokeTime = Mathf.Clamp(smokeTime - Time.deltaTime, 0f, 1f);
        }

        // Fire
        if(progress > .4f && progress < 0.8f) {
            fireAlpha = Mathf.Lerp(0f, 1f, fireTime);
            fireTime = Mathf.Clamp(fireTime + Time.deltaTime, 0f, 1f);
        } else {
            fireAlpha = Mathf.Lerp(1f, 0f, 1);
            fireTime = Mathf.Clamp(fireTime - Time.deltaTime, 0f, 1f);
        }


        foreach (ParticleSystem particle in fire) {
            ParticleSystem.MainModule _main = particle.main;
            _main.startColor = new ParticleSystem.MinMaxGradient(new Color(_main.startColor.color.r, _main.startColor.color.g, _main.startColor.color.b, fireAlpha));
        }

        foreach (ParticleSystem particle in smoke) {
            ParticleSystem.MainModule _main = particle.main;
            _main.startColor = new ParticleSystem.MinMaxGradient(new Color(_main.startColor.color.r, _main.startColor.color.g, _main.startColor.color.b, smokeAlpha));
        }

        foreach (ParticleSystem particle in whiteSmoke) {
            ParticleSystem.MainModule _main = particle.main;
            _main.startColor = new ParticleSystem.MinMaxGradient(new Color(_main.startColor.color.r, _main.startColor.color.g, _main.startColor.color.b, whiteSmokeAlpha));
        }
    }
}
