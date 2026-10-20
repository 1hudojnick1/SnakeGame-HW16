
using System;
using Shared;

public class ShowTextState : BaseGameState
{
    public string text = "";

    private float duration;
    private float timeLeft;

    public ShowTextState(int seconds)
    {
        duration = seconds;
    }

    public override void Reset()
    {
        timeLeft = duration;
    }

    public override void Update(float deltaTime)
    {
        timeLeft -= deltaTime;
    }

    public override void Draw(ConsoleRenderer renderer)
    {
        if (string.IsNullOrEmpty(text))
            return;

        int x = Math.Max(0, (renderer.width - text.Length) / 2);
        int y = renderer.height / 2;

        int availableWidth = renderer.width - x;

        if (availableWidth <= 0)
            return;

        string displayText = text;

        if (displayText.Length > availableWidth)
            displayText = displayText.Substring(0, availableWidth);

        renderer.DrawString(
            displayText,
            x,
            y,
            ConsoleColor.Green
        );
    }

    public override bool IsDone()
    {
        return timeLeft <= 0;
    }
}
