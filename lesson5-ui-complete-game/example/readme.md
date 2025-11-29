# Lesson 5 Example: UI & Complete Game

## 🎯 What This Example Teaches

This example demonstrates complete game development:
- **UI System**: Menus, HUD, and user interface
- **Game State Management**: Pause, resume, game over
- **Save/Load System**: Persisting game data
- **Build Pipeline**: Preparing games for release
- **Performance Optimization**: Profiling and optimization
- **Complete Game Loop**: From start to finish

## 🚀 How to Use This Example

### Step 1: Create Project Structure
1. Create new folder: `Assets/Scenes/`
2. Create 3 scenes: 
   - `MainMenu.unity` (scene index 0)
   - `Game.unity` (scene index 1)
   - `GameOver.unity` (scene index 2)
3. Add all scenes to Build Settings (File → Build Settings → Add Open Scenes)
4. Save each scene after creation

### Step 2: Setup Main Menu Scene
1. Open `MainMenu.unity`
2. Create UI Canvas: GameObject → UI → Canvas
3. Add EventSystem (auto-created with Canvas)
4. Set Canvas settings:
   - Render Mode: Screen Space - Overlay
   - UI Scale Mode: Scale With Screen Size
   - Reference Resolution: 1920x1080
5. Create UI hierarchy:
   ```
   Canvas
   └── MainMenuPanel
       ├── TitleText
       ├── StartButton
       ├── OptionsButton
       └── QuitButton
   ```
6. Add `MenuManager.cs` to an empty GameObject named "MenuManager"
7. Add `MainMenuPanel.cs` to MainMenuPanel
8. Link buttons to methods:
   - StartButton → MenuManager.StartGame()
   - OptionsButton → MenuManager.OpenOptions()
   - QuitButton → MenuManager.QuitGame()

### Step 3: Setup Game Scene
1. Open `Game.unity`
2. Create UI Canvas (same settings as MainMenu)
3. Create HUD hierarchy:
   ```
   Canvas
   ├── HUDPanel
   │   ├── HealthBar (UI Slider)
   │   ├── ScoreText
   │   └── PauseButton
   └── PauseMenuPanel (initially inactive)
       ├── ResumeButton
       ├── RestartButton
       └── MainMenuButton
   ```
4. Setup HealthBar:
   - Add UI Slider component
   - Set Min Value: 0, Max Value: 100
   - Disable Interactable
   - Add `HealthBar.cs` script
   - Reference Fill Image in script
5. Add `GameManager.cs` to an empty GameObject named "GameManager"
6. Add `HUDManager.cs` to HUDPanel
7. Link GameManager references:
   - PauseMenuPanel → PauseMenuPanel object
   - HUDManager → HUDManager component
   - HealthBar → HealthBar component
8. Setup Pause Menu buttons:
   - ResumeButton → GameManager.ResumeGame()
   - RestartButton → GameManager.RestartGame()
   - MainMenuButton → GameManager.LoadMainMenu()
9. Deactivate PauseMenuPanel in Inspector

### Step 4: Configure UI Components
1. Setup UIButton.cs:
   - Add to any button needing hover effects
   - Configure colors in Inspector
   - Add sound effects (optional)
2. Setup UIText.cs:
   - Add to ScoreText and other dynamic text
   - Configure animation settings
3. Setup UIPanel.cs:
   - Add to MainMenuPanel and PauseMenuPanel
   - Configure fade in/out duration
4. Setup SpecializedButtons.cs:
   - Use for special button behaviors
   - Configure specific button types

### Step 5: Test UI Navigation
1. Test Main Menu:
   - Press Play
   - Click Start Button → Should load Game scene
   - Test Options and Quit buttons
2. Test Game Scene:
   - Verify HUD displays correctly
   - Press ESC or Pause button → Pause menu appears
   - Test Resume → Game continues
   - Test Restart → Scene reloads
   - Test Main Menu → Returns to main menu
3. Test HealthBar:
   - Trigger damage events
   - Verify health bar decreases smoothly
   - Test at 0 health → Game Over

### Step 6: Build and Deploy
1. Open Build Settings (Ctrl+Shift+B)
2. Verify scenes are in correct order:
   - Scene 0: MainMenu
   - Scene 1: Game
   - Scene 2: GameOver
3. Select target platform
4. Click Player Settings:
   - Set Company Name
   - Set Product Name
   - Set Default Icon
   - Configure Quality settings
5. Click Build → Choose output folder
6. Test the built game

## 🔧 Troubleshooting

**UI not showing**: Check Canvas settings and UI elements
**Buttons not working**: Verify event handlers are assigned
**Save not working**: Check file permissions and paths
**Build errors**: Verify build settings and dependencies
**Performance issues**: Use Profiler to identify bottlenecks

## 💡 Learning Points

- **Canvas**: UI rendering system
- **Event System**: UI interaction handling
- **Game State**: Managing different game states
- **Save System**: Persisting game data
- **Build Pipeline**: Preparing for release
- **Performance**: Optimization techniques

---

**Next Steps**: 
1. Complete the lab exercises to practice these concepts
2. After completing the lab, test your knowledge with the [quiz](../quiz/quiz5.html).