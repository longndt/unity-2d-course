# Unity Editor Interface Overview

## 🎯 Learning Objectives

After reading this guide, you will be able to:
- Identify all major Unity Editor windows and panels
- Understand the purpose of each interface element
- Navigate the Unity Editor confidently
- Customize the interface layout for 2D development

---

## 📱 Unity Editor Layout

### Main Interface Components

```mermaid
flowchart TB
    subgraph Top["Unity Editor Interface"]
        MenuBar[Menu Bar<br/>File, Edit, Assets, GameObject, Component, etc.]
        Toolbar[Toolbar<br/>Play, Pause, Step, Layers, Layout]
        
        subgraph MainArea["Main Work Area"]
            SceneView[Scene View<br/>Level Editor<br/>2D Scene Editor]
            GameView[Game View<br/>Player View<br/>Game Preview Window]
            Inspector[Inspector<br/>Properties<br/>Component Properties]
        end
        
        subgraph Bottom["Bottom Panels"]
            Hierarchy[Hierarchy<br/>Scene Objects<br/>Main Camera<br/>Directional Light]
            Project["Project<br/>Asset Files<br/>Assets/<br/>Scenes/<br/>Scripts/<br/>Sprites/<br/>Prefabs/"]
            Console[Console<br/>Debug Messages<br/>Messages<br/>Warnings<br/>Errors]
        end
    end
    
    MenuBar --> Toolbar
    Toolbar --> MainArea
    MainArea --> Bottom
    
    style MenuBar fill:#e1f5ff
    style Toolbar fill:#fff4e1
    style SceneView fill:#e1ffe1
    style GameView fill:#f0e1ff
    style Inspector fill:#ffe1e1
    style Hierarchy fill:#e1f5ff
    style Project fill:#fff4e1
    style Console fill:#ffe1e1
```

<img src="../images/unity-6.2/editor-layout-overview.png" alt="Editor Layout Overview" width="900" />

---

## 🎮 Key Windows Explained

### 1. Scene View (Top Left)
**Purpose**: 3D/2D world editor where you design levels and arrange objects

**Key Features**:
- **2D Mode Toggle**: Switch between 2D and 3D editing
- **Gizmos**: Visual helpers for object manipulation
- **Grid**: Snap objects to grid for alignment
- **Toolbar**: Move, rotate, scale, and rect tools

**For 2D Games**:
- Always keep in 2D mode
- Use orthographic view
- Focus on X and Y axes (ignore Z)

<img src="../images/unity-6.2/scene-view-2d-toggle.png" alt="Scene View - 2D Toggle" width="900" />

### 2. Game View (Top Right)
**Purpose**: Shows what the player will see when the game runs

**Key Features**:
- **Play Mode**: Shows real-time game preview
- **Aspect Ratio**: Test different screen sizes
- **Stats**: Performance information
- **Gizmos Toggle**: Show/hide editor gizmos

**Important**: This is what players see, not the Scene view!

<img src="../images/unity-6.2/game-view-aspect-stats.png" alt="Game View - Aspect Ratio & Stats" width="900" />

### 3. Hierarchy (Bottom Left)
**Purpose**: Lists all GameObjects in the current scene

**Key Features**:
- **Parent-Child Relationships**: Nested object structure
- **Search**: Find objects quickly
- **Context Menu**: Right-click for options
- **Selection**: Click to select objects

**For 2D Games**:
- Organize by layers (Background, Characters, UI)
- Use empty GameObjects as containers

### 4. Project (Bottom Center)
**Purpose**: File browser for all project assets

**Key Features**:
- **Asset Organization**: Folders for different asset types
- **Import Settings**: Configure how assets are imported
- **Search**: Find assets by name or type
- **Preview**: See asset thumbnails

**Best Practice**: Keep assets organized in folders!

### 5. Inspector (Right Side)
**Purpose**: Shows properties and components of selected objects

**Key Features**:
- **Component List**: All attached components
- **Properties**: Editable values and settings
- **Add Component**: Attach new components
- **Script Variables**: Public variables from scripts

**For 2D Games**:
- Transform (Position, Rotation, Scale)
- Sprite Renderer (Visual appearance)
- Collider 2D (Physics collision)
- Rigidbody 2D (Physics behavior)

<img src="../images/unity-6.2/inspector-components.png" alt="Inspector - Components Example" width="900" />

### 6. Console (Bottom Right)
**Purpose**: Shows debug messages, warnings, and errors

**Key Features**:
- **Messages**: Debug.Log() output
- **Warnings**: Non-critical issues
- **Errors**: Critical problems that prevent compilation
- **Clear**: Remove old messages

**Important**: Always check Console for errors!

---

## 🛠️ Essential Toolbar

### Play Controls
- **▶️ Play**: Start/stop game simulation
- **⏸️ Pause**: Pause game simulation
- **⏭️ Step**: Advance one frame at a time

### Transform Tools
- **✋ Move Tool**: Move objects (Q key)
- **🔄 Rotate Tool**: Rotate objects (E key)
- **📏 Scale Tool**: Scale objects (R key)
- **📐 Rect Tool**: Move/scale UI elements (T key)

### 2D-Specific Tools
- **2D Mode**: Toggle 2D/3D editing
- **Grid Snapping**: Snap to grid
- **Pivot/Center**: Object pivot point

---

## ⌨️ Essential Keyboard Shortcuts

### Navigation
- **F**: Focus on selected object
- **Alt + Left Click**: Orbit around object
- **Mouse Wheel**: Zoom in/out
- **Right Click + Drag**: Pan view

### Object Manipulation
- **W**: Move tool
- **E**: Rotate tool
- **R**: Scale tool
- **T**: Rect tool
- **Q**: Selection tool

### Scene Management
- **Ctrl + S**: Save scene
- **Ctrl + Shift + S**: Save all
- **Ctrl + N**: New scene
- **Ctrl + O**: Open scene

### Play Mode
- **Space**: Play/Pause
- **Ctrl + P**: Play/Pause
- **Ctrl + Shift + P**: Pause
- **Ctrl + Alt + P**: Step

---

## 🎯 2D Development Layout

### Recommended Layout for 2D Games

1. **Scene View**: Large, on the left
2. **Game View**: Medium, top right
3. **Hierarchy**: Medium, bottom left
4. **Project**: Medium, bottom center
5. **Inspector**: Narrow, right side
6. **Console**: Small, bottom right

### Layout Customization

1. **Window → Layouts → Save Layout**
2. **Name**: "2D Game Development"
3. **Save**: Your custom layout

---

## 🔧 Interface Customization

### Window Management
- **Drag**: Move windows around
- **Tab**: Dock windows together
- **Float**: Make windows independent
- **Maximize**: Expand windows to full size

### Layout Presets
- **Default**: Standard Unity layout
- **2 by 3**: Good for 2D development
- **4 Split**: Multiple views
- **Tall**: Vertical layout

### Personalization
- **Preferences**: Edit → Preferences
- **Colors**: Customize interface colors
- **Fonts**: Adjust text size
- **Themes**: Light/Dark mode

---

## 🚨 Common Interface Issues

### Problem: Can't see objects in Scene view
**Solution**:
1. Check if 2D mode is enabled
2. Press F to focus on selected object
3. Reset camera position (double-click in Hierarchy)

### Problem: Inspector shows "Missing (MonoScript)"
**Solution**:
1. Check Console for compilation errors
2. Fix script errors
3. Reimport scripts

### Problem: Game view is black/empty
**Solution**:
1. Check camera position
2. Ensure objects are in camera view
3. Check camera settings (orthographic for 2D)

### Problem: Can't find specific window
**Solution**:
1. Use Window menu
2. Check if window is docked elsewhere
3. Reset layout: Window → Layouts → Default

---

## 📚 Next Steps

1. **Practice Navigation**: Explore the interface
2. **Try Shortcuts**: Use keyboard shortcuts instead of clicking
3. **Customize Layout**: Create your preferred layout
4. **Read Next Guide**: [Project Setup Guide](../project-setup/2d-project-setup.md)

---
