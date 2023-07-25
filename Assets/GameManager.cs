using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GameManager: singleton instance, instatiated *once*, holds global data.
// Important global data: the list of scenes associated with levels;
// the number of which level we're on.

public class GameManager
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }

    string[] GameLevels =
    {
        "Scenes/Levels/FirstLevel",
        "Scenes/Levels/PryFromWalls",
        "Scenes/Levels/LiftUsingWall",
        "Scenes/Levels/LiftUsingSeesaw",
        "Scenes/WinScreen" // We need a win screen to take us back to the main menu or something.
    };

    public int CurrentLevel = 0;

    // Loads the next level in the game, by incrementing the CurrentLevel
    // counter and performing a load.
    //
    // If we want to start over from 0, we can set resetToZero to true.
    //
    // Also note that the last level in the list should be something like
    // a win screen so that we can go back to the main menu.
    public void LoadNextGameLevel(bool resetToZero = false)
    {
        if(resetToZero)
        {
            CurrentLevel = 0;
        }
        else
        {
            // Induction: the first time we call, we set to 0 and load.
            // The next time we call, CurrentLevel will still be 0, so
            // we need to increment it.
            CurrentLevel += 1;
        }

        // Safety measure: don't try to load past the list in case
        // we forget a win screen.
        if(CurrentLevel > GameLevels.Length - 1)
        {
            CurrentLevel = GameLevels.Length - 1;
        }

        // Make sure to have a SceneTransitionCanvas in every scene :)
        SceneTransitionCanvas.ChangeSceneStatic(GameLevels[CurrentLevel]);
    }
}
