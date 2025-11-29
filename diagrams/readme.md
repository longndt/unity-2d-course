# 📊 Unity Course Diagrams

## 🎯 Overview

This directory contains visual diagrams and flowcharts that illustrate key concepts in Unity game development. These diagrams help visualize complex relationships, workflows, and architectural patterns specific to game development.

**Note**: All diagrams use **Mermaid** format for clear, interactive visualizations. Mermaid diagrams are supported by most Markdown viewers including GitHub, GitLab, VS Code, and many documentation tools.

## 📚 Available Diagrams

### **[game_loop.md](./game_loop.md)**
Unity's execution order and game loop flow:
- **MonoBehaviour Lifecycle** - Awake, Start, Update, FixedUpdate, LateUpdate
- **Execution Order** - How Unity processes GameObjects and components
- **Frame Timing** - Update vs FixedUpdate vs LateUpdate timing
- **Physics Steps** - Fixed timestep and physics calculations
- **Rendering Pipeline** - How objects are rendered each frame

**Best for**: Understanding Unity's core execution model and when to use different update methods

---

### **[animation_flow.md](./animation_flow.md)**
Animation system workflow and state machine flow:
- **Animator Controller** - States, transitions, and parameters
- **Animation Events** - How events trigger during animation playback
- **Blend Trees** - Complex animation blending
- **State Machine Logic** - Transition conditions and flow
- **Performance Considerations** - Animation optimization tips

**Best for**: Understanding Unity's animation system and creating smooth character animations

---

### **[physics_update_order.md](./physics_update_order.md)**
Physics system execution and collision detection flow:
- **Fixed Timestep** - Physics update frequency and timing
- **Collision Detection** - How Unity detects and processes collisions
- **Rigidbody2D/3D** - Physics body behavior and constraints
- **Collision Matrix** - Layer-based collision filtering
- **Performance Tips** - Physics optimization strategies

**Best for**: Understanding Unity's physics system and optimizing collision performance

---

### **[input_flow.md](./input_flow.md)**
Input system architecture and event flow:
- **Input Actions** - Action maps, actions, and bindings
- **Event Flow** - From device input to gameplay response
- **PlayerInput Component** - How input events are processed
- **UI vs Gameplay** - Input context switching
- **Rebinding System** - Runtime input customization

**Best for**: Understanding Unity's Input System and implementing responsive controls

---

### **[ui_navigation_flow.md](./ui_navigation_flow.md)**
UI system and menu navigation flow:
- **Canvas Hierarchy** - UI element organization and rendering
- **Event System** - How UI events are processed
- **Menu Navigation** - Keyboard/gamepad navigation patterns
- **State Management** - UI state transitions and persistence
- **Performance Optimization** - UI rendering and interaction optimization

**Best for**: Understanding Unity's UI system and creating accessible menu systems

---

### **[build_pipeline.md](./build_pipeline.md)**
Complete build process and deployment flow:
- **Build Settings** - Platform configuration and scene management
- **Asset Processing** - How assets are prepared for different platforms
- **Platform-Specific Settings** - Android, iOS, Windows, macOS, WebGL configurations
- **Build Optimization** - Size reduction and performance optimization
- **Deployment Process** - From build to distribution

**Best for**: Understanding Unity's build system and preparing games for release

---

## 🎯 How to Use These Diagrams

### **Learning Path**
1. **Start with Game Loop** - Understand Unity's core execution model
2. **Follow Lesson Progression** - Use diagrams as you progress through lessons
3. **Reference During Practice** - Use diagrams while coding and debugging
4. **Review for Understanding** - Revisit diagrams to reinforce concepts

### **Best Practices**
- **Read Before Coding** - Review relevant diagrams before starting exercises
- **Reference During Debugging** - Use diagrams to understand flow issues
- **Share with Team** - Use diagrams for code reviews and discussions
- **Update as You Learn** - Add your own notes and modifications

---

## 🔗 Related Resources

- **[Theory Lessons](../lesson*/theory/)** - Detailed explanations of concepts
- **[Lab Exercises](../lesson*/lab/)** - Hands-on practice with concepts
- **[Reference Guides](../lesson*/reference/)** - Quick reference materials
- **[Advanced Patterns](../extras/design-patterns.md)** - Advanced architectural patterns

These diagrams are regularly updated to reflect:
- **Unity Version Changes** - New features and deprecated functionality
- **Best Practice Evolution** - Industry standard changes
- **Course Content Updates** - New lessons and examples
- **Community Feedback** - Improvements based on user input

---

**Ready to explore?** Start with the [game loop](./game_loop.md) to understand Unity's core execution model!
