# Unity Course Sample Projects

## 🎯 Overview

This directory contains **complete sample projects** for each lesson, providing working examples that students can study, modify, and learn from. Each project demonstrates the concepts taught in the corresponding lesson.

### **📚 Relationship to Lesson Examples**
- **Lesson Examples**: Individual scripts and concepts in `lessonX-topic/example/`
- **Sample Projects**: Complete integrated projects with all concepts working together
- **Learning Path**: Study individual examples first, then explore complete sample projects

## 📁 Available Projects

### **Lesson 0: Game Development Fundamentals**
- **Project**: `lesson0-bouncing-ball/` (Advanced version)
- **Description**: Enhanced bouncing ball game with paddle control and complete game mechanics
- **Features**: Physics, collision detection, paddle control, game loop, audio, effects
- **Difficulty**: Beginner-Intermediate
- **Note**: Builds upon basic example in `lesson0-game-development-fundamentals/example/`

### **Lesson 1: Unity Basics**
- **Project**: `lesson1-unity-basics/`
- **Description**: Complete scene management system with prefabs and lifecycle demonstration
- **Features**: Multiple scenes, prefab instantiation, MonoBehaviour lifecycle, debug tools
- **Difficulty**: Beginner
- **Note**: Integrates concepts from `lesson1-unity-basics/example/`

### **Lesson 2: Sprites & Animation**
- **Project**: `lesson2-sprites-animation/`
- **Description**: Complete character animation system with sprite management
- **Features**: Animator Controller, sprite sheets, animation events, character controller
- **Difficulty**: Intermediate
- **Note**: Integrates concepts from `lesson2-sprites-animation/example/`

### **Lesson 3: Physics & Collision**
- **Project**: `lesson3-physics-collision/`
- **Description**: Complete 2D platformer with advanced physics mechanics
- **Features**: Rigidbody2D, collision detection, jump mechanics, physics materials, moving platforms
- **Difficulty**: Intermediate
- **Note**: Integrates concepts from `lesson3-physics-collision/example/`

### **Lesson 4: Input & Player Controller**
- **Project**: `lesson4-input-player-controller/`
- **Description**: Complete character controller with Input System and camera following
- **Features**: New Input System, camera follow, advanced movement, input feedback
- **Difficulty**: Intermediate
- **Note**: Integrates concepts from `lesson4-input-player-controller/example/`

### **Lesson 5: UI & Complete Game**
- **Project**: `lesson5-ui-complete-game/`
- **Description**: Complete 2D game with full UI system, audio, and build process
- **Features**: Complete game loop, menus, HUD, save system, build settings, game state management
- **Difficulty**: Advanced
- **Note**: Integrates concepts from `lesson5-ui-complete-game/example/`

## 🚀 How to Use Sample Projects

### **Method 1: Study and Learn**
1. **Download** the project for your current lesson
2. **Open** in Unity Editor
3. **Explore** the code and scene structure
4. **Play** the game to understand functionality
5. **Modify** code to see how changes affect behavior

### **Method 2: Use as Starting Point**
1. **Copy** the project folder
2. **Rename** to your own project name
3. **Modify** and extend the existing code
4. **Add** your own features and improvements

### **Method 3: Reference Implementation**
1. **Compare** your implementation with the sample
2. **Identify** differences and improvements
3. **Learn** from the sample's architecture
4. **Apply** best practices to your own code

## 📋 Project Requirements

### **Unity Version**
- **Unity 6.2 (6000.2.10f1)** (recommended)
- **Unity 2022.3 LTS** (minimum, for compatibility)
- **2D (URP) Template** for all projects

### **Required Packages**
- **Input System** (for Lesson 4+)
- **2D Sprite** (for all projects)
- **Cinemachine** (for Lesson 4+)

### **System Requirements**
- **Windows 10/11** or **macOS 12+**
- **8GB RAM** minimum
- **DirectX 11** compatible graphics

## 🎮 Project Descriptions

### **Lesson 0: Bouncing Ball Game**
A simple physics-based game where players control a paddle to keep a ball bouncing. Perfect for understanding Unity's basic concepts.

**Key Learning Points**:
- GameObject and Component system
- Physics simulation
- Collision detection
- Basic game loop

**Controls**:
- **A/D**: Move paddle left/right
- **Space**: Launch ball

### **Lesson 1: Scene Management Demo**
Demonstrates Unity's scene management system with multiple scenes and prefab usage.

**Key Learning Points**:
- Scene loading and switching
- Prefab creation and usage
- MonoBehaviour lifecycle
- Component communication

**Features**:
- Main menu scene
- Game scene
- Settings scene
- Prefab instantiation

### **Lesson 2: Character Animation**
A character that can walk, jump, and perform various animations using Unity's animation system.

**Key Learning Points**:
- Sprite import and configuration
- Animation window usage
- Animator Controller setup
- Animation events

**Features**:
- Idle, walk, jump animations
- Sprite flipping
- Animation state machine
- Event-driven animations

### **Lesson 3: Physics Platformer**
A complete 2D platformer with realistic physics, collision detection, and advanced jump mechanics.

**Key Learning Points**:
- 2D physics system
- Collision detection
- Physics materials
- Advanced movement mechanics

**Features**:
- Player movement and jumping
- Platform collision
- Collectible items
- Physics-based interactions

### **Lesson 4: Input System Demo**
Demonstrates Unity's New Input System with responsive character controls and camera following.

**Key Learning Points**:
- Input Actions configuration
- Player Input component
- Event-driven input handling
- Camera systems

**Features**:
- Keyboard and gamepad support
- Smooth camera following
- Input rebinding
- Responsive controls

### **Lesson 5: Complete 2D Game**
A full-featured 2D game with menus, gameplay, UI, audio, and build process.

**Key Learning Points**:
- Complete game development
- UI system implementation
- Audio integration
- Build and deployment

**Features**:
- Main menu and settings
- Gameplay with scoring
- Pause and game over screens
- Audio system
- Save/load functionality

## 🔧 Setup Instructions

### **1. Download Projects**
1. **Clone** or **download** this repository
2. **Navigate** to the `sample-projects/` folder
3. **Choose** the project for your current lesson

### **2. Open in Unity**
1. **Launch** Unity Hub
2. **Click** "Add" and select the project folder
3. **Open** the project in Unity Editor
4. **Wait** for Unity to import all assets

### **3. Verify Setup**
1. **Check** Console for any errors
2. **Verify** all required packages are installed
3. **Test** the project in Play mode
4. **Read** the project's README for specific instructions

## 📚 Learning Path

### **Beginner (Lessons 0-1)**
Start with **Lesson 0** to understand basic concepts, then move to **Lesson 1** for Unity fundamentals.

### **Intermediate (Lessons 2-3)**
Use **Lesson 2** to learn animation, then **Lesson 3** for physics and collision.

### **Advanced (Lessons 4-5)**
Study **Lesson 4** for input systems, then **Lesson 5** for complete game development.

## 🐛 Troubleshooting

### **Common Issues**

#### **Project won't open**
- Check Unity version compatibility
- Verify all files downloaded correctly
- Try opening from Unity Hub

#### **Missing packages**
- Install required packages from Package Manager
- Check project settings
- Restart Unity Editor

#### **Compilation errors**
- Check Console for specific errors
- Verify all scripts are in correct folders
- Reimport all assets

#### **Input not working**
- Check Input System package is installed
- Verify Input Actions are configured
- Test with different input devices

### **Getting Help**
1. **Check** the project's README file
2. **Review** the main course materials
3. **Ask** in the course discussion forum
4. **Contact** the instructor for assistance

## 📝 Contributing

### **Improving Projects**
If you find bugs or have suggestions for improvements:
1. **Document** the issue clearly
2. **Provide** steps to reproduce
3. **Suggest** potential solutions
4. **Submit** feedback through the course platform

### **Adding Features**
Feel free to extend the sample projects with your own features:
1. **Create** a new branch
2. **Add** your improvements
3. **Test** thoroughly
4. **Share** with the community

---

**Happy Learning!** These sample projects are designed to help you master Unity 2D game development. Take your time to explore, experiment, and learn from each project.
