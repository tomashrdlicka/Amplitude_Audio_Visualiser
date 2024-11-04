# Amplitude Visualizer Project

This Unity project is an audio-driven visualizer consisting of dynamic visual elements that respond to audio input. The project includes bars that respond to audio amplitudes, a rotating and scaling pulsing background, and a controller for adjusting visual properties based on the audio spectrum.
The project is intended to be displayed on a VR headset. Meta Quest 2 was used to deploy it.
Below is a breakdown of the functionality for each main script:

## Project Overview

The project aims to create a visually immersive experience using the following components:
- **Bars arranged in a circle** that scale and change color according to the audio amplitude.
- **A pulsing background** that adjusts its emission and texture properties based on audio data.
- **Rotational and depth-based effects** to enhance visual interest.

![Game Mode Showcase of Project](showcase1.gif)


## Code Breakdown

### 1. **BarSpawner.cs**

This script is responsible for instantiating bars in a circular arrangement around a central point.

#### Key Features:
- **Bar Creation**: Instantiates a specified number of bars (`numberOfBars`) and positions them in a circle around a center point.
- **Rotation**: Each bar is rotated to face the center of the circle.
- **Bar Height**: Configurable bar height for visual consistency.

#### Code Overview:
- **`Awake()` Method**:
  - Calculates positions using trigonometric functions (`Mathf.Cos` and `Mathf.Sin`) to create a circle layout.
  - Instantiates bars and sets their rotation to face the center.
  - Adjusts the bar scale and makes each bar a child of the `BarSpawner` GameObject for easy management.

### 2. **AudioVisualizer.cs**

This script controls the bar scale, color, and movement based on audio data, creating an animated response to the audio.

#### Key Features:
- **Audio Sampling**: Uses `GetOutputData()` to get raw audio data for analysis.
- **Bar Scaling**: Scales each bar vertically based on the audio amplitude, with interpolation between sample points for smoother transitions.
- **Radial Depth Effect**: Adjusts bar positions to create a depth effect based on the global amplitude.
- **Color and Emission**: Smooth color transitions from blue to yellow, with emission intensity linked to audio amplitude.
- **Group Rotation**: Rotates the entire group of bars based on the average audio amplitude.

#### Code Overview:
- **`Update()` Method**:
  - Retrieves audio data and calculates amplitude.
  - Smoothly adjusts bar scale and color using `Mathf.Lerp()` for visual fluidity.
  - Applies depth and rotation effects to enhance the immersive experience.

### 3. **PulsingBackground.cs**

This script manages the pulsing and dynamic texture effects of the background based on audio amplitude.

#### Key Features:
- **Emission Control**: Adjusts the emission color and intensity based on audio amplitude for a glow effect.
- **Dynamic Texture Scaling**: Scales and offsets the texture dynamically to create a pulsing and moving effect.
- **Smooth Transitions**: Uses `Mathf.Lerp()` for smooth transitions of emission strength to avoid abrupt changes.

#### Code Overview:
- **`Start()` Method**:
  - Initializes the background renderer and ensures emission is enabled.
  - Stores the original emission color for future adjustments.
- **`Update()` Method**:
  - Retrieves audio data and calculates overall amplitude.
  - Smoothly transitions the emission strength and adjusts the texture scale and offset to create motion.
   

## Script Parameters

### **BarSpawner.cs**
- `barPrefab`: The prefab for individual bars.
- `numberOfBars`: Number of bars to create.
- `radius`: The radius of the circular arrangement.
- `barHeight`: The initial height of each bar.
- `centerPoint`: The point around which bars are placed (optional).

### **AudioVisualizer.cs**
- `audioSource`: The audio source providing the audio data.
- `bars`: The array of bars controlled by the script.
- `scaleMultiplier`: Multiplier for scaling bar height.
- `pulseFrequency`, `waveSpeed`, `depthScale`: Parameters for controlling depth effects.
- `colorChangeSpeed`, `colorOscillationAmplitude`: Control color transitions.

### **PulsingBackground.cs**
- `audioSource`: The audio source providing audio data.
- `backgroundObject`: The GameObject for the background (e.g., a sphere).
- `emissionMultiplier`: Controls the strength of the emission.
- `smoothingSpeed`: Speed for smoothing the emission changes.
- `audioSamples`: Array size for capturing audio data.


## How to Use This Project

1. **Clone or download the repository** from GitHub.
2. **Open the project in Unity** (compatible with Unity version 2020.3+).
3. **Add your audio source**:
   - Drag a song of choice into the MUSIC folder.
   - Click on the AudioObject in Hierarchy view.
   - Replace the AudioClip in Inspector view with your song.
4. **Run on a VR headset**:
   - This project was configured to run on Quest 2, reconfiguration might be needed for other headsets.
   - If using Quest 2, connect the device (in Developer mode) via cable to your computer
   - Go to File tab -> Build & Run
   - Run project on headset

##Song Credit
Reference song used for project construction is Sweet Disposition - Fitch (BIG UP!)


## License

This project is available under the [MIT License](LICENSE), making it free to use and modify.
