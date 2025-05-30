﻿using SFML.Graphics;
using SFML.System;
using static k.Constants;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace k
{
    public class MapCollider
    {
        private readonly Texture wallTexture;
        private readonly byte[] wallMask;
        private readonly List<Sprite> wallSprite;
        private readonly List<Sprite> boxSprite;
        private readonly Texture boxTexture;
        private readonly byte[] boxMask;
        private readonly Vector2f wallScale;

        public MapCollider(List<Sprite> wallTilePositions,List<Sprite> boxSprites)
        {
            wallTexture = new Texture(Path.Combine(AssetsPath,"gray_wall.png"));
            wallMask = PixelPerfectCollision.CreateMask(wallTexture);
            wallSprite =  wallTilePositions;

            boxSprite = boxSprites;
            boxTexture = new Texture(Path.Combine(AssetsPath, "box.png"));
            boxMask =PixelPerfectCollision.CreateMask(boxTexture);


            wallScale = new Vector2f(64f / wallTexture.Size.X, 64f / wallTexture.Size.Y);
        }

        public (bool,Sprite?) Collides(Sprite tankSprite, byte[] tankMask, Vector2f testPos)
        {
            var oldPos = tankSprite.Position;
            tankSprite.Position = testPos;

            foreach (var wallPos in wallSprite)
            {
                

                if (PixelPerfectCollision.Test(tankSprite, tankMask, wallPos, wallMask, AlphaLimit))
                {
                    tankSprite.Position = oldPos;
                    return (true,wallPos);
                }
            }

            tankSprite.Position = oldPos; 
            return (false,null);
        }
        public bool CollidesWithBox(Sprite tankSprite, byte[] tankMask,Vector2f testPos)
        {
            var oldPos = tankSprite.Position;
            tankSprite.Position = testPos;

            foreach (var box in boxSprite)
            {
                if (PixelPerfectCollision.Test(tankSprite, tankMask, box, boxMask, AlphaLimit))
                {
                    tankSprite.Position = oldPos;
                    return true;
                }
            }

            tankSprite.Position = oldPos;
            return false;
        }
    }
}
