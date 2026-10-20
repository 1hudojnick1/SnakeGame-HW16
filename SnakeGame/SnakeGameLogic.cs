using System;

public class SnakeGameLogic : BaseGameLogic
{
    private SnakeGameplayState gameplayState = new SnakeGameplayState();

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
        gameplayState.fieldWidth = screenWidth;
        gameplayState.fieldHeight = screenHeight;

        ChangeState(gameplayState);
    }

    public override void Update(float deltaTime)
    {
        if (currentState != gameplayState)
        {
            GotoGameplay();
        }
    }
}