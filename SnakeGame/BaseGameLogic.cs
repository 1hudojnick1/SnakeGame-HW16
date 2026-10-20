using System;
using Shared;

public abstract class BaseGameLogic : ConsoleInput.IArrowListener
{
    protected BaseGameState currentState;

    protected float time;

    protected int screenWidth;
    protected int screenHeight;

    public void InitializeInput(ConsoleInput input)
    {
        input.Subscribe(this);
    }

    public void ChangeState(BaseGameState state)
    {
        currentState = state;
        currentState.Reset();
    }

    public void DrawNewState(float deltaTime, ConsoleRenderer renderer)
    {
        time += deltaTime;

        screenWidth = renderer.width;
        screenHeight = renderer.height;

        if (currentState != null)
        {
            currentState.Update(deltaTime);
            currentState.Draw(renderer);
        }

        Update(deltaTime);
    }

    public abstract ConsoleColor[] CreatePallet();

    public abstract void Update(float deltaTime);

    public virtual void OnArrowUp()
    {
    }

    public virtual void OnArrowDown()
    {
    }

    public virtual void OnArrowLeft()
    {
    }

    public virtual void OnArrowRight()
    {
    }
}