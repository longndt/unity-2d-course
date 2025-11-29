# Group Project Guidelines - Lesson 5

## 🎯 Overview

Lesson 5 is the perfect opportunity to work on a **group project** and build a complete 2D game as a team. This guide provides best practices, workflows, and tips for successful collaboration in Unity game development.

---

## 👥 Team Formation & Roles

### Recommended Team Size
- **2-3 members**: Ideal for learning and communication
- **4-5 members**: Good for larger projects, requires more coordination
- **6+ members**: Only for advanced teams with clear structure

### Suggested Roles

#### **Role 1: Game Designer / Project Manager**
- **Responsibilities**:
  - Define game concept and mechanics
  - Create game design document (GDD)
  - Manage project timeline and milestones
  - Coordinate team communication
- **Skills needed**: Game design thinking, organization, communication

#### **Role 2: Programmer / Technical Lead**
- **Responsibilities**:
  - Set up project structure and Git repository
  - Implement core game systems
  - Code review and quality control
  - Technical decision making
- **Skills needed**: C# programming, Unity architecture, problem-solving

#### **Role 3: UI/UX Designer**
- **Responsibilities**:
  - Design and implement UI systems
  - Create menu flows and HUD layouts
  - Ensure consistent visual style
  - User experience testing
- **Skills needed**: UI design, Unity UI system, user experience thinking

#### **Role 4: Artist / Visual Designer** (Optional)
- **Responsibilities**:
  - Create or source game assets (sprites, animations)
  - Maintain visual consistency
  - Optimize assets for performance
- **Skills needed**: Art skills, Unity asset pipeline knowledge

#### **Role 5: Audio Designer** (Optional)
- **Responsibilities**:
  - Source or create sound effects
  - Implement audio system
  - Background music selection
- **Skills needed**: Audio editing, Unity Audio system

### Flexible Role Assignment
- **Small teams (2-3)**: Members can take multiple roles
- **Rotate responsibilities**: Everyone should try different aspects
- **Play to strengths**: But also learn new skills

---

## 📋 Project Planning

### Step 1: Define Project Scope

#### **Scope Guidelines**
- **Timeline**: 2-4 weeks for Lesson 5 group project
- **Complexity**: Should be achievable but challenging
- **Features**: Focus on core gameplay loop, not too many features

#### **Project Scope Template**
```
Project Name: [Your Game Name]
Team Members: [List names and roles]

Core Features (Must Have):
- [ ] Player movement and controls
- [ ] Basic gameplay mechanic
- [ ] Main menu and game over screen
- [ ] Score/Progress system
- [ ] Basic audio

Nice-to-Have Features:
- [ ] Multiple levels
- [ ] Power-ups
- [ ] Particle effects
- [ ] Advanced UI animations

Out of Scope (Future):
- [ ] Multiplayer
- [ ] Advanced AI
- [ ] Complex save system
```

### Step 2: Create Game Design Document (GDD)

#### **Simple GDD Template**
```markdown
# [Game Name] - Game Design Document

## 1. Game Concept
- **Genre**: [Platformer, Puzzle, Action, etc.]
- **Target Audience**: [Who will play this?]
- **Core Mechanic**: [What makes it fun?]
- **Unique Selling Point**: [What's special about it?]

## 2. Gameplay
- **Player Actions**: [What can the player do?]
- **Win Condition**: [How do you win?]
- **Lose Condition**: [How do you lose?]
- **Progression**: [How does difficulty increase?]

## 3. Controls
- **Input Method**: [Keyboard, Gamepad, Touch]
- **Control Scheme**: [WASD, Arrow Keys, etc.]

## 4. Visual Style
- **Art Style**: [Pixel art, Hand-drawn, Minimalist, etc.]
- **Color Palette**: [Main colors]
- **Mood**: [Happy, Dark, Mysterious, etc.]

## 5. Audio
- **Music Style**: [Upbeat, Ambient, etc.]
- **Sound Effects**: [List needed SFX]

## 6. Technical Requirements
- **Unity Version**: 6.2 (6000.2.10f1)
- **Target Platform**: [Windows, Mac, WebGL]
- **Performance Goals**: [60 FPS, etc.]
```

### Step 3: Break Down into Tasks

#### **Task Breakdown Template**
```
## Sprint 1: Foundation (Week 1)
- [ ] Set up Git repository
- [ ] Create project structure
- [ ] Implement player controller
- [ ] Create basic level
- [ ] Set up UI framework

## Sprint 2: Core Gameplay (Week 2)
- [ ] Implement core mechanic
- [ ] Create game loop
- [ ] Add scoring system
- [ ] Implement win/lose conditions

## Sprint 3: Polish (Week 3)
- [ ] Add audio
- [ ] Polish UI
- [ ] Add visual effects
- [ ] Bug fixing
- [ ] Build and test
```

---

## 🔄 Collaboration Workflow

### Version Control Setup (Git)

#### **Initial Setup**
```bash
# 1. One team member creates repository
git init
git remote add origin [repository-url]

# 2. Create .gitignore for Unity
# (Use Unity's default .gitignore)

# 3. Initial commit
git add .
git commit -m "Initial project setup"
git push -u origin main
```

#### **Branch Strategy**
```
main branch (production-ready code)
  ├── develop branch (integration branch)
      ├── feature/player-controller
      ├── feature/ui-system
      ├── feature/audio-system
      └── bugfix/collision-issue
```

#### **Git Workflow**
1. **Create feature branch:**
   ```bash
   git checkout -b feature/your-feature-name
   ```

2. **Work on your feature:**
   - Make commits frequently
   - Write clear commit messages

3. **Push and create Pull Request:**
   ```bash
   git push origin feature/your-feature-name
   ```

4. **Code Review:**
   - Team reviews the code
   - Discuss changes
   - Merge when approved

#### **Commit Message Best Practices**
```
✅ Good commit messages:
- "Add player jump mechanic with coyote time"
- "Fix collision detection bug in platform edges"
- "Implement main menu UI with button navigation"

❌ Bad commit messages:
- "Update"
- "Fixed stuff"
- "Changes"
```

### Unity-Specific Collaboration

#### **Scene Management**
- **Use scene variants** for different team members' work
- **Prefab everything** that might be reused
- **Avoid conflicts** by working on different scenes when possible

#### **Script Organization**
```
Assets/
├── Scripts/
│   ├── Player/
│   │   ├── PlayerController.cs (Team Member A)
│   │   └── PlayerHealth.cs (Team Member A)
│   ├── UI/
│   │   ├── MenuManager.cs (Team Member B)
│   │   └── HUDManager.cs (Team Member B)
│   └── Game/
│       ├── GameManager.cs (Team Member C)
│       └── ScoreManager.cs (Team Member C)
```

#### **Asset Organization**
```
Assets/
├── Sprites/
│   ├── Player/ (Team Member A)
│   ├── Environment/ (Team Member B)
│   └── UI/ (Team Member C)
├── Audio/
│   ├── Music/ (Team Member D)
│   └── SFX/ (Team Member D)
└── Prefabs/
    ├── Player.prefab (Team Member A)
    └── UI/ (Team Member B)
```

### Communication Best Practices

#### **Daily Standups** (Even for small teams)
- **What did you do yesterday?**
- **What will you do today?**
- **Any blockers?**

#### **Communication Channels**
- **Slack/Discord**: For quick questions and updates
- **GitHub Issues**: For bug tracking and feature requests
- **Weekly Meetings**: For planning and reviews

#### **Documentation**
- **Keep README updated** with setup instructions
- **Document design decisions** in code comments
- **Update GDD** as game evolves

---

## 🛠️ Technical Best Practices

### Code Standards

#### **Naming Conventions**
```csharp
// Classes: PascalCase
public class PlayerController : MonoBehaviour { }

// Methods: PascalCase
public void Jump() { }

// Variables: camelCase
private float jumpForce = 10f;

// Constants: UPPER_SNAKE_CASE
private const float MAX_SPEED = 20f;

// Private fields: camelCase with underscore prefix (optional)
private float _currentHealth;
```

#### **Code Organization**
```csharp
public class PlayerController : MonoBehaviour
{
    // 1. Serialized Fields
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    
    // 2. Private Fields
    private Rigidbody2D _rb;
    private bool _isGrounded;
    
    // 3. Properties
    public bool IsGrounded => _isGrounded;
    
    // 4. Unity Lifecycle Methods
    void Awake() { }
    void Start() { }
    void Update() { }
    
    // 5. Public Methods
    public void Jump() { }
    
    // 6. Private Methods
    private void CheckGrounded() { }
}
```

### Prefab Best Practices

#### **Always Use Prefabs**
- **GameObjects** that appear multiple times → Prefab
- **UI elements** that are reused → Prefab
- **Enemies, collectibles** → Prefab

#### **Prefab Variants**
- Use **Prefab Variants** for slight variations
- Example: Different colored enemies from same base prefab

### Scene Organization

#### **Scene Structure**
```
Scene Hierarchy:
├── Managers (Empty GameObject)
│   ├── GameManager
│   ├── AudioManager
│   └── UIManager
├── Player
│   └── Player (Prefab)
├── Environment
│   ├── Platforms
│   └── Background
└── UI
    └── Canvas
```

---

## 🐛 Conflict Resolution

### Common Unity Merge Conflicts

#### **Scene File Conflicts**
- **Problem**: Multiple people editing same scene
- **Solution**: 
  - Work on different scenes when possible
  - Use **Unity Collaborate** or **Plastic SCM** for better scene merging
  - Communicate before editing shared scenes

#### **Prefab Conflicts**
- **Problem**: Multiple people editing same prefab
- **Solution**:
  - Assign prefab ownership to one person
  - Use prefab variants for experimentation
  - Merge carefully, test after merge

#### **Script Conflicts**
- **Problem**: Git merge conflicts in C# files
- **Solution**:
  - Use proper merge tools
  - Test thoroughly after resolving conflicts
  - Have team member review the resolution

### Conflict Prevention

1. **Assign clear ownership** of files/scenes
2. **Communicate before major changes**
3. **Use branches** for experimental features
4. **Merge frequently** to avoid large conflicts
5. **Test after every merge**

---

## 📊 Project Management

### Task Tracking

#### **Simple Task Board** (Can use GitHub Projects, Trello, or Notion)
```
To Do          | In Progress    | Review        | Done
---------------|----------------|---------------|----------
Player jump    | UI system      | Audio setup   | Git setup
Enemy AI       | Level design   |               | Player move
```

### Milestones

#### **Suggested Milestones**
1. **Week 1 End**: Core gameplay working
2. **Week 2 End**: All features implemented
3. **Week 3 End**: Polish and bug fixes
4. **Week 4 End**: Final build and presentation

### Progress Reviews

#### **Weekly Review Checklist**
- [ ] All assigned tasks completed?
- [ ] Any blockers or issues?
- [ ] Code reviewed and merged?
- [ ] Game builds and runs?
- [ ] Next week's tasks assigned?

---

## ✅ Quality Assurance

### Testing Checklist

#### **Before Merging Code**
- [ ] Code compiles without errors
- [ ] No console warnings (or justified warnings)
- [ ] Feature works as intended
- [ ] Doesn't break existing features
- [ ] Code follows team standards
- [ ] Comments added for complex logic

### Playtesting

#### **Regular Playtesting Sessions**
- **Daily**: Quick 5-minute playtests
- **Weekly**: Full game playthrough
- **Before Release**: Comprehensive testing

#### **Playtest Feedback Template**
```
Feature: [Feature Name]
Tester: [Name]
Date: [Date]

What Worked:
- 

Issues Found:
- 

Suggestions:
- 
```

---

## 🎯 Success Criteria

### Project Completion Checklist

#### **Technical Requirements**
- [ ] Game builds successfully
- [ ] No critical bugs
- [ ] Runs at target framerate (60 FPS)
- [ ] All features implemented
- [ ] Code is clean and documented

#### **Team Requirements**
- [ ] All team members contributed
- [ ] Code reviewed by at least one other member
- [ ] Git history shows collaboration
- [ ] Documentation is complete

#### **Presentation Requirements**
- [ ] Game demo ready
- [ ] Team can explain design decisions
- [ ] Can discuss challenges and solutions
- [ ] Future improvements identified

---

## 💡 Tips for Success

### Communication
- **Over-communicate** rather than under-communicate
- **Ask questions** early, don't wait
- **Share progress** regularly
- **Celebrate small wins** together

### Time Management
- **Break tasks into small chunks** (1-2 hours each)
- **Set realistic deadlines**
- **Buffer time** for unexpected issues
- **Regular check-ins** to stay on track

### Learning
- **Help each other** learn new concepts
- **Code review** is a learning opportunity
- **Document what you learn**
- **Share resources** you find useful

### Problem Solving
- **Don't get stuck** on one problem for too long
- **Ask for help** after 30 minutes of trying
- **Use pair programming** for difficult problems
- **Take breaks** when frustrated

---

## 📚 Resources

### Version Control
- **Git Basics**: See `extras/version-control-guide.md`
- **Unity Git Setup**: [Unity Git Guide](https://docs.unity3d.com/Manual/UnityCloudBuildGit.html)
- **GitHub Desktop**: User-friendly Git GUI

### Collaboration Tools
- **GitHub**: Code hosting and collaboration
- **Discord/Slack**: Team communication
- **Trello/Notion**: Project management
- **Figma**: UI/UX design collaboration

### Unity Collaboration
- **Unity Collaborate**: Built-in collaboration (deprecated, use Git)
- **Plastic SCM**: Unity's recommended version control
- **Unity Cloud Build**: Automated builds

---

## 🎓 Learning Outcomes

By completing a group project, you will learn:
- ✅ **Version control** in real project context
- ✅ **Code collaboration** and review
- ✅ **Project management** basics
- ✅ **Team communication** skills
- ✅ **Problem-solving** in team environment
- ✅ **Professional workflows** used in industry

---

## 🚀 Next Steps

1. **Form your team** (2-5 members)
2. **Define project scope** using templates above
3. **Set up Git repository** (see `extras/version-control-guide.md`)
4. **Create game design document**
5. **Break down into tasks** and assign
6. **Start coding!** Remember to commit frequently
7. **Review each other's code** regularly
8. **Test and iterate** together
9. **Polish and prepare** for presentation
10. **Celebrate your completed game!** 🎉

---

**Remember**: The goal is not just to build a game, but to learn how to work effectively in a team. Communication, organization, and collaboration are just as important as coding skills!

