using System;
using System.Collections.Generic;

public class ConsoleInput
{
    public interface IArrowListener
    {
        void OnArrowUp();
        void OnArrowDown();
        void OnArrowLeft();
        void OnArrowRight();
    }

    private List<IArrowListener> arrowListeners = new List<IArrowListener>();

    public void Subscribe(IArrowListener listener)
    {
        arrowListeners.Add(listener);
    }

    public void Update()
    {
        if (!Console.KeyAvailable)
            return;

        ConsoleKey key = Console.ReadKey(true).Key;

        foreach (IArrowListener listener in arrowListeners)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    listener.OnArrowUp();
                    break;

                case ConsoleKey.DownArrow:
                    listener.OnArrowDown();
                    break;

                case ConsoleKey.LeftArrow:
                    listener.OnArrowLeft();
                    break;

                case ConsoleKey.RightArrow:
                    listener.OnArrowRight();
                    break;
            }
        }
    }
}