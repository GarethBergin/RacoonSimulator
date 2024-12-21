using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class FireflyLightL : MonoBehaviour
{
    public Light2D spotlight;
    public float minIntensity = 0.2f;
    public float maxIntensity = 0.6f;
    public float minOuterRadius = 1f;
    public float maxOuterRadius = 1.5f;
    public float minSpeed = 1f;
    public float maxSpeed = 3f;

    private float targetIntensity;
    private float targetOuterRadius;
    private float currentIntensity;
    private float currentOuterRadius;
    private float changeIntensitySpeed;
    private float changeRadiusSpeed;

    void Start()
    {
        InitializeTargetValues();
    }

    void Update()
    {
        if (Mathf.Abs(currentIntensity - targetIntensity) < 0.01f)
            InitializeTargetValues();

        currentIntensity = Mathf.MoveTowards(currentIntensity, targetIntensity, changeIntensitySpeed * Time.deltaTime);
        currentOuterRadius = Mathf.MoveTowards(currentOuterRadius, targetOuterRadius, changeRadiusSpeed * Time.deltaTime);

        spotlight.intensity = currentIntensity;
        spotlight.pointLightOuterRadius = currentOuterRadius;
    }

    void InitializeTargetValues()
    {
        targetIntensity = Random.Range(minIntensity, maxIntensity);
        targetOuterRadius = Random.Range(minOuterRadius, maxOuterRadius);
        changeIntensitySpeed = Random.Range(minSpeed, maxSpeed);
        changeRadiusSpeed = Random.Range(minSpeed, maxSpeed);
    }
}
