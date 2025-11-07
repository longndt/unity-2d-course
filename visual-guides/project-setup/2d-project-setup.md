# 2D Project Setup - Complete Guide

## 🎯 Learning Objectives

After following this guide, you will be able to:
- Create a new 2D Unity project correctly
- Configure project settings for 2D development
- Set up the optimal layout for 2D game development
- Understand all essential 2D project settings

---

## 🚀 Step 1: Creating a New 2D Project

### 1.1 Open Unity Hub
1. Launch **Unity Hub** from your desktop or Start menu
2. Click on the **"Projects"** tab
3. Click **"New project"** button

<img src="../images/unity-6.2/hub-new-project.png" alt="Unity Hub - New Project" width="900" />

### 1.2 Select 2D Template
1. In the **"New project"** window, select **"2D (URP)"** template
   - **Why URP?**: Universal Render Pipeline provides better 2D performance
   - **Alternative**: "2D" template (older, but still functional)

<img src="../images/unity-6.2/hub-template-2d-urp.png" alt="Select 2D (URP) Template" width="900" />

### 1.3 Configure Project Settings
1. **Project name**: Enter a descriptive name (e.g., "My2DGame")
2. **Location**: Choose where to save your project
   - **Recommended**: Create a dedicated "Unity Projects" folder
3. **Version**: Ensure Unity 6.2 (6000.2.10f1) is selected
4. Click **"Create project"**

<img src="../images/unity-6.2/hub-select-version-6000.2.10f1.png" alt="Select Version 6000.2.10f1" width="900" />

<img src="../images/unity-6.2/hub-create-project.png" alt="Create Project Button" width="900" />

### 1.4 Wait for Project Creation
- Unity will create the project and open the editor
- This process varies depending on your system
- **Don't close Unity Hub** during this process

---

## ⚙️ Step 2: Essential Project Settings

### 2.1 Open Project Settings
1. Go to **Edit → Project Settings**
2. This opens the Project Settings window

### 2.2 Player Settings (Essential)
Navigate to **XR Plug-in Management → Player**:

#### **Company Name**
- Set your company/developer name
- This appears in build settings

#### **Product Name**
- Set your game's name
- This appears in the final build

#### **Version**
- Set version number (e.g., "1.0.0")
- Increment for updates

#### **Icon**
- Set your game icon
- Recommended: 1024x1024 PNG

### 2.3 Graphics Settings (2D Optimization)
Navigate to **XR Plug-in Management → Graphics**:

#### **Color Space**
- **Linear**: Better color accuracy (recommended)
- **Gamma**: Faster, older standard

#### **Rendering Path**
- **Forward**: Standard for 2D games
- **Deferred**: Not needed for 2D

#### **Anti Aliasing**
- **Disabled**: For pixel art games
- **2x/4x/8x**: For smooth graphics

### 2.4 Physics 2D Settings
Navigate to **XR Plug-in Management → Physics 2D**:

#### **Gravity**
- **X**: 0 (no horizontal gravity)
- **Y**: -9.81 (standard gravity)
- **Adjust**: Based on your game feel

#### **Default Material**
- **None**: Use default physics
- **Custom**: Create Physics Material 2D

#### **Velocity Iterations**
- **8**: Standard value
- **Higher**: More accurate physics (slower)

#### **Position Iterations**
- **3**: Standard value
- **Higher**: Better collision resolution (slower)

---

## 🎨 Step 3: 2D-Specific Configuration

### 3.1 Camera Setup
1. Select **Main Camera** in Hierarchy
2. In Inspector, configure:

#### **Camera Component**
- **Projection**: Orthographic
- **Size**: 5 (adjust based on your game scale)
- **Near Clipping Plane**: 0.3
- **Far Clipping Plane**: 1000

#### **Transform Component**
- **Position**: (0, 0, -10)
- **Rotation**: (0, 0, 0)
- **Scale**: (1, 1, 1)

### 3.2 Scene View Configuration
1. In **Scene View**, click the **2D** button
2. Ensure **2D mode** is enabled
3. Set **Scene View** to **Orthographic**

### 3.3 Grid Settings
1. In **Scene View**, click the **Grid** button
2. Right-click **Grid** button for settings:
   - **Grid Size**: 1 (adjust for your game scale)
   - **Grid Opacity**: 0.5
   - **Show Grid**: Enabled

---

## 📁 Step 4: Project Organization

### 4.1 Create Essential Folders
In **Project** window, create these folders:

```
Assets/
├── Scenes/           # Game scenes
├── Scripts/          # C# scripts
├── Sprites/          # 2D images
├── Animations/       # Animation files
├── Prefabs/          # Reusable objects
├── Materials/        # Physics materials
├── Audio/            # Sound effects and music
├── UI/               # User interface elements
└── Settings/         # Configuration files
```

### 4.2 Create Initial Scene
1. **File → New Scene**
2. Choose **2D** template
3. **File → Save Scene As**
4. Save as "MainMenu" in **Scenes** folder

---

## 🎮 Step 5: Input System Setup

### 5.1 Install Input System Package
1. **Window → Package Manager**
2. **Unity Registry** → Search "Input System"
3. **Install** the package
4. **Restart Unity** when prompted

### 5.2 Configure Input Settings
1. **Edit → Project Settings**
2. **XR Plug-in Management → Player**
3. **Configuration → Active Input Handling**
4. Select **"Input System Package (New)"**

### 5.3 Create Input Actions Asset
1. **Assets → Create → Input Actions**
2. Name it "PlayerInputActions"
3. Save in **Settings** folder

---

## 🎯 Step 6: Build Settings

### 6.1 Add Scenes to Build
1. **File → Build Settings**
2. **Add Open Scenes** (add your current scene)
3. **Drag scenes** from Project to Build Settings
4. **Order matters**: First scene is the starting scene

### 6.2 Platform Settings
1. **Select Platform** (PC, Mac & Linux Standalone)
2. **Player Settings** button
3. Configure platform-specific settings

---

## 🔧 Step 7: Quality Settings

### 7.1 Open Quality Settings
1. **Edit → Project Settings**
2. **XR Plug-in Management → Quality**

### 7.2 2D Game Quality Preset
Create a custom quality preset:

#### **Rendering**
- **Texture Quality**: Full Res
- **Anisotropic Textures**: Disabled
- **Anti Aliasing**: Disabled (for pixel art)
- **Soft Particles**: Disabled
- **Realtime Reflection Probes**: Disabled

#### **Shadows**
- **Shadow Resolution**: Low
- **Shadow Distance**: 0 (2D games don't need shadows)

#### **Other**
- **Pixel Light Count**: 0
- **VSync Count**: Don't Sync (for development)

---

## 📊 Step 8: Performance Settings

### 8.1 Time Settings
1. **Edit → Project Settings**
2. **XR Plug-in Management → Time**

#### **Fixed Timestep**
- **0.02**: 50 FPS physics (standard)
- **0.016667**: 60 FPS physics (smoother)

#### **Maximum Allowed Timestep**
- **0.1**: Prevents physics slowdown

### 8.2 Physics 2D Optimization
1. **Edit → Project Settings**
2. **XR Plug-in Management → Physics 2D**

#### **Layer Collision Matrix**
- Configure which layers can collide
- Disable unnecessary collisions for performance

---

## ✅ Step 9: Verification Checklist

### Project Structure
- [ ] 2D (URP) template selected
- [ ] Project name and location set
- [ ] Essential folders created
- [ ] Main scene saved

### Settings Configuration
- [ ] Player settings configured
- [ ] Physics 2D settings adjusted
- [ ] Camera set to Orthographic
- [ ] 2D mode enabled in Scene view

### Input System
- [ ] Input System package installed
- [ ] Input Actions asset created
- [ ] Input handling set to "Input System Package (New)"

### Build Settings
- [ ] Scenes added to build
- [ ] Platform selected
- [ ] Quality settings optimized for 2D

---

## 🚨 Common Setup Issues

### Problem: Project won't open
**Solutions**:
1. Check Unity version compatibility
2. Verify project location is accessible
3. Try opening from Unity Hub

### Problem: 2D mode not working
**Solutions**:
1. Enable 2D mode in Scene view
2. Set camera to Orthographic
3. Check if 2D template was selected

### Problem: Input System errors
**Solutions**:
1. Install Input System package
2. Set Active Input Handling correctly
3. Restart Unity after installation

### Problem: Build fails
**Solutions**:
1. Check for compilation errors
2. Verify scenes are in Build Settings
3. Check platform-specific settings

---

## 📚 Next Steps

1. **Test Your Setup**: Create a simple 2D sprite and test movement
2. **Explore Interface**: Get comfortable with Unity Editor
3. **Read Next Guide**: [Input System Setup Guide](../component-setup/input-system-setup.md)
4. **Start Lesson 1**: Begin with Unity fundamentals

---

## 💡 Pro Tips

### Development Workflow
- **Save frequently**: Ctrl+S for scene, Ctrl+Shift+S for project
- **Use version control**: Git for project backup
- **Test regularly**: Play mode to check functionality
- **Organize assets**: Keep project structure clean

### Performance Tips
- **Optimize sprites**: Use appropriate import settings
- **Limit draw calls**: Use sprite atlases
- **Profile regularly**: Use Unity Profiler
- **Test on target device**: Don't just test in editor

---

