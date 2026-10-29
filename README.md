# 360° VR Virtual Tour (Meta Quest / Unity)

A 360-degree panoramic virtual tour project developed in Unity for the **Meta Quest** VR headset using **OpenXR**, **Universal Render Pipeline (URP)**, and the **XR Interaction Toolkit**.

---

## 📌 Project Features

- **360° Panoramic Skybox Tour:** High-resolution 360° captures rendered seamlessly via panoramic skybox materials.
- **Cross-Platform Controls:**
  - **VR Controller:** Left/Right thumbstick navigation on the right Touch controller.
  - **Gaze / Stare Selection:** Look at floating 3D **NEXT** or **BACK** buttons for 1.5 seconds to switch scenes hands-free.
  - **PC / Editor Testing:** Keyboard keys (`A` / `D` or Left / Right arrows) and mouse clicks on interactive buttons.
- **Platform Support:**
  - **Standalone Android APK** for direct on-device execution (ARM64).
  - **PC VR / Quest Link** tethered mode for real-time Unity Editor playtesting.

---

## 🛠️ Tech Stack & Requirements

- **Unity Version:** Unity 6 (6000.0.x LTS)
- **Render Pipeline:** Universal Render Pipeline (URP 17.3.0)
- **XR SDK:** OpenXR Plugin (1.16.1) & XR Interaction Toolkit (3.3.2)
- **Input System:** Unity New Input System (1.19.0)
- **Target Platform:** Android (Meta Quest 2 / Quest 3 / Quest Pro / Quest 3S) & Windows PC (Oculus Link / Air Link)

---

## 📂 Project Structure

```text
├── Assets/
│   ├── Panaromas/               # 360° images (.JPG) and Skybox materials (.mat)
│   ├── Scenes/
│   │   └── SampleScene.unity    # Main VR scene with XR Origin and Panorama Manager
│   ├── Settings/                # URP render pipeline settings and volume profiles
│   ├── XR/                      # OpenXR settings and loader configurations
│   ├── XRI/                     # XR Interaction Toolkit settings
│   └── panaromaswitcher.cs      # Core tour controller script
├── Packages/
│   └── manifest.json            # Unity package dependencies
└── ProjectSettings/             # Unity project, quality, and platform build settings
```

---

## 🚀 How to Run & Test

### Option 1: In Unity Editor via Meta Quest Link (Tethered)
1. Connect your Meta Quest to your PC via USB-C cable or Air Link.
2. In the Meta Quest Link desktop app, ensure OpenXR is active under **Settings > General**.
3. In Unity, open `Edit > Project Settings > XR Plug-in Management`.
4. Under the **Windows / PC** tab, ensure **OpenXR** is checked.
5. Open `Assets/Scenes/SampleScene.unity` and click **Play (▶)**.

### Option 2: Standalone Meta Quest Build (Android APK)
1. In Unity, go to `File > Build Settings`.
2. Switch platform to **Android**.
3. Ensure Texture Compression is set to **ASTC** and Architecture is set to **ARM64**.
4. Click **Build and Run** (with Quest connected in Developer Mode) or install the generated `.apk` using SideQuest or ADB:
   ```bash
   adb install vr_project.apk
   ```

---

## 🎮 Controls Summary

| Action | VR Headset / Controller | Keyboard / Mouse |
| :--- | :--- | :--- |
| **Next Panorama** | Flick Right Thumbstick Right OR Gaze at "NEXT" button (1.5s) | `D` or `Right Arrow` / Click "NEXT" |
| **Previous Panorama** | Flick Right Thumbstick Left OR Gaze at "BACK" button (1.5s) | `A` or `Left Arrow` / Click "BACK" |
