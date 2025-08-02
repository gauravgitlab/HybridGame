# HybridGame

NOTES :
===================================================================================================================================================================
- Using Unity 2022.3.55f1
- we are avoiding commenting on code as given code does not have any.

Task 1 : New Mechanic, Shop
====================================================================================================================================================================
- for the shop data, we can either Use ScriptableObjects (in Unity) or a JSON-based config to define cosmetics, here we are using ScriptableObjects, as we can see we are already using it for Droppables Data
- things to do
- UI, Scriptable objects, saved equipped items and saved level progressions
-  

Task 2 : Improvement
=====================================================================================================================================================================
- Animating Screens (GameScreen, ShopScreen, StartScreen, LevelFailedScreen and LevelCompleteScreen) when Appearing, Currently we are using the code to animate the screen, we can also use Animation for that.
- Adding the EnemyType config to assign for spawing enemy with different health and scale, we can add more enemy type using scriptable object
- Assign random Hats and Beards on spawing enemy, (we can also make cosmetic as part of config, but after completely all levels, the level restarts to 0, so random will gives new vibe in the same level.)
- assignn random skycolor on level load
- UI Screen (GameScreen, ShopScreen, StartScreen, LevelFailedScreen and LevelCompleteScreen) alignment and polishing


Task 3 : Bugs
=====================================================================================================================================================================
- Bug 1
- Status - Fixed, we are saving the current level in `PlayerProgressionData`, we can also used `PlayerPrefs` as well as its doing for given `GameBank` as well.

- Bug 2
- Status - Fixed, we are resetting the player level to 0 again, after completing all the levels, it will remove the soft lock and player keep continue playing the game.
