
using System;

public class SnakeGameLogic : BaseGameLogic
{
    private SnakeGameplayState gameplayState = new SnakeGameplayState();

    private ShowTextState showTextState = new ShowTextState(2);

    private bool newGamePending = false;

    private int currentLevel;

    public override ConsoleColor[] CreatePallet()
    {
        return new ConsoleColor[]
        {
            ConsoleColor.Black,
            ConsoleColor.Green
        };
    }

    public override void OnArrowUp()
    {
        if (currentState != gameplayState)
            return;

        gameplayState.SetDirection(SnakeGameplayState.SnakeDir.Up);
    }

    public override void OnArrowDown()
    {
        if (currentState != gameplayState)
            return;

        gameplayState.SetDirection(SnakeGameplayState.SnakeDir.Down);
    }

    public override void OnArrowLeft()
    {
        if (currentState != gameplayState)
            return;

        gameplayState.SetDirection(SnakeGameplayState.SnakeDir.Left);
    }

    public override void OnArrowRight()
    {
        if (currentState != gameplayState)
            return;

        gameplayState.SetDirection(SnakeGameplayState.SnakeDir.Right);
    }

    public void GotoGameplay()
    {
        gameplayState.level = currentLevel;

        gameplayState.fieldWidth = screenWidth;
        gameplayState.fieldHeight = screenHeight;

        ChangeState(gameplayState);
    }

    public void GoToGameOver()
    {
        currentLevel = 0;

        newGamePending = true;

        showTextState.text = "GAME OVER";

        ChangeState(showTextState);
    }

    public void GoToNextLevel()
    {
        currentLevel += 1;

        newGamePending = false;

        showTextState.text = "LEVEL " + currentLevel;

        ChangeState(showTextState);
    }

    public override void Update(float deltaTime)
    {
        if (currentState != null && !currentState.IsDone())
            return;

        if (currentState == null ||
            (currentState == gameplayState && !gameplayState.gameOver))
        {
            GoToNextLevel();
        }
        else if (currentState == gameplayState && gameplayState.gameOver)
        {
            GoToGameOver();
        }
        else if (currentState != gameplayState && newGamePending)
        {
            GoToNextLevel();
        }
        else if (currentState != gameplayState && !newGamePending)
        {
            GotoGameplay();
        }
    }
}
