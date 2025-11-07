# Lesson 3: Physics & Collision

## Overview

**Difficulty**: Intermediate
**Prerequisites**: Lesson 2 completed, Basic C# programming

Master Rigidbody2D, colliders, physics materials, layers, and callbacks. Implement platformer jumping with coyote time and variable jump height.

---

## 🎯 Learning Objectives

- Configure `Rigidbody2D` and colliders for stable physics
- Understand `FixedUpdate` vs `Update`, time step, and interpolation
- Use collision layers/masks and raycasts responsibly
- Implement jump mechanics and ground checks
- Differentiate triggers vs collisions and handle callbacks

---

## 🚀 Quick Start

1. Create a scene `Lesson3-Physics`
2. Add player with `Rigidbody2D` + collider; add ground colliders
3. Add scripts from `example/`: `RigidbodyControl.cs`, `ColliderSetup.cs`, `PhysicsMaterialSetup.cs`, `PlayerJump.cs`, `AdvancedJump.cs`, `TriggerDetection.cs`, `ForceControl.cs`
4. Tune gravity, drag, material bounciness; test jump behavior

---

## 📚 Learning Path

- Reference → `reference/` (physics checklist)
- Example → `example/` (playtest and tune values)
- Theory → `theory/theory3.md`
- Lab → `lab/lab3-instructions.md`
- Quiz → [`quiz/quiz3.html`](quiz/quiz3.html) (test your understanding)

---

## ✅ What's Next

Proceed to [Lesson 4: Advanced Input & Camera Systems](../lesson4-input-player-controller/) to learn about the New Input System and professional camera setups. You already know basic input and camera follow from Lesson 1, so this lesson will build on that foundation.

---

## 🚀 **Next Steps**

- **Study Examples**: Start with `example/` folder to understand concepts
- **Practice Labs**: Complete exercises in `lab/` folder
- **Take the Quiz**: Test your knowledge with [`quiz/quiz3.html`](quiz/quiz3.html)
- **Explore Sample Project**: Check out `sample-projects/lesson3-physics-collision/` for complete implementation
- **Build Your Own**: Create your own physics-based platformer

---

## Resources & References

- Rigidbody2D and Colliders
- Physics Update Order and Fixed Timestep
- Layer-based collision matrix


