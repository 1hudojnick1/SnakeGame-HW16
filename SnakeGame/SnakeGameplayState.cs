using System.Collections.Generic;
using Shared;

public class SnakeGameplayState : BaseGameState
{
    public struct Cell
    {
        public int X;
        public int Y;

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public enum SnakeDir
    {
        Up,
        Down,
        Left,
        Right
    }

    public int fieldWidth;
    public int fieldHeight;

    private List<Cell> Body = new List<Cell>();

    private SnakeDir currentDir;

    private float timeToMove;

    private const char SnakeSymbol = '■';

    public void SetDirection(SnakeDir dir)
    {
        currentDir = dir;
    }

    private Cell ShiftTo(Cell cell)
    {
        switch (currentDir)
        {
            case SnakeDir.Up:
                cell.Y -= 1;
                break;

            case SnakeDir.Down:
                cell.Y += 1;
                break;

            case SnakeDir.Left:
                cell.X -= 1;
                break;

            case SnakeDir.Right:
                cell.X += 1;
                break;
        }

        return cell;
    }

    public override void Draw(ConsoleRenderer renderer)
    {
        for (int i = 0; i < Body.Count; i++)
        {
            Cell cell = Body[i];

            if (cell.X < 0 || cell.X >= fieldWidth || cell.Y < 0 || cell.Y >= fieldHeight)
                continue;

            renderer.SetPixel(cell.X, cell.Y, SnakeSymbol, 1);
        }
    }

    public override void Update(float deltaTime)
    {
        timeToMove -= deltaTime;

        if (timeToMove > 0)
            return;

        timeToMove = 1f / 5f;

        Cell head = Body[0];
        Cell nextCell = ShiftTo(head);

        Body.RemoveAt(Body.Count - 1);
        Body.Insert(0, nextCell);
    }

    public override void Reset()
    {
        Body.Clear();

        int middleY = fieldHeight / 2;
        int middleX = fieldWidth / 2;

        currentDir = SnakeDir.Right;

        Body.Add(new Cell(middleX, middleY));

        timeToMove = 0;
    }
}