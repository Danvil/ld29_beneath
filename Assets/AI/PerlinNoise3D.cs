﻿//
//   Project:           Silverlight 3D Perlin Noise
//
//   Description:       Implementation of 3D Perlin Noise after Ken Perlin's reference implementation.
//
//   Changed by:        $Author: rene.schulte $
//   Changed on:        $Date: 2009-05-25 10:30:22 +0200 (Mo, 25 Mai 2009) $
//   Changed in:        $Revision: 7 $
//   Project:           $URL: file:///U:/Data/Development/SVN/SilverlightDemos/trunk/SLPerlinNoise/SLPerlinNoise/SLPerlinNoise/PerlinNoise3D.cs $
//   Id:                $Id: PerlinNoise3D.cs 7 2009-05-25 08:30:22Z rene.schulte $
//
//
//   Copyright (c) 2009 Rene Schulte
//
//   This program is open source software. Please read the License.txt.
//

using System;

namespace SLPerlinNoise
{
   /// <summary>
   /// Implementation of 3D Perlin Noise after Ken Perlin's reference implementation.
   /// </summary>
   public class PerlinNoise3D
   {
      #region Fields

      private int[] permutation;
      private int[] p;

      #endregion

      #region Properties

      public float Frequency { get; set; }
      public float Amplitude { get; set; }
      public float Persistence { get; set; }
      public int Octaves { get; set; }

      #endregion

      #region Contructors

      public PerlinNoise3D()
      {
         permutation = new int[256];
         p = new int[permutation.Length * 2];
         InitNoiseFunctions();

         // Default values
         Frequency      = 0.023f;
         Amplitude      = 2.2f;
         Persistence    = 0.9f;
         Octaves        = 2;
      }

      #endregion

      #region Methods

      public void InitNoiseFunctions()
      {
         Random rand = new Random();

         // Fill empty
         for (int i = 0; i < permutation.Length; i++)
         {
            permutation[i] = -1;
         }

         // Generate random numbers
         for (int i = 0; i < permutation.Length; i++)
         {
            while (true)
            {
               int iP = rand.Next() % permutation.Length;
               if (permutation[iP] == -1)
               {
                  permutation[iP] = i;
                  break;
               }
            }
         }

         // Copy
         for (int i = 0; i < permutation.Length; i++)
         {
            p[permutation.Length + i] = p[i] = permutation[i];
         }
      }

      public float Compute(float x, float y, float z)
      {
         float noise = 0;
         float amp = this.Amplitude;
         float freq = this.Frequency;
         for (int i = 0; i < this.Octaves; i++)
         {
            noise += Noise(x * freq, y * freq, z * freq) * amp;
            freq  *= 2;                                // octave is the double of the previous frequency
            amp   *= this.Persistence;
         }

         // Clamp and return the result
         // if (noise < 0)
         // {
         //    return 0;
         // }
         // else if (noise > 1)
         // {
         //    return 1;
         // }
         return noise;
      }

      private float Noise(float x, float y, float z)
      {
         // Find unit cube that contains point
         int iX = (int)Math.Floor(x) & 255;
         int iY = (int)Math.Floor(y) & 255;
         int iZ = (int)Math.Floor(z) & 255;

         // Find relative x, y, z of the point in the cube.
         x -= (float)Math.Floor(x);
         y -= (float)Math.Floor(y);
         z -= (float)Math.Floor(z);

         // Compute fade curves for each of x, y, z
         float u = Fade(x);
         float v = Fade(y);
         float w = Fade(z);

         // Hash coordinates of the 8 cube corners
         int A = p[iX] + iY;
         int AA = p[A] + iZ;
         int AB = p[A + 1] + iZ;
         int B = p[iX + 1] + iY;
         int BA = p[B] + iZ;
         int BB = p[B + 1] + iZ;

         // And add blended results from 8 corners of cube.
         return Lerp(w, Lerp(v, Lerp(u, Grad(p[AA], x, y, z),
                            Grad(p[BA], x - 1, y, z)),
                    Lerp(u, Grad(p[AB], x, y - 1, z),
                            Grad(p[BB], x - 1, y - 1, z))),
            Lerp(v, Lerp(u, Grad(p[AA + 1], x, y, z - 1),
                            Grad(p[BA + 1], x - 1, y, z - 1)),
                    Lerp(u, Grad(p[AB + 1], x, y - 1, z - 1),
                            Grad(p[BB + 1], x - 1, y - 1, z - 1))));
      }

      private static float Fade(float t)
      {
         // Smooth interpolation parameter
         return (t * t * t * (t * (t * 6 - 15) + 10));
      }

      private static float Lerp(float alpha, float a, float b)
      {
         // Linear interpolation
         return (a + alpha * (b - a));
      }

      private static float Grad(int hashCode, float x, float y, float z)
      {
         // Convert lower 4 bits of hash code into 12 gradient directions
         int h = hashCode & 15;
         float u = h < 8 ? x : y;
         float v = h < 4 ? y : h == 12 || h == 14 ? x : z;
         return (((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v));
      }

      #endregion
   }
}
