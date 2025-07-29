using UnityEngine;

public class CameraFollow : StateBehaviour
{
    //states order need to match the order of the components in the inspector
    private enum CameraState
    {
        GAMEPLAY,
        LEVEL_COMPLETE,
        GAME_OVER,
        NUM
    }

    protected override void Awake()
    {
        base.Awake();
        
        GameplayEvents.LevelComplete += GameplayEventsOnLevelComplete;
        GameplayEvents.LevelReset += GameplayEventsOnLevelReset;
        GameplayEvents.GameOver += GameplayEventsOnGameOver;
        
        GoToState((int)CameraState.GAMEPLAY);
    }

    private void GameplayEventsOnGameOver()
    {
        GoToState((int)CameraState.GAME_OVER);
    }

    private void OnDestroy()
    {
        GameplayEvents.LevelComplete -= GameplayEventsOnLevelComplete;
        GameplayEvents.LevelReset -= GameplayEventsOnLevelReset;
        GameplayEvents.GameOver -= GameplayEventsOnGameOver;
    }
    
    private void GameplayEventsOnLevelReset()
    {
        GoToState((int)CameraState.GAMEPLAY);
    }
    
    private void GameplayEventsOnLevelComplete()
    {
        GoToState((int)CameraState.LEVEL_COMPLETE);
    }

    private void FixedUpdate()
    {
        UpdateCurrentState();
    }
}