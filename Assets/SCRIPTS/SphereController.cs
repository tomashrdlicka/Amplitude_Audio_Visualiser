using UnityEngine;

public class PulsingBackground : MonoBehaviour
{
    public AudioSource audioSource;  // Reference to the audio source
    public GameObject backgroundObject;  // The background object (e.g., a sphere)
    public float[] audioSamples = new float[64];  // Audio samples array
    public float emissionMultiplier = 5.0f;  // Multiplier for emission strength
    public float smoothingSpeed = 2.0f;  // Speed for smoothing the transition

    private Renderer backgroundRenderer;  // Renderer to control material properties
    private Color originalEmissionColor;  // Store the original emission color
    private float currentEmissionStrength = 0f;  // Store the current smoothed emission strength

    void Start()
    {
        // Get the renderer of the background object
        backgroundRenderer = backgroundObject.GetComponent<Renderer>();

        // Enable emission in the material if necessary
        backgroundRenderer.material.EnableKeyword("_EMISSION");

        // Store the original emission color
        originalEmissionColor = backgroundRenderer.material.GetColor("_EmissionColor");
    }

    void Update()
    {
        // Get audio data
        audioSource.GetOutputData(audioSamples, 0);

        // Calculate overall amplitude from the audio samples
        float totalAmplitude = 0f;
        for (int i = 0; i < audioSamples.Length; i++)
        {
            totalAmplitude += Mathf.Abs(audioSamples[i]);
        }

        // Calculate target emission strength based on audio amplitude
        float targetEmissionStrength = Mathf.Clamp(totalAmplitude * emissionMultiplier, 0f, 5f);

        // Smooth the transition between current and target emission strength
        currentEmissionStrength = Mathf.Lerp(currentEmissionStrength, targetEmissionStrength, Time.deltaTime * smoothingSpeed);

        // Set the emission intensity without changing the original color
        backgroundRenderer.material.SetColor("_EmissionColor", originalEmissionColor * currentEmissionStrength);

        // Adjust dynamic texture scaling based on the audio amplitude
        float textureTiling = Mathf.Lerp(1.0f, 5.0f, currentEmissionStrength);  // Adjust texture scale dynamically
        backgroundRenderer.material.SetTextureScale("_MainTex", new Vector2(textureTiling, textureTiling));

        // Adjust dynamic texture offset to create a moving effect
        float textureOffset = Time.time * 0.1f * currentEmissionStrength;  // Create a scroll effect
        backgroundRenderer.material.SetTextureOffset("_MainTex", new Vector2(textureOffset, textureOffset));
    }
}

