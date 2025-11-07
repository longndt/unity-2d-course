# Unity Course - LongNDT

## Course Overview

This course teaches 2D game development with Unity from the ground up. Perfect for beginners with web/mobile development experience who want to learn game development.

**Format**: Light theory, hands-on practice, and project-based learning

---

## Prerequisites

- **C# Programming**: Classes, methods, events, properties
- **Unity Hub**: Latest version for project management
- **Unity 6.2 (6000.2.10f1)**: Latest recommended version for this course
- **IDE**: Visual Studio 2022, VS Code, or JetBrains Rider
- **Git**: Version control (optional but recommended)
- **Helpful but not required**: Web/mobile development experience
- **Optional**: Basic understanding of physics and animation concepts

### **System Requirements**
- **Windows**: Windows 10/11 (64-bit), 8GB RAM, DX11 compatible GPU
- **macOS**: macOS 12+ (Monterey/Sonoma), 8GB RAM, Metal compatible GPU
- **Storage**: 25GB free space for Unity and projects

---

## What You Will Learn

- Build complete 2D game slices with Unity
- Understand GameObject/Component architecture and MonoBehaviour lifecycle
- Master the Input System (from basics to advanced patterns)
- Create 2D sprite workflows and animation state machines
- Implement physics-based mechanics and robust collisions
- Build professional camera systems and player controllers
- Design UI/HUD, menus, and gameplay state management
- Profile and optimize builds for target platforms

**Note**: This syllabus is aligned with Unity Learn best practices and follows a progressive learning path that introduces interactivity early, building complexity gradually.

---

## 📚 Course Structure & Learning Path

### **🎯 Learning Flow Overview**
```
Lesson 0 (Fundamentals) → Lesson 1 (Basics + Basic Input) → Lesson 2 (Sprites & Animation) →
Lesson 3 (Physics) → Lesson 4 (Advanced Input & Camera) → Lesson 5 (UI & Complete Game)
```

**Learning Progression:**
- **Early Interactivity**: Basic input and camera in Lesson 1 for immediate feedback
- **Visual Polish**: Sprites and animation in Lesson 2
- **Game Mechanics**: Physics and collision in Lesson 3 (using input from Lesson 1)
- **Professional Systems**: Advanced input patterns and camera systems in Lesson 4
- **Complete Experience**: UI, game systems, and build pipeline in Lesson 5

### **📖 Detailed Course Structure**

#### **Lesson 0: Game Development Fundamentals & Mindset**
- **🎯 Purpose**: Bridge from web/mobile to game development
- **📚 Topics**: Game design basics, player experience, Unity Editor overview
- **🎮 Project**: Simple "Hello World" bouncing ball game
- **🔗 Path**: `lesson0-game-development-fundamentals/`

#### **Lesson 1: Unity Fundamentals & Project Setup**
- **🎯 Purpose**: Master Unity's core architecture and basic interactivity
- **📚 Topics**: Editor, scenes, prefabs, MonoBehaviour lifecycle, basic Input System (keyboard), simple camera follow
- **🎮 Project**: Interactive scene with player movement and camera following
- **🔗 Path**: `lesson1-unity-basics/`

#### **Lesson 2: Sprites & Animation**
- **🎯 Purpose**: Master 2D visual systems and animation
- **📚 Topics**: Import pipeline, sorting layers, Animator Controller, animation events
- **🎮 Project**: Complete character animation system with idle/walk/jump/attack
- **🔗 Path**: `lesson2-sprites-animation/`

#### **Lesson 3: Physics & Collision**
- **🎯 Purpose**: Implement responsive physics mechanics
- **📚 Topics**: Rigidbody2D, colliders, materials, layers, FixedUpdate, raycast
- **🎮 Project**: 2D platformer with advanced jump mechanics (coyote time, variable jump)
- **🔗 Path**: `lesson3-physics-collision/`

#### **Lesson 4: Advanced Input & Camera Systems**
- **🎯 Purpose**: Master the Input System and professional camera setups
- **📚 Topics**: Input Actions, PlayerInput, rebinding, advanced camera systems (Cinemachine, bounds, smoothing), input handling patterns
- **🎮 Project**: Professional character controller with gamepad/keyboard support and cinematic camera
- **🔗 Path**: `lesson4-input-player-controller/`

#### **Lesson 5: UI, Gameplay Loop & Build**
- **🎯 Purpose**: Complete game development cycle with polished systems
- **📚 Topics**: Basic UI (health bars, text), advanced UGUI/UIToolkit, menus, pause system, save/load, build pipeline, game state management
- **🎮 Project**: Complete vertical slice from main menu → gameplay → results with full UI systems
- **🔗 Path**: `lesson5-ui-complete-game/`

---

## 🗺️ Navigation & Learning Flow

### **📋 How to Use This Course**

#### **🎯 For Complete Beginners:**
1. **Start Here**: `lesson0-game-development-fundamentals/` - Learn game development mindset
2. **Follow Sequence**: Complete lessons 0→1→2→3→4→5 in order
3. **Study Materials**: Read theory → Study examples → Complete labs
4. **Build Projects**: Use sample projects as reference and inspiration

#### **🎯 For Experienced Developers:**
1. **Skip to Basics**: `lesson1-unity-basics/` - Focus on Unity-specific concepts
2. **Jump Around**: Use lessons as reference for specific topics
3. **Sample Projects**: Study `sample-projects/` for complete implementations
4. **Quick Reference**: Use `extras/` for advanced topics and troubleshooting

#### **🎯 Learning Path Options:**
- **🎮 Project-First**: Start with `sample-projects/` → Study theory → Build your own
- **📚 Theory-First**: Read `theory/` → Study `example/` → Complete `lab/`
- **🔧 Problem-Solving**: Use `extras/troubleshooting-guide.md` → Find relevant lesson

---

## 📁 Lesson Structure

Each lesson follows a standardized structure designed for effective learning:

```
lesson-topic/
├──  reference/          # Quick reference codes & checklists
├──  example/            # Working code examples
├──  theory/             # Light documentation (single theoryX.md)
├──  lab/                # Hands-on playtest tasks
└──  quiz/               # Interactive quiz to test understanding
```

Note: All lesson folders now use the standardized `example/` structure. Each lesson includes an interactive quiz for self-assessment.

---

## 🗺️ Visual Diagrams

- Game Loop & Execution Order
- Animator Flow & State Machine
- Physics Update & Collision Matrix
- Input Flow (devices → actions → gameplay/UI)
- UI Navigation & Gameplay States
- Build Pipeline (targets, profiles, compression)

---

## 📚 How to Study This Course Effectively

### Step 1: Start with Reference
- Skim `reference/` to see APIs and checklists used in the lesson

### Step 2: Explore Working Example
- Open the lesson `example/` and run the sample scene
- Playtest first, then peek into scripts structure

### Step 3: Read Theory as Reference
- Read only the sections related to what you're implementing
- Jump between theory ↔ example ↔ reference

### Step 4: Code Along
- Recreate the example features in your own scene
- Test frequently and iterate in small steps

### Step 5: Complete Lab
- Follow playtest criteria to validate features (measurable outcomes)

### Step 6: Take the Quiz
- Complete the interactive quiz to test your understanding
- Review any concepts you missed

### Step 7: Review & Reflect
- Compare with example, note pitfalls, create personal cheat sheets

---

## 🚀 Getting Started

### Step 1: Check and Install Development Environment
- Read `extras/environment-setup.md` for Unity Hub, Unity 6.2, IDE, Git

### Step 2: Open the Project
- Open this folder in Unity Hub and launch with Unity 6.2 (6000.2.10f1)

### Step 3: Recommended Learning Path
- Lesson 0 → Lesson 1 → Lesson 2 → Lesson 3 → Lesson 4 → Lesson 5

---

## 📚 Course Resources

### **Learning Materials**
- **Learning Path**: `extras/learning-path.md` - Visual course journey
- **Environment Setup**: `extras/environment-setup.md` - Complete setup guide
- **Study Guide**: `extras/study-guide.md` - How to study effectively

### **Assessment & Quizzes**
- **Lesson 0 Quiz**: [`lesson0-game-development-fundamentals/quiz/quiz0.html`](lesson0-game-development-fundamentals/quiz/quiz0.html) - Test your understanding of game development fundamentals
- **Lesson 1 Quiz**: [`lesson1-unity-basics/quiz/quiz1.html`](lesson1-unity-basics/quiz/quiz1.html) - Assess your Unity basics knowledge
- **Lesson 2 Quiz**: [`lesson2-sprites-animation/quiz/quiz2.html`](lesson2-sprites-animation/quiz/quiz2.html) - Test sprites and animation concepts
- **Lesson 3 Quiz**: [`lesson3-physics-collision/quiz/quiz3.html`](lesson3-physics-collision/quiz/quiz3.html) - Evaluate physics and collision understanding
- **Lesson 4 Quiz**: [`lesson4-input-player-controller/quiz/quiz4.html`](lesson4-input-player-controller/quiz/quiz4.html) - Check input and camera systems knowledge
- **Lesson 5 Quiz**: [`lesson5-ui-complete-game/quiz/quiz5.html`](lesson5-ui-complete-game/quiz/quiz5.html) - Assess UI and complete game development skills

**💡 Tip**: Complete each quiz after finishing the corresponding lesson to reinforce your learning!

### **Code & Examples**
- **Common Scripts Library**: `extras/common-scripts-library.md` - Reusable code library
- **Free Assets Resources**: `extras/free-assets-resources.md` - Free asset sources and integration guide

### **Additional Resources**
- **Design Patterns**: `extras/design-patterns.md` - Common design patterns for game development
- **Performance Optimization**: `extras/performance-optimization.md` - Advanced optimization techniques
- **Troubleshooting Guide**: `extras/troubleshooting-guide.md` - Common issues and solutions
- **All Extras**: `extras/readme.md` - Complete resource overview

---

## 💡 Learning Tips

- Use **Markdown Preview Enhanced** for better reading experience
- Playtest early and often; optimize later
- Profile builds on the target device before shipping

---