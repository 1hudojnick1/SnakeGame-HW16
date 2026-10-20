
using System;
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

    public int level;

    public bool gameOver;
    public bool hasWon;

    private List<Cell> Body = new List<Cell>();

    private SnakeDir currentDir;

    private float timeToMove;

    private const char SnakeSymbol = '■';
    private const char circleSymbol = '0';

    private Cell apple;

    private Random random = new Random();

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

    private void GenerateApple()
    {
        Cell cell;

        do
        {
            cell = new Cell(
                random.Next(fieldWidth),
                random.Next(2, fieldHeight)
            );
        }
        while (Body.Contains(cell));

        apple = cell;
    }

    public override void Draw(ConsoleRenderer renderer)
    {
        for (int i = 0; i < Body.Count; i++)
        {
            Cell cell = Body[i];

            if (cell.X < 0 || cell.X >= fieldWidth ||
                cell.Y < 0 || cell.Y >= fieldHeight)
                continue;

            renderer.SetPixel(
                cell.X,
                cell.Y,
                SnakeSymbol,
                1
            );
        }

        renderer.SetPixel(
            apple.X,
            apple.Y,
            circleSymbol,
            1
        );

        int applesCollected = Body.Count - 1;

        renderer.DrawString(
            "Apples: " + applesCollected,
            0,
            0,
            ConsoleColor.Green
        );

        renderer.DrawString(
            "Level: " + level,
            0,
            1,
            ConsoleColor.Green
        );
    }

    public override void Update(float deltaTime)
    {
        timeToMove -= deltaTime;

        if (gameOver || hasWon || timeToMove > 0)
            return;

        timeToMove = 1f / (level + 4f);

        Cell head = Body[0];

        Cell nextCell = ShiftTo(head);

        if (nextCell.X < 0 ||
            nextCell.X >= fieldWidth ||
            nextCell.Y < 0 ||
            nextCell.Y >= fieldHeight)
        {
            gameOver = true;
            return;
        }

        if (nextCell.Equals(apple))
        {
            Body.Insert(0, apple);

            if (Body.Count >= level + 3)
            {
                hasWon = true;
            }

            GenerateApple();

            return;
        }

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

        apple = new Cell(0, 0);

        GenerateApple();

        gameOver = false;
        hasWon = false;

        timeToMove = 0;
    }

    public override bool IsDone()
    {
        return gameOver || hasWon;
    }
}
