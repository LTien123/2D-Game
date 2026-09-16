using Unity.Mathematics;
using UnityEngine;

public class BounceEffectController : MonoBehaviour
{

    [SerializeField] private ParticleSystem particleSystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(float impactIntensity)
    {
        float clamIntensity = Mathf.Clamp(impactIntensity, 0.05f, 0.2f);

        if (particleSystem != null ) { 
            var main = particleSystem.main;
            main.startSizeMultiplier = clamIntensity;
            main.startSpeedMultiplier = clamIntensity + 0.5f;
        }

        Destroy(gameObject, 1f);
    }
}
