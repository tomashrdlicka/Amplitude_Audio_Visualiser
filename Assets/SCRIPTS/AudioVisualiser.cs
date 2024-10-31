using UnityEngine;

public class AudioVisualizer : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject[] bars;
    public float[] audioSamples = new float[32];  // Use 32 samples for interpolation
    public float scaleMultiplier = 20.0f;

    float pulseFrequency = 174.0f / 60.0f;
    float waveSpeed = 0.5f;
    float depthScale = 1.0f;
    float colorChangeSpeed = 0.05f;
    float colorOscillationAmplitude = 0.1f;

    public Transform[] barTransforms;
    private Vector3[] initialPositions;
    private float smoothedColorIntensity;

    void Start()
    {
        Resources.UnloadUnusedAssets();
        System.GC.Collect();

        bars = new GameObject[transform.childCount];
        barTransforms = new Transform[bars.Length];
        initialPositions = new Vector3[bars.Length];

        for (int i = 0; i < bars.Length; i++)
        {
            bars[i] = transform.GetChild(i).gameObject;
            barTransforms[i] = bars[i].transform;
            initialPositions[i] = barTransforms[i].localPosition;
        }
    }

    void Update()
    {
        audioSource.GetOutputData(audioSamples, 0);

        float globalAmplitude = Mathf.Max(audioSamples);

        // Calculate a smoothed color intensity based on time and amplitude
        float targetColorIntensity = Mathf.Clamp01(globalAmplitude * 5);
        smoothedColorIntensity = Mathf.Lerp(smoothedColorIntensity, targetColorIntensity, colorChangeSpeed * Time.deltaTime);

        // Add oscillation based on pulse frequency to smoothedColorIntensity
        float colorOscillation = Mathf.Sin(Time.time * pulseFrequency * Mathf.PI * 2f) * colorOscillationAmplitude;
        float finalColorIntensity = Mathf.Clamp01(smoothedColorIntensity + colorOscillation);

        for (int i = 0; i < barTransforms.Length; i++)
        {
            Vector3 currentScale = barTransforms[i].localScale;

            // Determine which two audio samples to interpolate between
            int sampleIndex = i / 2;
            float sampleValue;

            // Interpolate if it's an odd bar index, or use the sample directly
            if (i % 2 == 0)
            {
                sampleValue = Mathf.Abs(audioSamples[sampleIndex]);
            }
            else
            {
                float nextSampleValue = Mathf.Abs(audioSamples[Mathf.Min(sampleIndex + 1, audioSamples.Length - 1)]);
                sampleValue = Mathf.Lerp(Mathf.Abs(audioSamples[sampleIndex]), nextSampleValue, 0.5f);  // Midpoint interpolation
            }

            // Apply cubic response for amplitude scaling
            float adjustedAmplitude = Mathf.Pow(sampleValue, 1.5f) * scaleMultiplier;
            float targetScaleY = Mathf.Max(adjustedAmplitude, 0.1f);

            currentScale.y = Mathf.Lerp(currentScale.y, targetScaleY, Time.deltaTime * 3.0f);
            barTransforms[i].localScale = currentScale;

            // Radial depth effect based on global amplitude
            Vector3 radialDirection = initialPositions[i].normalized;
            float globalOffset = Mathf.Sin(Time.time * waveSpeed) * globalAmplitude * depthScale;
            barTransforms[i].localPosition = initialPositions[i] + radialDirection * globalOffset;

            // Smooth color transition with added oscillation
            Renderer barRenderer = bars[i].GetComponent<Renderer>();
            Color finalColor = Color.Lerp(Color.blue, Color.yellow, finalColorIntensity);

            // Set lower emission intensity for smoother glow
            float emissionIntensity = Mathf.Lerp(0.3f, 1.0f, finalColorIntensity);
            barRenderer.material.SetColor("_EmissionColor", finalColor * emissionIntensity);
            barRenderer.sharedMaterial.color = finalColor;
        }

        // Rotate group based on average amplitude
        float averageAmplitude = 0f;
        foreach (float sample in audioSamples)
            averageAmplitude += Mathf.Abs(sample);
        averageAmplitude /= audioSamples.Length;

        float rotationSpeed = Mathf.Lerp(10f, 50f, averageAmplitude);
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}








