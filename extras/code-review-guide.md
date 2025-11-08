# Code Review Guide for Unity Game Development

## 🎯 Overview

Code review is a collaborative process where team members examine each other's code before it's merged into the main codebase. This guide teaches you how to conduct effective code reviews for Unity projects, both as a reviewer and as a code author.

---

## 📚 Table of Contents

1. [Why Code Review?](#why-code-review)
2. [Code Review Process](#code-review-process)
3. [What to Look For](#what-to-look-for)
4. [Giving Feedback](#giving-feedback)
5. [Receiving Feedback](#receiving-feedback)
6. [Unity-Specific Considerations](#unity-specific-considerations)
7. [Common Issues](#common-issues)
8. [Review Checklist](#review-checklist)

---

## 🤔 Why Code Review?

### Benefits

#### **For the Team**
- ✅ **Catch bugs early** before they reach production
- ✅ **Share knowledge** across team members
- ✅ **Maintain code quality** and consistency
- ✅ **Learn from each other** and improve skills

#### **For the Author**
- ✅ **Get fresh perspective** on your code
- ✅ **Learn best practices** from reviewers
- ✅ **Catch mistakes** you might have missed
- ✅ **Improve code quality** before merge

#### **For the Project**
- ✅ **Better codebase** overall
- ✅ **Fewer bugs** in final product
- ✅ **Easier maintenance** with consistent code
- ✅ **Knowledge sharing** prevents single points of failure

### Real-World Example
```
Without Code Review:
- Developer A writes buggy code
- Code gets merged
- Bug discovered in testing
- Time wasted fixing bug
- Project delayed

With Code Review:
- Developer A writes code
- Developer B reviews, catches bug
- Bug fixed before merge
- Clean code in main branch
- Project on schedule
```

---

## 🔄 Code Review Process

### Step-by-Step Workflow

#### **1. Author Creates Pull Request**
```bash
# After completing feature
git push origin feature/player-controller

# Create Pull Request on GitHub/GitLab
# Include:
# - Description of changes
# - What was added/changed
# - How to test
# - Screenshots (if UI changes)
```

#### **2. Pull Request Template**
```markdown
## Description
Brief description of what this PR does

## Changes Made
- [ ] Added player jump mechanic
- [ ] Implemented coyote time
- [ ] Added variable jump height

## Testing
- [ ] Tested jump in gameplay scene
- [ ] Verified coyote time works correctly
- [ ] Tested on different platforms

## Screenshots (if applicable)
[Add screenshots here]

## Related Issues
Closes #123
```

#### **3. Assign Reviewers**
- **At least one reviewer** for small changes
- **Two reviewers** for major features
- **Rotate reviewers** so everyone gets experience

#### **4. Review Process**
- **Reviewer examines code**
- **Leaves comments** on specific lines
- **Approves or requests changes**
- **Author addresses feedback**
- **Re-review if needed**
- **Merge when approved**

---

## 🔍 What to Look For

### 1. Correctness

#### **Does the Code Work?**
- ✅ **Compiles without errors**
- ✅ **No console warnings** (or justified warnings)
- ✅ **Logic is correct**
- ✅ **Edge cases handled**

#### **Example Review Comment**
```csharp
// ❌ Problem: No null check
void OnTriggerEnter2D(Collider2D other)
{
    other.GetComponent<PlayerController>().TakeDamage(10);
}

// ✅ Review Comment:
// "What if other doesn't have PlayerController component? 
//  Add null check to prevent NullReferenceException"
```

### 2. Code Quality

#### **Is the Code Clean?**
- ✅ **Readable and understandable**
- ✅ **Follows naming conventions**
- ✅ **Properly commented**
- ✅ **No code duplication**

#### **Example Review Comment**
```csharp
// ❌ Problem: Magic numbers
if (health < 0)
{
    health = 0;
}

// ✅ Review Comment:
// "Consider using a constant: private const float MIN_HEALTH = 0f;"
```

### 3. Performance

#### **Is it Efficient?**
- ✅ **No unnecessary operations in Update()**
- ✅ **Proper object pooling** (if applicable)
- ✅ **Efficient algorithms**
- ✅ **No memory leaks**

#### **Example Review Comment**
```csharp
// ❌ Problem: Finding object every frame
void Update()
{
    GameObject player = GameObject.Find("Player");
    // ...
}

// ✅ Review Comment:
// "GameObject.Find() is expensive. Cache the reference in Start() or Awake()"
```

### 4. Unity Best Practices

#### **Unity-Specific Issues**
- ✅ **Uses FixedUpdate() for physics**
- ✅ **Uses Update() for input**
- ✅ **Uses LateUpdate() for camera**
- ✅ **Proper component architecture**
- ✅ **Prefabs used correctly**

#### **Example Review Comment**
```csharp
// ❌ Problem: Physics in Update(), using deprecated rigidbody2D property
void Update()
{
    rigidbody2D.velocity = new Vector2(speed, 0);
}

// ✅ Review Comment:
// "Physics movement should be in FixedUpdate() for consistent behavior. 
// Also, cache Rigidbody2D reference and use it instead of deprecated property."
```

### 5. Security & Safety

#### **Is it Safe?**
- ✅ **No hardcoded secrets**
- ✅ **Input validation**
- ✅ **Null checks where needed**
- ✅ **Bounds checking**

#### **Example Review Comment**
```csharp
// ❌ Problem: No bounds check
public void SetHealth(float newHealth)
{
    health = newHealth;
}

// ✅ Review Comment:
// "Add bounds checking: health = Mathf.Clamp(newHealth, 0, maxHealth);"
```

---

## 💬 Giving Feedback

### Feedback Principles

#### **Be Constructive**
```
❌ Bad: "This code is terrible"
✅ Good: "This could be improved by caching the reference"

❌ Bad: "You're doing it wrong"
✅ Good: "Consider using FixedUpdate() for physics movement"
```

#### **Be Specific**
```
❌ Bad: "Fix the bug"
✅ Good: "Line 42: Add null check before calling TakeDamage()"

❌ Bad: "This is confusing"
✅ Good: "This method name doesn't clearly indicate what it does. 
          Consider renaming to CalculateJumpVelocity()"
```

#### **Be Kind**
```
❌ Bad: "Why did you do this? It's obviously wrong"
✅ Good: "I see what you're trying to do. Have you considered 
          this alternative approach?"
```

#### **Explain Why**
```
❌ Bad: "Use FixedUpdate()"
✅ Good: "Use FixedUpdate() instead of Update() because physics 
          calculations need to run at fixed intervals for 
          consistent behavior across different frame rates"
```

### Types of Comments

#### **1. Must Fix (Blocking)**
```markdown
**Must Fix**: This will cause a crash. Add null check before accessing component.
```
- **Blocks merge** until fixed
- **Critical issues** only

#### **2. Should Fix (Important)**
```markdown
**Should Fix**: Consider caching this reference to avoid repeated GetComponent() calls.
```
- **Should be addressed** but not blocking
- **Code quality improvements**

#### **3. Nice to Have (Optional)**
```markdown
**Suggestion**: This could be more readable with a helper method.
```
- **Optional improvements**
- **Polish and refinement**

#### **4. Questions**
```markdown
**Question**: Why is this value hardcoded? Should it be configurable?
```
- **Seeking clarification**
- **Understanding decisions**

### Review Comment Examples

#### **Good Review Comment**
```csharp
// Code being reviewed
void Update()
{
    if (Input.GetKeyDown(KeyCode.Space))
    {
        Jump();
    }
}

// Review Comment:
"✅ Good use of GetKeyDown() for jump input. 
 Consider using the new Input System for better flexibility 
 (though this works fine for now)."
```

#### **Another Good Review Comment**
```csharp
// Code being reviewed
private float health = 100f;

// Review Comment:
"💡 Suggestion: Consider making this a [SerializeField] 
 so it can be adjusted in the Inspector without code changes."
```

---

## 👂 Receiving Feedback

### How to Handle Reviews

#### **1. Don't Take it Personally**
- **Code review is about code**, not you
- **Everyone gets feedback**
- **It's a learning opportunity**

#### **2. Read All Comments Carefully**
- **Understand the concern**
- **Ask questions if unclear**
- **Don't dismiss feedback**

#### **3. Respond to Comments**
```markdown
// On GitHub/GitLab, respond to each comment:

"Good catch! I've added the null check."

"Thanks for the suggestion. I've refactored this to use 
 a helper method for better readability."

"I see your point, but I chose this approach because [reason]. 
 What do you think?"
```

#### **4. Make Changes**
- **Address all "Must Fix" comments**
- **Consider "Should Fix" suggestions**
- **Explain if you disagree** (respectfully)

#### **5. Request Re-review**
- **After making changes**, request another review
- **Don't merge** until approved

### Example Response
```markdown
Thanks for the review! I've addressed your comments:

✅ Added null check (line 42)
✅ Cached the reference in Awake()
✅ Moved physics to FixedUpdate()

I kept the magic number for now because it's only used once, 
but I'm open to extracting it if you think it's needed.

Ready for re-review!
```

---

## 🎮 Unity-Specific Considerations

### Common Unity Review Points

#### **1. MonoBehaviour Lifecycle**
```csharp
// ✅ Good: Proper lifecycle usage
void Awake()
{
    // Component references
    rb = GetComponent<Rigidbody2D>();
}

void Start()
{
    // Dependencies on other objects
    player = FindObjectOfType<PlayerController>();
}

void Update()
{
    // Input and game logic
}

void FixedUpdate()
{
    // Physics
}
```

#### **2. Component Access**
```csharp
// ❌ Bad: Finding every frame
void Update()
{
    GameObject.Find("Player").transform.position = target;
}

// ✅ Good: Cache reference
private Transform playerTransform;

void Start()
{
    playerTransform = GameObject.Find("Player").transform;
}

void Update()
{
    playerTransform.position = target;
}
```

#### **3. Prefab Usage**
```csharp
// ✅ Good: Using prefabs
public GameObject enemyPrefab;

void SpawnEnemy()
{
    Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
}
```

#### **4. Scene Management**
```csharp
// ✅ Good: Proper scene loading
using UnityEngine.SceneManagement;

public void LoadNextLevel()
{
    SceneManager.LoadScene(nextSceneName);
}
```

### Unity Performance Review

#### **Check for Performance Issues**
- ❌ **GetComponent() in Update()** → Cache in Awake/Start
- ❌ **GameObject.Find() in Update()** → Cache reference
- ❌ **String comparisons in Update()** → Use tags or layers
- ❌ **Instantiate/Destroy frequently** → Use object pooling
- ❌ **Expensive calculations every frame** → Cache results

---

## ⚠️ Common Issues

### Issue 1: Missing Null Checks
```csharp
// ❌ Problem
void OnTriggerEnter2D(Collider2D other)
{
    other.GetComponent<PlayerController>().TakeDamage(10);
}

// ✅ Fix
void OnTriggerEnter2D(Collider2D other)
{
    PlayerController player = other.GetComponent<PlayerController>();
    if (player != null)
    {
        player.TakeDamage(10);
    }
}
```

### Issue 2: Wrong Update Method
```csharp
// ❌ Problem: Physics in Update(), using deprecated rigidbody2D property
void Update()
{
    rigidbody2D.velocity = new Vector2(speed, 0);
}

// ✅ Fix: Physics in FixedUpdate(), cache Rigidbody2D reference
private Rigidbody2D rb;

void Awake()
{
    rb = GetComponent<Rigidbody2D>();
}

void FixedUpdate()
{
    rb.velocity = new Vector2(speed, 0);
}
```

### Issue 3: Magic Numbers
```csharp
// ❌ Problem
if (health < 0)
{
    health = 0;
}

// ✅ Fix
private const float MIN_HEALTH = 0f;
private const float MAX_HEALTH = 100f;

if (health < MIN_HEALTH)
{
    health = MIN_HEALTH;
}
```

### Issue 4: Code Duplication
```csharp
// ❌ Problem: Duplicated code
void Attack1()
{
    animator.SetTrigger("Attack");
    damage = 10;
    // ... attack logic
}

void Attack2()
{
    animator.SetTrigger("Attack");
    damage = 15;
    // ... same attack logic
}

// ✅ Fix: Extract common logic
void PerformAttack(int attackDamage, string triggerName)
{
    animator.SetTrigger(triggerName);
    damage = attackDamage;
    // ... attack logic
}
```

---

## ✅ Review Checklist

### General Code Quality
- [ ] Code compiles without errors
- [ ] No unnecessary console warnings
- [ ] Follows naming conventions
- [ ] Properly commented
- [ ] No code duplication
- [ ] Handles edge cases

### Unity-Specific
- [ ] Uses correct Update method (Update/FixedUpdate/LateUpdate)
- [ ] Components cached, not found every frame
- [ ] Prefabs used where appropriate
- [ ] Proper scene management
- [ ] No memory leaks

### Performance
- [ ] No expensive operations in Update()
- [ ] Efficient algorithms
- [ ] Proper object pooling (if needed)
- [ ] No unnecessary allocations

### Safety
- [ ] Null checks where needed
- [ ] Input validation
- [ ] Bounds checking
- [ ] No hardcoded secrets

### Testing
- [ ] Code has been tested
- [ ] Test instructions provided
- [ ] Edge cases considered

---

## 🎓 Best Practices Summary

### For Reviewers
1. **Be constructive** and kind
2. **Be specific** with feedback
3. **Explain why** something should change
4. **Ask questions** to understand decisions
5. **Approve quickly** for good code
6. **Review promptly** (within 24-48 hours)

### For Authors
1. **Keep PRs small** and focused
2. **Write clear descriptions**
3. **Test your code** before requesting review
4. **Respond to all comments**
5. **Don't take feedback personally**
6. **Learn from reviews**

### For the Team
1. **Review regularly** (daily if possible)
2. **Rotate reviewers** for learning
3. **Discuss patterns** and best practices
4. **Celebrate good code** in reviews
5. **Learn together** from mistakes

---

## 🚀 Getting Started

### Your First Code Review

1. **Read the PR description** carefully
2. **Understand what changed** (look at file changes)
3. **Check for obvious issues** (null checks, performance)
4. **Leave constructive comments**
5. **Approve or request changes**
6. **Follow up** on addressed comments

### Practice Exercise

Review this code and provide feedback:

```csharp
public class PlayerController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GameObject player = GameObject.Find("Player");
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        }
    }
}
```

**What issues can you spot?**
- GetComponent() in Update()
- GameObject.Find() in Update()
- Should use GetKeyDown() not GetKey()
- Should use FixedUpdate() for physics
- No null checks

---

## 📚 Resources

- **GitHub Code Review Guide**: https://guides.github.com/activities/code-review/
- **Google's Code Review Guide**: https://google.github.io/eng-practices/review/
- **Unity Code Review Checklist**: See review checklist above

---

**Remember**: Code review is a skill that improves with practice. Start by reviewing small changes, learn from feedback on your own code, and you'll become a better reviewer and developer!

