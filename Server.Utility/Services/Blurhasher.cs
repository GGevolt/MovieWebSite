using Server.Utility.Interfaces;
using System;
using System;
using Blurhash;
using System.Drawing;

namespace Server.Utility.Services
{
    public class Blurhasher : IBlurhasher
    {
        public string Encode(string imagePath)
        {
            using var bmp = new Bitmap(imagePath);
            int width = bmp.Width;
            int height = bmp.Height;
            var pixels = new Pixel[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixelColor = bmp.GetPixel(x, y);
                    pixels[x, y] = new Pixel(
                        pixelColor.R / 255f, 
                        pixelColor.G / 255f,
                        pixelColor.B / 255f
                    );
                }
            }
            return Blurhash.Core.Encode(pixels, 3, 1);
        }
    }
}
