using UnityEngine;

public class BarSpawner : MonoBehaviour
{
    public GameObject barPrefab;  // The prefab for the individual bars
    public int numberOfBars = 64;  // The number of bars to create
    public float radius = 10.0f;  // Radius of the circle
    public float barHeight = 1.0f;  // Height of each bar
    public Transform centerPoint;  // Center point around which to place the bars (optional)

    void Awake()
    {
        // Loop to create and position the bars
        for (int i = 0; i < numberOfBars; i++)
        {
            // Calculate angle for each bar
            float angle = i * Mathf.PI * 2f / numberOfBars;

            // Calculate the position using trigonometry
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            Vector3 position = new Vector3(x, 0, z);  // Position of each bar around the center

            // Instantiate the bar at the calculated position
            GameObject bar = Instantiate(barPrefab, position, Quaternion.identity);

            // Rotate the bar to face the center
            bar.transform.LookAt(centerPoint != null ? centerPoint.position : Vector3.zero);

            // Optionally, set the bar height
            bar.transform.localScale = new Vector3(0.5f, barHeight, 0.5f);

            // Make the bar a child of this BarGroup
            bar.transform.parent = this.transform;
        }
    }
}

