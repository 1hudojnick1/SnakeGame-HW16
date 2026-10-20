using System;
using System.Threading;
using Shared;

public class Program
{
    private const float targetFrameTime = 1f / 60f;

    public static void Main()
    {
        SnakeGameLogic gameLogic = new SnakeGameLogic();

        ConsoleInput input = new ConsoleInput();

        gameLogic.InitializeInput(input);

        ConsoleColor[] pallete = gameLogic.CreatePallet();

        ConsoleRenderer renderer0 = new ConsoleRenderer(pallete);
        ConsoleRenderer renderer1 = new ConsoleRenderer(pallete);

        ConsoleRenderer prevRenderer = renderer0;
        ConsoleRenderer currRenderer = renderer1;

        DateTime lastFrameTime = DateTime.Now;

        while (true)
        {
            input.Update();

            DateTime frameStartTime = DateTime.Now;

            float deltaTime =
                (float)(frameStartTime - lastFrameTime).TotalSeconds;

            gameLogic.DrawNewState(deltaTime, currRenderer);

            if (currRenderer != prevRenderer)
            {
                currRenderer.Render();
            }

            ConsoleRenderer tmp = prevRenderer;

            prevRenderer = currRenderer;
            currRenderer = tmp;

            currRenderer.Clear();

            lastFrameTime = frameStartTime;

            DateTime nextFrameTime =
                frameStartTime.AddSeconds(targetFrameTime);

            DateTime endFrameTime = DateTime.Now;

            if (nextFrameTime > endFrameTime)
            {
                int sleepTime =
                    (int)(nextFrameTime - endFrameTime).TotalMilliseconds;

                Thread.Sleep(sleepTime);
            }
        }
    }
}