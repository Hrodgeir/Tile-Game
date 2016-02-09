﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TileGame
{
    public class Map
    {
        public List<Tile> tileList;
        public TextureList textureList;
        
        public Map(SpriteBatch spriteBatch, TextureList newTextureList, Viewport viewport)
        {
            textureList = newTextureList;
            tileList = new List<Tile>();

            for (int x = 3; x < (viewport.Width - 128) / 32; x++)
            {
                for (int y = 3; y < (viewport.Height - 128) / 32; y++)
                {
                    Tile tile = new Tile(textureList.Get("tile-grid"), new Vector2(x * 32, y * 32));
                    tileList.Add(tile);
                }
            }
        }

        /// <summary>
        /// Finds the path from start to finish using A* pathfinding algorithm.
        /// </summary>
        /// <param name="start">The start location of the selected character.</param>
        /// <param name="finish">The selected location on the map.</param>
        /// <returns>The shortest path from start to finish.</returns>
        public List<Tile> FindPath(Tile start, Tile finish)
        {
            List<Tile> openList = new List<Tile>() { start };
            List<Tile> closedList = new List<Tile>();
            Dictionary<Tile, Tile> cameFrom = new Dictionary<Tile, Tile>();
            Dictionary<Tile, int> currentDistance = new Dictionary<Tile, int>();
            Dictionary<Tile, float> predictedDistance = new Dictionary<Tile, float>();

            currentDistance.Add(start, 0);
            predictedDistance.Add(start, Math.Abs(start.position.X - finish.position.X) + Math.Abs(start.position.Y - finish.position.Y));

            while (openList.Count > 0)
            {
                // Gets the node with the lowest estimated cost to the finish.
                Tile current = (from p in openList orderby predictedDistance[p] ascending select p).First();

                if (current.position.X == finish.position.X && current.position.Y == finish.position.Y)
                {
                    return ConstructPath(cameFrom, finish);
                }

                openList.Remove(current);
                closedList.Add(current);

                foreach (Tile neighbour in GetNeighbourTiles(current))
                {
                    int tempCurrentDistance = currentDistance[current] + 1;

                    if (closedList.Contains(neighbour) && tempCurrentDistance >= currentDistance[neighbour])
                    {
                        continue;
                    }

                    if (!closedList.Contains(neighbour) || tempCurrentDistance < currentDistance[neighbour])
                    {
                        if (cameFrom.Keys.Contains(neighbour))
                        {
                            cameFrom[neighbour] = current;
                        }
                        else
                        {
                            cameFrom.Add(neighbour, current);
                        }

                        currentDistance[neighbour] = tempCurrentDistance;
                        predictedDistance[neighbour] = currentDistance[neighbour] 
                            + Math.Abs(neighbour.position.X - finish.position.X) 
                            + Math.Abs(neighbour.position.Y - finish.position.Y);

                        if (!openList.Contains(neighbour))
                        {
                            openList.Add(neighbour);
                        }
                    }
                }
            }

            throw new Exception(
                string.Format
                ("Unable to find a path from {0},{1}, to {2},{3}", 
                start.position.X, start.position.Y, 
                finish.position.X, finish.position.Y));
        }

        /// <summary>
        /// Creates a list of valid paths generated by the FindPath method and returns a path to current.
        /// </summary>
        /// <param name="cameFrom">An origin and list of nodes.</param>
        /// <param name="current">The destination node we are looking for.</param>
        /// <returns>The shortest path from the start tile to the finish tile.</returns>
        private List<Tile> ConstructPath(Dictionary<Tile, Tile> cameFrom, Tile current)
        {
            if (!cameFrom.Keys.Contains(current))
            {
                return new List<Tile> { current };
            }

            List<Tile> path = ConstructPath(cameFrom, cameFrom[current]);
            path.Add(current);
            return path;
        }

        private IEnumerable<Tile> GetNeighbourTiles(Tile currentTile)
        {
            List<Tile> neighbourTiles = new List<Tile>();

            foreach (Tile tile in tileList)
            {
                // Up
                if (Math.Abs(currentTile.position.X - tile.position.X) < 1 
                    && Math.Abs(currentTile.position.Y - 32 - tile.position.Y) < 1)
                {
                    neighbourTiles.Add(tile);
                }

                // Right
                if (Math.Abs(currentTile.position.X + 32 - tile.position.X) < 1
                    && Math.Abs(currentTile.position.Y - tile.position.Y) < 1)
                {
                    neighbourTiles.Add(tile);
                }

                // Down
                if (Math.Abs(currentTile.position.X - tile.position.X) < 1
                    && Math.Abs(currentTile.position.Y + 32 - tile.position.Y) < 1)
                {
                    neighbourTiles.Add(tile);
                }

                // Left
                if (Math.Abs(currentTile.position.X - 32 - tile.position.X) < 1
                    && Math.Abs(currentTile.position.Y - tile.position.Y) < 1)
                {
                    neighbourTiles.Add(tile);
                }
            }

            return neighbourTiles;
        }

        public void Draw(SpriteBatch spriteBatch, TextureList textureList)
        {
            foreach (Tile tile in tileList)
            {
                if (tile.isPath)
                {
                    spriteBatch.Draw(textureList.Get("tile-grass"), tile.position, Color.White);
                }
                else
                {
                    spriteBatch.Draw(tile.texture, tile.position, Color.White);
                }
            }
        }
    }
}