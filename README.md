# HybridGame
============================================================================================================

============================================================================================================
NOTES :
============================================================================================================
- Using Unity 2022.3.55f1
- Attempt all tasks ( Task 1, Task 2 and Task 3 (Bonus Tasks) )

============================================================================================================
Task 1 : New Mechanic, Shop
============================================================================================================
- Introduced ScriptableObject-Based Cosmetic System for Shop Items (Hats & Beards)
Scripts: `CosmeticItemScriptable.cs` and `CosmeticDatabaseScriptable.cs`
To support a growing collection of cosmetic items (e.g., hats and beards) in the in-game shop, I implemented a data-driven system using ScriptableObjects. This allows designers to create and manage individual cosmetics without modifying code, while keeping all cosmetic-related data centralized and structured. All the objects are created inside the `Data` folder.

- Dynamic Cosmetic Shop Screen with Categorized UI & Event-Driven Updates
Scripts: `ShopScreen.cs`
Implemented a complete shop interface to display categorized cosmetic items (hats, beards, etc.) dynamically at runtime. Previously, there was no structured UI for browsing or equipping cosmetics. This system is now modular and supports future category expansion without hardcoding.
Tabs are created at runtime based on the categories defined in `CosmeticManager.Instance.m_cosmeticCategorizedItems.Keys`, allowing non-programmers to add new cosmetic categories through data only.
Utilized prefab-based structure (m_categoryTabPrefab, m_cosmeticItemScrollViewPrefab, m_categoryItemPrefab) to instantiate cosmetic cards and scroll views per category.

Event-Driven UI Updates:
Subscribed to `ShopEvents.CosmeticPurchased` and `ShopEvents.CosmeticEquipped` to update the UI in response to runtime changes.
Efficiently refreshes or toggles visual state of cosmetic item cards (e.g., updating equip state or refreshing unlock visuals).

- Implemented Generic JSON Save System with Environment-Aware File Paths
Script: `SaveSystem.cs`
Implemented a reusable save/load system that can serialize any game data type into JSON. This system ensures easy data inspection during development and robust file handling in builds, supporting both runtime persistence and debugging.
One system can handle all game save needs (e.g., player progress, settings, cosmetic unlocks) without duplicate logic.
In Editor: Uses `Application.dataPath + /Progression/`, so developers can inspect and validate JSON save files directly within the Unity project folder.
In Build: Uses `Application.persistentDataPath`, ensuring platform-safe, sandboxed file access at runtime.

============================================================================================================
Task 2 : Improve the game feel
============================================================================================================
1- Change: Added ScreenAnimator Script for UI Screens Animation
Script: `ScreenAnimator.cs`
Reason: Introduced this script to enhance user experience by adding smooth scaling animations to UI screens/popups when they appear. The animation improves visual feedback and gives a more polished, responsive feel to the interface. This change focuses on UI/UX polish and aligns with modern app standards where dynamic, responsive interfaces are expected.

2- Change: Enforced Enemy Elimination as Completion Condition
Reason: Previously, levels could be completed by simply reaching the end, allowing players to bypass all enemies. This undermined the intended gameplay challenge and progression pacing. I updated the level completion logic to require all enemies to be defeated before a level is marked as complete.

3- Change: Introduced Scriptable Object for Enemy Type Configuration
Script: `EnemyTypeScriptable.cs`
Reason: The game previously used a single, hardcoded enemy prefab with fixed health, scale. This limited variety and made balancing or theming enemies tedious. I introduced a ScriptableObject–based system to define enemy types with configurable properties such as health and scale.

4- Change: Randomized Enemy Visuals with Hats and Beards
Reason: Previously, all enemies spawned with the same appearance (e.g., default HornedHelm hat, no beard), which reduced visual diversity and led to repetitive encounters. I introduced logic to randomize each enemy's hat and beard on spawn, improving the variety and visual richness of the game world.

5- Change: Dynamic Skybox Color Update on Level Reset
Script: `SkyboxGradientHandler.cs`
Reason: Added this script to improve visual variety and reinforce the feeling of progression by dynamically changing the skybox gradient colors each time the level resets. This avoids visual monotony and adds a subtle but effective layer of feedback to the game loop.

6- Change: UI Alignment and Layout Standardization Across Screens
Reason: Reworked the alignment and layout of all major UI screens to ensure visual consistency, improved user experience, and better responsiveness across resolutions. Previously, UI elements were scattered and misaligned, leading to a cluttered interface.

7- Change: Implemented Visual Joystick UI with Handle(thumb) Movement Logic
Script Method: `UIVisualSet()`
Reason: Previously, the game included virtual joystick input logic but lacked the corresponding UI components and visual feedback. I implemented joystick UI visuals and added logic to move the inner handle (thumb) based on player input, enhancing player control clarity on touch devices.

8- Change: Replaced Enemy Count '0' with Tick Icon on Completion on GameScreen
Script Method: `GameplayEventsOnEnemyCountUpdated(int count)`
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