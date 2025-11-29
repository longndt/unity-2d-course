# Theory: UI System & Complete 2D Game

## 🎯 Learning Objectives

After completing this lesson, students will be able to:
- Design and implement complete UI system for 2D games
- Create responsive menus and HUD elements
- Manage multiple scenes and game states
- Integrate audio system with sound effects and music
- Setup build process to publish games
- Apply game design principles for complete game experience

---

## 1. Unity UI System (uGUI) Overview

### 1.1 Canvas System

#### **Canvas Component**:
Canvas is the **root element** for all UI elements in Unity:

**Render Modes:**
- **Screen Space - Overlay**: UI rendered on top, not affected by camera
- **Screen Space - Camera**: UI rendered by specific camera, can be affected by camera effects
- **World Space**: UI elements in 3D world space

**Canvas Scaler**: Handles UI scaling across different screen resolutions

#### **Canvas Setup for 2D Games**:
```csharp
// Recommended settings for 2D games
Render Mode: Screen Space - Overlay
Canvas Scaler:
├── UI Scale Mode: Scale With Screen Size
├── Reference Resolution: 1920x1080 (or 1280x720)
├── Screen Match Mode: Match Width Or Height
└── Match: 0.5 (balanced scaling)
```

### 1.2 UI Element Hierarchy

#### **Typical UI Structure**:
```
Canvas (Screen Space - Overlay)
├── Main Menu Panel
│   ├── Title Image
│   ├── Play Button
│   ├── Settings Button
│   └── Quit Button
├── Game HUD Panel
│   ├── Health Bar
│   ├── Score Text
│   ├── Lives Counter
│   └── Pause Button
├── Pause Menu Panel
│   ├── Resume Button
│   ├── Main Menu Button
│   └── Quit Button
└── Game Over Panel
    ├── Final Score Text
    ├── Restart Button
    └── Main Menu Button
```

### 1.3 Event System

**EventSystem GameObject** handles UI input and interactions:
- **Standalone Input Module**: Keyboard/mouse input
- **Touch Input Module**: Touch/mobile input
- **Graphic Raycaster**: Detects UI element clicks

---

## 2. Core UI Components

### 2.1 Button Component

#### **Button Properties**:
- **Interactable**: Enable/disable button
- **Transition**: Visual feedback (Color Tint, Sprite Swap, Animation)
- **Navigation**: Keyboard/controller focus movement
- **OnClick()**: Event fired when button is pressed

#### **Button Hook**:

```csharp
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    void Awake()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnPlayClicked);
    }

    void OnPlayClicked()
    {
        SceneManager.LoadScene("GameScene");
    }
}
```


### 2.2 Text Component (TextMeshPro)

#### **TextMeshPro vs Legacy Text**:
- ✅ **TextMeshPro**: Better rendering, effects, performance
- ❌ **Legacy Text**: Deprecated, limited features

#### **Text Configuration**:
```csharp
using TMPro;

public class UIText : MonoBehaviour
{
    private TextMeshProUGUI textComponent;

    [Header("Text Settings")]
    public string defaultText = "Default Text";
    public Color defaultColor = Color.white;
    public float defaultFontSize = 24f;

    void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        InitializeText();
    }

    void InitializeText()
    {
        textComponent.text = defaultText;
        textComponent.color = defaultColor;
        textComponent.fontSize = defaultFontSize;
    }

    public void SetText(string newText)
    {
        textComponent.text = newText;
    }

    public void SetColor(Color newColor)
    {
        textComponent.color = newColor;
    }

    public void AnimateText(string newText)
    {
        StartCoroutine(TypewriterEffect(newText));
    }

    IEnumerator TypewriterEffect(string fullText)
    {
        textComponent.text = "";

        for (int i = 0; i < fullText.Length; i++)
        {
            textComponent.text += fullText[i];
            yield return new WaitForSeconds(0.05f);
        }
    }
}
```

### 2.3 Image Component

#### **Image Types**:
- **Simple**: Basic sprite display.  
- **Sliced**: 9-slice sprite for scalable UI.  
- **Tiled**: Repeating pattern.  
- **Filled**: Radial/linear fill (health bars, loading bars).

#### **Health Bar**:

```csharp
public class SimpleHealthBar : MonoBehaviour
{
    public Image healthFillImage;

    public void SetHealth01(float value01)
    {
        healthFillImage.fillAmount = Mathf.Clamp01(value01);
    }
}
```


### 2.4 Slider Component

Sliders are often used for **settings** (volume, brightness, sensitivity).

#### **Basic Volume Slider**:

```csharp
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider slider;

    void Awake()
    {
        slider.onValueChanged.AddListener(OnSliderChanged);
    }

    void OnSliderChanged(float value)
    {
        // Map 0–1 slider to your audio system
        AudioListener.volume = value;
    }
}
```


---

## 3. Menu System Design

### 3.1 Panel Management

In a complete game you typically have several panels:
- Main Menu, Settings, Pause, Game Over.

Each panel is a `GameObject` with its own Canvas/Panel. A manager shows/hides them.

Example:

```csharp
public class SimpleMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject pausePanel;

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void ShowPauseMenu()
    {
        mainMenuPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
}
```

More advanced patterns (base `UIPanel` class, fade animations, panel stacks) are implemented in the Lesson 5 sample project and shared UI utilities.

### 3.2 Main Menu and Pause Menu Implementation

Advanced menu system implementations with base UIPanel class, Main Menu Panel, and Pause Menu Panel are covered in:

📖 **[Advanced UI Systems](../../extras/advanced-ui-systems.md)**

---

## 4. HUD and Game UI

### 4.1 Game HUD System and Inventory

Advanced HUD Manager with animations and complete Inventory System are covered in:

📖 **[Advanced UI Systems](../../extras/advanced-ui-systems.md)**

---

## 5. Scene Management and Game State

Advanced scene transition system with fade effects and complete game state management system are covered in:

📖 **[Advanced Game Systems](../../extras/advanced-game-systems.md)**

---

## 6. Audio System Integration

Comprehensive Audio Manager with music, SFX, ambient sounds, volume control, and crossfading is covered in:

📖 **[Advanced Game Systems](../../extras/advanced-game-systems.md)**

---

## 7. Save System

Complete Save System with player data management, high score tracking, and settings persistence is covered in:

📖 **[Advanced Game Systems](../../extras/advanced-game-systems.md)**

---

## 8. Build and Deployment

Advanced Build Manager with build configuration, optimization settings, and performance monitoring is covered in:

📖 **[Advanced Game Systems](../../extras/advanced-game-systems.md)**

For performance optimization techniques, see:

📖 **[Performance Optimization](../../extras/performance-optimization.md)**

---

## Chapter Summary

### Core Knowledge:
1. ✅ **UI System (uGUI)**: Canvas, panels, and component hierarchy
2. ✅ **Menu Design**: Professional menu systems and navigation
3. ✅ **HUD Integration**: Real-time game interface elements
4. ✅ **Scene Management**: Smooth transitions and state handling
5. ✅ **Audio System**: Comprehensive sound and music integration
6. ✅ **Save System**: Player data persistence and settings

### Technical Skills Acquired:
- 🖼️ **Professional UI Design**: Complete menu and HUD systems
- 🎵 **Audio Integration**: Music, SFX, and ambient sound management
- 💾 **Data Persistence**: Save/load systems and player settings
- 🔄 **Scene Transitions**: Smooth loading and state management
- 📦 **Build Pipeline**: Game optimization and deployment preparation
- 🎮 **Complete 2D Game**: From concept to publishable product
- 🏆 **Game Polish**: Audio feedback, visual effects, and game feel
- ⚡ **Performance Optimization**: Efficient code and resource management
- 🚀 **Deployment Ready**: Build settings and distribution preparation

### Practice:
Complete **Lab 05** to create a fully functional 2D game with complete UI system, audio integration, save/load functionality, and build-ready optimization. This represents the culmination of the entire Unity 2D course!

---

## ✅ What's Next

You've completed the core Unity 2D course! Continue with advanced topics in `extras/` to further enhance your game development skills.