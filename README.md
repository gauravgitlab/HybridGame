# HybridGame
============================================================================================================

============================================================================================================
NOTES :
============================================================================================================
- Using Unity 2022.3.55f1
- We attempt all tasks ( Task 1, Task 2 and Task 3(Bonus Tasks) )


============================================================================================================
Task 1 : New Mechanic, Shop
============================================================================================================
- for the shop data, we can either Use ScriptableObjects (in Unity) or a JSON-based config to define cosmetics, here we are using ScriptableObjects, as we can see we are already using it for Droppables Data
- things to do
- UI, Scriptable objects, saved equipped items and saved level progressions
- Adding SaveSystem


============================================================================================================
Task 2 : Improve the game feel
============================================================================================================
- Change: Added ScreenAnimator Script for UI Screens Animation
Script: ScreenAnimator.cs
Reason: Introduced this script to enhance user experience by adding smooth scaling animations to UI screens/popups when they appear. The animation improves visual feedback and gives a more polished, responsive feel to the interface. This change focuses on UI/UX polish and aligns with modern app standards where dynamic, responsive interfaces are expected.

- Change: Enforced Enemy Elimination as Completion Condition
Reason: Previously, levels could be completed by simply reaching the end, allowing players to bypass all enemies. This undermined the intended gameplay challenge and progression pacing. I updated the level completion logic to require all enemies to be defeated before a level is marked as complete.

- Change: Introduced Scriptable Object for Enemy Type Configuration
Script: EnemyTypeScriptable.cs
Reason: The game previously used a single, hardcoded enemy prefab with fixed health, scale. This limited variety and made balancing or theming enemies tedious. I introduced a ScriptableObject–based system to define enemy types with configurable properties such as health and scale.

- Change: Randomized Enemy Visuals with Hats and Beards
Reason: Previously, all enemies spawned with the same appearance (e.g., default HornedHelm hat, no beard), which reduced visual diversity and led to repetitive encounters. I introduced logic to randomize each enemy's hat and beard on spawn, improving the variety and visual richness of the game world.

- Change: Dynamic Skybox Color Update on Level Reset
Script: SkyboxGradientHandler.cs
Reason: Added this script to improve visual variety and reinforce the feeling of progression by dynamically changing the skybox gradient colors each time the level resets. This avoids visual monotony and adds a subtle but effective layer of feedback to the game loop.

- Change: UI Alignment and Layout Standardization Across Screens
Reason: Reworked the alignment and layout of all major UI screens to ensure visual consistency, improved user experience, and better responsiveness across resolutions. Previously, UI elements were scattered and misaligned, leading to a cluttered interface.

- Change: Implemented Visual Joystick UI with Handle(thumb) Movement Logic
Script Method: UIVisualSet()
Reason: Previously, the game included virtual joystick input logic but lacked the corresponding UI components and visual feedback. I implemented joystick UI visuals and added logic to move the inner handle (thumb) based on player input, enhancing player control clarity on touch devices.

- Change: Replaced Enemy Count '0' with Tick Icon on Completion on GameScreen
Script Method: GameplayEventsOnEnemyCountUpdated(int count)
Reason: Improved end-of-combat feedback in the UI by hiding the numeric value with a visual tick icon when all enemies are defeated. This provides a more intuitive and polished experience for players, clearly signaling task completion without relying on raw numbers.


============================================================================================================
Task 3 : Bugs
============================================================================================================
- Bug 1
Status - we are saving the current level in `PlayerProgressionData`, with the purchased and equppid items.

- Bug 2
Status - we are resetting the player level to 0 again, after completing all the levels, it will remove the soft lock and player keep continue playing the game.

============================================================================================================
============================================================================================================