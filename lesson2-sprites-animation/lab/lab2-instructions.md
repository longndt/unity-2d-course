# Lab 2: Sprites & Animation

## 🎯 Learning Objectives

- Master Unity's 2D sprite system and import settings
- Learn the complete animation workflow from import to playback
- Understand Animator Controller and state machines
- Create character animations with proper transitions
- Implement animation events and curves

## 🎮 Playtest Criteria

**Complete when you can:**
- [ ] Import and configure sprites correctly
- [ ] Create smooth character animations
- [ ] Set up Animator Controller with states and transitions
- [ ] Use animation events and curves effectively
- [ ] Optimize sprite rendering for performance

---

## 🚀 Quick Start

### Step 1: Prepare Your Project

1. **Open your Unity project** from Lesson 1
2. **Create new scene**: `File → New Scene → 2D`
3. **Save scene** as "AnimationLab"
4. **Create folder structure**:
   - `Assets/Sprites/`
   - `Assets/Animations/`
   - `Assets/Animators/`

### Step 2: Import Sample Sprites

1. **Download sample sprite sheets** from the course resources
2. **Import sprites** into `Assets/Sprites/` folder
3. **Configure import settings** for each sprite sheet
4. **Test sprite rendering** in Scene view

---

## 🎯 Lab Tasks

### Task 1: Sprite Import and Configuration

#### **1.1 Import Sprite Sheets**

**Import Character Sprite Sheet:**
1. **Download** character sprite sheet (e.g., "CharacterSpriteSheet.png")
2. **Drag file** into `Assets/Sprites/` folder
3. **Select the imported sprite** in Project window
4. **In Inspector**, configure import settings:
   - **Texture Type**: Sprite (2D and UI)
   - **Sprite Mode**: Multiple
   - **Pixels Per Unit**: 32 (adjust based on your game scale)
   - **Filter Mode**: Point (for pixel art) or Bilinear (for smooth art)
   - **Compression**: None (for pixel art) or High Quality (for smooth art)

**Import Environment Sprites:**
1. **Download** environment sprite sheet (e.g., "EnvironmentSpriteSheet.png")
2. **Drag file** into `Assets/Sprites/` folder
3. **Configure import settings** similar to character sprites
4. **Adjust Pixels Per Unit** if needed for proper scaling

#### **1.2 Slice Sprite Sheets**

**Open Sprite Editor:**
1. **Select character sprite sheet** in Project window
2. **Click "Sprite Editor"** button in Inspector
3. **Sprite Editor window** opens

**Slice Sprites:**
1. **Click "Slice"** button in Sprite Editor
2. **Choose slicing method**:
   - **Automatic**: Unity detects sprite boundaries
   - **Grid By Cell Size**: Specify cell dimensions
   - **Grid By Cell Count**: Specify number of cells
3. **Configure slice settings**:
   - **Cell Size**: 32x32 (adjust based on your sprites)
   - **Pivot**: Center
   - **Method**: Delete Existing
4. **Click "Slice"** to create individual sprites
5. **Click "Apply"** to save changes

**Verify Slicing:**
1. **Check Project window** for individual sprites
2. **Select each sprite** to verify properties
3. **Test in Scene view** by dragging sprites

#### **1.3 Configure Sprite Properties**

**Set Sprite Properties:**
1. **Select a character sprite** in Project window
2. **In Inspector**, configure:
   - **Sprite**: Verify correct sprite is selected
   - **Color**: White (no tinting)
   - **Material**: Default Sprite Material
   - **Sorting Layer**: Default
   - **Order in Layer**: 0

**Create Sorting Layers:**
1. **Edit → Project Settings → Tags and Layers**
2. **Add Sorting Layers**:
   - Background (Order: 0)
   - Characters (Order: 1)
   - Foreground (Order: 2)
   - UI (Order: 3)

**Test Sprite Rendering:**
1. **Create GameObject** with Sprite Renderer
2. **Assign character sprite** to Sprite Renderer
3. **Set Sorting Layer** to "Characters"
4. **Test in Scene view** and Game view

### Task 2: Basic Animation Creation

#### **2.1 Create Animation Clips**

**Create Idle Animation:**
1. **Select character GameObject** in Hierarchy
2. **Window → Animation → Animation** (opens Animation window)
3. **Click "Create"** button in Animation window
4. **Save animation** as "CharacterIdle" in `Assets/Animations/`
5. **Animation window** shows timeline

**Add Keyframes:**
1. **Ensure GameObject is selected** and Animation window is open
2. **Click "Add Property"** button
3. **Select "Sprite Renderer.sprite"**
4. **Timeline shows** sprite property
5. **Add keyframes** at different times:
   - **Frame 0**: Idle sprite 1
   - **Frame 8**: Idle sprite 2
   - **Frame 16**: Idle sprite 3
   - **Frame 24**: Idle sprite 4
6. **Set animation length** to 1 second (30 frames at 30 FPS)

**Create Walk Animation:**
1. **Create new animation clip** "CharacterWalk"
2. **Add keyframes** for walk cycle:
   - **Frame 0**: Walk sprite 1
   - **Frame 4**: Walk sprite 2
   - **Frame 8**: Walk sprite 3
   - **Frame 12**: Walk sprite 4
   - **Frame 16**: Walk sprite 5
   - **Frame 20**: Walk sprite 6
3. **Set animation length** to 0.67 seconds (20 frames at 30 FPS)

**Create Jump Animation:**
1. **Create new animation clip** "CharacterJump"
2. **Add keyframes** for jump cycle:
   - **Frame 0**: Jump sprite 1
   - **Frame 8**: Jump sprite 2
   - **Frame 16**: Jump sprite 3
3. **Set animation length** to 0.53 seconds (16 frames at 30 FPS)

#### **2.2 Test Animations**

**Play Animations:**
1. **Select character GameObject** in Hierarchy
2. **In Animation window**, click **Play** button
3. **Watch animation** play in Scene view
4. **Test each animation clip** individually

**Adjust Animation Settings:**
1. **In Animation window**, click **Settings** button
2. **Configure**:
   - **Sample Rate**: 30 (frames per second)
   - **Root Transform Position**: Bake Into Pose
   - **Root Transform Rotation**: Bake Into Pose
   - **Root Transform Scale**: Bake Into Pose

### Task 3: Animator Controller Setup

#### **3.1 Create Animator Controller**

**Create Controller:**
1. **Right-click in Project** → **Create** → **Animator Controller**
2. **Name it** "CharacterAnimator"
3. **Save in** `Assets/Animators/` folder
4. **Double-click** to open Animator window

**Add Animation States:**
1. **In Animator window**, right-click in empty space
2. **Create State** → **From New Clip**
3. **Select "CharacterIdle"** animation clip
4. **Rename state** to "Idle"
5. **Repeat** for "Walk" and "Jump" states

#### **3.2 Set Default State**

**Configure Default State:**
1. **Right-click "Idle" state** in Animator
2. **Select "Set as Layer Default State"**
3. **Idle state** becomes orange (default state)
4. **Other states** remain gray

#### **3.3 Create Transitions**

**Idle to Walk Transition:**
1. **Right-click "Idle" state** → **Make Transition**
2. **Click on "Walk" state** to create transition
3. **Select the transition arrow** between states
4. **In Inspector**, configure:
   - **Has Exit Time**: Unchecked
   - **Transition Duration**: 0.1
   - **Transition Offset**: 0

**Walk to Idle Transition:**
1. **Right-click "Walk" state** → **Make Transition**
2. **Click on "Idle" state** to create transition
3. **Configure transition** similar to above

**Jump Transitions:**
1. **Create transitions** from Idle to Jump
2. **Create transitions** from Walk to Jump
3. **Create transitions** from Jump back to Idle and Walk

### Task 4: Animation Parameters and Triggers

#### **4.1 Add Parameters**

**Create Parameters:**
1. **In Animator window**, click **Parameters** tab
2. **Click "+" button** → **Float**
3. **Name it** "Speed"
4. **Add more parameters**:
   - **Bool**: "IsGrounded"
   - **Bool**: "IsJumping"
   - **Trigger**: "JumpTrigger"

#### **4.2 Configure Transitions with Parameters**

**Speed-Based Transitions:**
1. **Select Idle to Walk transition**
2. **In Inspector**, add condition:
   - **Condition**: Speed > 0.1
3. **Select Walk to Idle transition**
4. **In Inspector**, add condition:
   - **Condition**: Speed < 0.1

**Jump Transitions:**
1. **Select Idle to Jump transition**
2. **In Inspector**, add condition:
   - **Condition**: IsJumping == true
3. **Select Jump to Idle transition**
4. **In Inspector**, add condition:
   - **Condition**: IsGrounded == true

#### **4.3 Test Parameter Changes**

**Create Test Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "AnimationTester"
3. **Replace content** with:

```csharp
using UnityEngine;

public class AnimationTester : MonoBehaviour
{
    [Header("Animation Testing")]
    public Animator animator;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        // Get input
        float horizontalInput = Input.GetAxis("Horizontal");
        
        // Move character
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        
        // Update animation parameters
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        animator.SetBool("IsGrounded", isGrounded);
        
        // Jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("IsJumping", true);
        }
        
        // Check if grounded
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        
        if (isGrounded)
        {
            animator.SetBool("IsJumping", false);
        }
    }
}
```

**Test Animation System:**
1. **Attach AnimationTester script** to character
2. **Add Animator component** if not present
3. **Assign CharacterAnimator** to Animator Controller
4. **Click Play** and test with A/D keys and Space

### Task 5: Animation Events and Curves

#### **5.1 Add Animation Events**

**Create Event Handler Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "AnimationEventHandler"
3. **Replace content** with:

```csharp
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    [Header("Animation Events")]
    public AudioSource audioSource;
    public ParticleSystem dustEffect;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    // Called by animation events
    public void OnFootstep()
    {
        Debug.Log("Footstep sound!");
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
    
    public void OnJumpStart()
    {
        Debug.Log("Jump started!");
        if (dustEffect != null)
        {
            dustEffect.Play();
        }
    }
    
    public void OnJumpLand()
    {
        Debug.Log("Jump landed!");
        if (dustEffect != null)
        {
            dustEffect.Play();
        }
    }
}
```

**Add Events to Animations:**
1. **Open CharacterWalk animation** in Animation window
2. **Click "Add Event"** button
3. **Position event** at appropriate frame
4. **Select event** and configure:
   - **Function**: OnFootstep
   - **Float Parameter**: 0
   - **Int Parameter**: 0
   - **String Parameter**: ""

**Test Animation Events:**
1. **Attach AnimationEventHandler script** to character
2. **Play walk animation** and watch Console
3. **Verify events** are triggered at correct times

#### **5.2 Create Animation Curves**

**Add Speed Curve:**
1. **Open CharacterWalk animation** in Animation window
2. **Click "Add Property"** → **Transform.localScale.x**
3. **Create keyframes** for scale animation:
   - **Frame 0**: Scale X = 1
   - **Frame 10**: Scale X = 1.1
   - **Frame 20**: Scale X = 1
4. **Set curve** to smooth interpolation

**Add Rotation Curve:**
1. **Add Transform.rotation.z** property
2. **Create keyframes** for rotation:
   - **Frame 0**: Rotation Z = 0
   - **Frame 5**: Rotation Z = 5
   - **Frame 10**: Rotation Z = 0
   - **Frame 15**: Rotation Z = -5
   - **Frame 20**: Rotation Z = 0

**Test Curves:**
1. **Play animation** and observe scale/rotation changes
2. **Adjust keyframes** for desired effect
3. **Use curve editor** for smooth interpolation

### Task 6: Performance Optimization

#### **6.1 Sprite Atlas Setup**

**Create Sprite Atlas:**
1. **Right-click in Project** → **Create** → **Sprite Atlas**
2. **Name it** "CharacterAtlas"
3. **In Inspector**, click **"Add"** button
4. **Add character sprite folder** to atlas
5. **Click "Pack Preview"** to generate atlas

**Configure Atlas Settings:**
1. **In Inspector**, configure:
   - **Texture Format**: RGBA32
   - **Compression**: None (for pixel art)
   - **Max Texture Size**: 2048
   - **Padding**: 2

#### **6.2 Animation Optimization**

**Optimize Animation Clips:**
1. **Select animation clip** in Project window
2. **In Inspector**, configure:
   - **Legacy**: Unchecked
   - **Compression**: Optimal
   - **Rotation Error**: 0.1
   - **Position Error**: 0.1
   - **Scale Error**: 0.1

**Reduce Keyframes:**
1. **Open animation** in Animation window
2. **Remove unnecessary keyframes**
3. **Use curve interpolation** instead of keyframes
4. **Test animation** still looks good

#### **6.3 Rendering Optimization**

**Configure Sprite Renderer:**
1. **Select character GameObject**
2. **In Sprite Renderer**, configure:
   - **Material**: Default Sprite Material
   - **Sorting Layer**: Characters
   - **Order in Layer**: 0

**Use Sprite Atlas:**
1. **Assign Sprite Atlas** to Sprite Renderer
2. **Test rendering** in Game view
3. **Check performance** with Unity Profiler

---

## ✅ Completion Checklist

### **Sprite Import and Configuration**
- [ ] **Imported sprite sheets** with correct settings
- [ ] **Sliced sprites** into individual frames
- [ ] **Configured sprite properties** for optimal rendering
- [ ] **Created sorting layers** for proper layering

### **Animation Creation**
- [ ] **Created animation clips** for Idle, Walk, and Jump
- [ ] **Added keyframes** at appropriate intervals
- [ ] **Tested animations** in Animation window
- [ ] **Configured animation settings** for optimal playback

### **Animator Controller**
- [ ] **Created Animator Controller** with animation states
- [ ] **Set default state** and configured transitions
- [ ] **Added parameters** for state control
- [ ] **Tested state machine** with parameter changes

### **Animation Events and Curves**
- [ ] **Added animation events** for sound and effects
- [ ] **Created animation curves** for smooth interpolation
- [ ] **Tested event triggers** and curve effects
- [ ] **Optimized animations** for performance

### **Performance Optimization**
- [ ] **Set up Sprite Atlas** for efficient rendering
- [ ] **Optimized animation clips** for file size
- [ ] **Configured rendering settings** for performance
- [ ] **Tested performance** with Unity Profiler

---

## 🚨 Troubleshooting

### **Common Issues and Solutions**

#### **Sprites not displaying correctly**
**Possible causes:**
- Incorrect import settings
- Wrong Pixels Per Unit
- Incorrect sprite mode

**Solutions:**
1. Check sprite import settings in Inspector
2. Adjust Pixels Per Unit for proper scaling
3. Verify sprite mode is set to Multiple
4. Reimport sprites if needed

#### **Animations not playing**
**Possible causes:**
- Animator not assigned
- Animation clips not in controller
- Parameters not set correctly

**Solutions:**
1. Verify Animator component is attached
2. Check Animator Controller is assigned
3. Ensure animation clips are in controller
4. Verify parameters are set correctly

#### **Animation transitions not working**
**Possible causes:**
- Transition conditions not set
- Parameters not updating
- Exit time settings incorrect

**Solutions:**
1. Check transition conditions in Inspector
2. Verify parameters are being set in script
3. Adjust exit time settings
4. Test transitions manually

#### **Performance issues**
**Possible causes:**
- Too many draw calls
- Large texture sizes
- Inefficient animation setup

**Solutions:**
1. Use Sprite Atlas to reduce draw calls
2. Optimize texture sizes and compression
3. Reduce animation keyframes
4. Use Unity Profiler to identify bottlenecks

---

## 📝 Self-Assessment

After completing this lab, test your understanding with the interactive quiz:

**Take the Quiz**: [`quiz/quiz2.html`](../quiz/quiz2.html)

The quiz will help you:
- Verify your understanding of key concepts
- Identify areas that need review
- Prepare for the next lesson

---

## 📚 Next Steps

### **Immediate Next Steps**
1. **Complete all tasks** in this lab
2. **Test your animation system** thoroughly
3. **Experiment with different animations** and effects
4. **Practice using animation events** and curves

### **Prepare for Next Lesson**
1. **Review animation concepts** and workflows
2. **Understand Animator Controller** and state machines
3. **Practice with sprite management** and optimization
4. **Read Lesson 3 materials** in advance

### **Further Learning**
1. **Unity Learn tutorials** for additional animation practice
2. **Unity documentation** for deeper understanding
3. **Community forums** for questions and help
4. **Practice projects** to reinforce learning

---

## 💡 Pro Tips

### **Animation Best Practices**
- **Plan your animations** before creating them
- **Use consistent frame rates** across all animations
- **Test animations** in context with gameplay
- **Optimize for performance** from the start

### **Sprite Management**
- **Organize sprites** in logical folders
- **Use consistent naming** conventions
- **Optimize sprite sizes** for your target platform
- **Use Sprite Atlas** for better performance

### **Workflow Efficiency**
- **Create animation templates** for similar characters
- **Use animation events** for sound and effects
- **Test animations** with different speeds and scales
- **Document your animation** setup for future reference

---

