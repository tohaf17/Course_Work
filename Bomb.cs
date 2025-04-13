﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace  My_Game;
public class Bomb
{
    private Texture2D texture;
    private Vector2 position;
    private Vector2 direction;
    private float speed = 5f; // Швидкість польоту бомби
    private bool isActive = true; // Чи активна бомба

    public Bomb(Texture2D texture, Vector2 startPosition, float rotation)
    {
<<<<<<< HEAD
        this.texture = texture;
        this.position = startPosition;
        this.direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
    }

    public void Update(int[,] map, int tileSize, Tank[] tanks)
    {
        if (!isActive) return;

        position += direction * speed;

        // Перевіряємо зіткнення з картою
        if (CollidesWithMap(map, tileSize))
        {
            isActive = false; // Бомба зникає
        }

        // Перевіряємо зіткнення з танками
        foreach (var tank in tanks)
        {
            if (Intersects(tank))
            {
                tank.SetDestroyed(); // Танк вибухає
                isActive = false;
                break;
            }
        }
    }

    private bool CollidesWithMap(int[,] map, int tileSize)
    {
        int tileX = (int)position.X / tileSize;
        int tileY = (int)position.Y / tileSize;

        return map[tileY, tileX] != 0; // Якщо осередок не порожній – зіткнення
    }

    private bool Intersects(Tank tank)
    {
        Rectangle bombRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        Rectangle tankRect = new Rectangle((int)tank.Position.X, (int)tank.Position.Y, tank.Texture.Width, tank.Texture.Height);

        return bombRect.Intersects(tankRect);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (isActive)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }

    public bool IsActive => isActive;
}
=======
        private Texture2D texture;
        private Vector2 position;
        private Vector2 direction;
        private float rotation;
        private float speed = 5f;
        private bool isActive = true;
        private Vector2 mapOffset = new Vector2(500, 250);
        private Tank owner;

        public Bomb(Texture2D texture, Vector2 startPosition, Vector2 direction, float rotation, Tank owner)
        {
            this.texture = texture;
            this.position = startPosition;
            this.direction = direction;
            this.rotation = rotation;
            this.owner = owner;
        }

        public void Update(int[,] map, int tileSize, Tank[] tanks)
        {
            if (!isActive) return;

            Vector2 newPosition = position + direction * speed;
            Vector2 checkPos = newPosition - mapOffset;

            if (!IsInsideMap(checkPos, map, tileSize) || CollidesWithMap(map, tileSize, checkPos))
            {
                isActive = false;
                return;
            }

            position = newPosition;

            foreach (var tank in tanks)
            {
                if (tank != null && tank != owner && Intersects(tank))
                {
                    tank.SetDestroyed();
                    isActive = false;
                    Console.WriteLine("💥 Бомба влучила в танк!");
                    break;
                }
            }
        }

        private bool IsInsideMap(Vector2 pos, int[,] map, int tileSize)
        {
            int x = (int)pos.X / tileSize;
            int y = (int)pos.Y / tileSize;
            return x >= 0 && y >= 0 && y < map.GetLength(0) && x < map.GetLength(1);
        }

        private bool CollidesWithMap(int[,] map, int tileSize, Vector2 pos)
        {
            int tileX = (int)pos.X / tileSize;
            int tileY = (int)pos.Y / tileSize;
            return map[tileY, tileX] != 0;
        }

        private bool Intersects(Tank tank)
        {
            Rectangle bombRect = new Rectangle(
                (int)(position.X - texture.Width * 0.125f),
                (int)(position.Y - texture.Height * 0.125f),
                (int)(texture.Width * 0.25f),
                (int)(texture.Height * 0.25f)
            );

            Rectangle tankRect = new Rectangle(
                (int)tank.Position.X - 32,
                (int)tank.Position.Y - 32,
                64,
                64
            );

            return bombRect.Intersects(tankRect);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                spriteBatch.Draw(
                    texture,
                    position,
                    null,
                    Color.White,
                    rotation + MathF.PI,
                    new Vector2(texture.Width / 2f, texture.Height / 2f),
                    0.25f,
                    SpriteEffects.None,
                    0f
                );
            }
        }

        public bool IsActive => isActive;
        public Vector2 Position => position;
    }
}
>>>>>>> 45861ca (Bomb -update 1)
