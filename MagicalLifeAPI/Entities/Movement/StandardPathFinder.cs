﻿using DijkstraAlgorithm.Graphing;
using DijkstraAlgorithm.Pathing;
using MagicalLifeAPI.World;
using System.Linq;

namespace MagicalLifeAPI.Entities.Movement
{
    /// <summary>
    /// A class that handles the construction of the graph used to do optimal pathfinding.
    /// </summary>
    public static class StandardPathFinder
    {
        /// <summary>
        /// Holds data that describes which tiles connect to which tiles.
        /// This graph contains data used to pathfind for the standard movement.
        /// </summary>
        private static GraphBuilder tileConnectionGraph = new GraphBuilder();

        private static Graph builtGraph;

        /// <summary>
        /// Used to determine the fastest route between two points.
        /// </summary>
        private static PathFinder pathFinder;

        /// <summary>
        /// Returns the fastest route between the source and destination tiles.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static Path GetFastestPath(Tile source, Tile destination)
        {
            Path path = pathFinder.FindShortestPath(
                StandardPathFinder.builtGraph.Nodes.Single(node => node.Id == source.Location.ToString()),
                StandardPathFinder.builtGraph.Nodes.Single(node => node.Id == destination.Location.ToString()));
            return path;
        }

        /// <summary>
        /// Populates the <see cref="tileConnectionGraph"/> with data.
        /// This should be called once after the world is generated.
        /// </summary>
        /// <param name="world"></param>
        public static void BuildPathGraph(World.World world)
        {
            StandardPathFinder.AddNodes(world);
            StandardPathFinder.AddLinkes(world);
            StandardPathFinder.builtGraph = StandardPathFinder.tileConnectionGraph.Build();
            StandardPathFinder.pathFinder = new PathFinder(StandardPathFinder.builtGraph);
        }

        /// <summary>
        /// Creates connections between tiles in the <see cref="tileConnectionGraph"/>.
        /// </summary>
        /// <param name="world"></param>
        private static void AddLinkes(World.World world)
        {
            Tile[,,] tiles = world.Tiles;
            int xSize = tiles.GetLength(0);
            int ySize = tiles.GetLength(1);
            int zSize = tiles.GetLength(2);

            int x = 0;
            int y = 0;
            int z = 0;

            //Iterate over each row.
            for (int i = 0; i < xSize; i++)
            {
                //Iterate over each column
                for (int ii = 0; ii < ySize; ii++)
                {
                    //Iterate over the depth of each tile in the z axis.
                    for (int iii = 0; iii < zSize; iii++)
                    {
                        //Each tile can be accessed by the xyz coordinates from this inner loop properly.
                        StandardPathFinder.AddNeighborLink(1, 0, 0, tiles, tiles[x, y, z]);
                        StandardPathFinder.AddNeighborLink(-1, 0, 0, tiles, tiles[x, y, z]);
                        StandardPathFinder.AddNeighborLink(0, 1, 0, tiles, tiles[x, y, z]);
                        StandardPathFinder.AddNeighborLink(0, -1, 0, tiles, tiles[x, y, z]);
                        StandardPathFinder.AddNeighborLink(1, 1, 0, tiles, tiles[x, y, z]);
                        StandardPathFinder.AddNeighborLink(1, -1, 0, tiles, tiles[x, y, z]);
                        StandardPathFinder.AddNeighborLink(-1, 1, 0, tiles, tiles[x, y, z]);
                        StandardPathFinder.AddNeighborLink(-1, -1, 0, tiles, tiles[x, y, z]);
                        z++;
                    }
                    y++;
                    z = 0;
                }
                y = 0;
                x++;
            }
        }

        /// <summary>
        /// Adds a link to a neighboring tile if the tile exists/is in bounds.
        /// </summary>
        /// <param name="xChange"></param>
        /// <param name="yChange"></param>
        /// <param name="zChange"></param>
        private static void AddNeighborLink(int xChange, int yChange, int zChange, Tile[,,] tiles, Tile source)
        {
            int x = (int)source.Location.X;
            int y = (int)source.Location.Y;
            int z = (int)source.Location.Z;

            if (x + xChange > -1 && x + xChange < tiles.GetLength(0))
            {
                x += xChange;
            }
            else
            {
                //The neighboring tile didn't exist.
                return;
            }

            if (y + yChange > -1 && y + yChange < tiles.GetLength(1))
            {
                y += yChange;
            }
            else
            {
                //The neighboring tile didn't exist.
                return;
            }

            if (z + zChange > -1 && z + zChange < tiles.GetLength(2))
            {
                z += zChange;
            }
            else
            {
                //The neighboring tile didn't exist.
                return;
            }

            StandardPathFinder.tileConnectionGraph.AddLink(source.Location.ToString(), tiles[x, y, z].Location.ToString(), 101 - tiles[x, y, z].MovementCost);
        }

        /// <summary>
        /// Adds tiles as nodes into the <see cref="tileConnectionGraph"/>.
        /// </summary>
        /// <param name="world"></param>
        private static void AddNodes(World.World world)
        {
            Tile[,,] tiles = world.Tiles;
            int xSize = tiles.GetLength(0);
            int ySize = tiles.GetLength(1);
            int zSize = tiles.GetLength(2);

            int x = 0;
            int y = 0;
            int z = 0;

            //Iterate over each row.
            for (int i = 0; i < xSize; i++)
            {
                //Iterate over each column
                for (int ii = 0; ii < ySize; ii++)
                {
                    //Iterate over the depth of each tile in the z axis.
                    for (int iii = 0; iii < zSize; iii++)
                    {
                        //Each tile can be accessed by the xyz coordinates from this inner loop properly.
                        StandardPathFinder.tileConnectionGraph.AddNode(tiles[x, y, z].Location.ToString());
                        z++;
                    }
                    y++;
                    z = 0;
                }
                y = 0;
                x++;
            }
        }
    }
}