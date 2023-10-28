using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace MazeProgram3
{

    internal class MazeProgram
    {
        public static void Main(string[] args)
        {
            char[,] mazeFile = {    // 다른 미로 사용시 mazeFIle만 변경, 반드시 시작점은 0,0 & 도착점은 row-1, col-1
                { '0','1','1','1','1','1','1','1','1','1'},
                { '0','0','0','0','1','0','0','0','0','1'},
                { '1','0','0','0','1','0','0','1','0','1'},
                { '1','0','1','1','1','0','0','1','0','1'},
                { '1','0','0','0','1','0','0','1','0','1'},
                { '1','0','1','0','1','0','0','1','0','1'},
                { '1','0','1','0','1','0','0','1','0','1'},
                { '1','0','1','0','1','0','0','1','0','1'},
                { '1','0','1','0','0','0','0','1','0','0'},
                { '1','1','1','1','1','1','1','1','1','0'}
            };
            MazeRoom maze = new MazeRoom(mazeFile);
            maze.printMaze();
            maze.findMaze();
            maze.printResult();
        }
    }
    class MazeRoom
    {
        PositionXY start;
        PositionXY destination;
        Stack ps;
        int[,] room;
        int row;
        int col;
        public MazeRoom(char[,] file)
        {
            row = file.GetLength(0);
            col = file.GetLength(1);
            start = new PositionXY(0, 0);
            destination = new PositionXY(row - 1, col - 1);
            ps = new Stack();
            room = new int[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    room[i, j] = (int)(file[i, j] - '0');
                }
            }
            room[0, 0] = 3;
        }
        public void findMaze()
        {
            PositionXY traveler = start;
            while (isNotArrived(traveler))
            {
                Console.WriteLine("Move to (" + traveler.x + ", " + traveler.y + ")");

                findAndPush(traveler);
                traveler = moveAndCheck(traveler);
            }
            Console.WriteLine("- Success -");
            room[row - 1, col - 1] = 4;
        }
        void findAndPush(PositionXY traveler)
        {
            PositionXY north = new PositionXY(traveler.x - 1, traveler.y);
            PositionXY west = new PositionXY(traveler.x, traveler.y - 1);
            PositionXY south = new PositionXY(traveler.x + 1, traveler.y);
            PositionXY east = new PositionXY(traveler.x, traveler.y + 1);
            PositionXY[] directions = new PositionXY[4];
            directions[0] = west;
            directions[1] = north;
            directions[2] = south;
            directions[3] = east;
            if (traveler.y == 0) { directions[0] = null; }   // if traveler의 (y = 0) !west
            if (traveler.x == 0) { directions[1] = null; }   // if (x = 0) !north
            if (traveler.y == col - 1) { directions[3] = null; }   // if (y = col-1) !east
            if (traveler.x == row - 1) { directions[2] = null; } // if (x = row-1) !south
            foreach (PositionXY tmp in directions)
            {
                if (tmp != null)
                {
                    if (room[tmp.x, tmp.y] == 0)
                    {
                        ps.Push(tmp);
                    }
                }
            }
        }
        PositionXY moveAndCheck(PositionXY traveler)
        {
            if (ps.Count == 0)
            {
                Console.WriteLine("not Found");
                printMaze();
                Environment.Exit(0);
            }            
                PositionXY tmp = (PositionXY)ps.Pop();                
                room[tmp.x, tmp.y] = 2;
                return tmp;
            
        }
        public void printMaze()
        {
            for (int i = 0; i < room.GetLength(0); i++)
            {
                for (int j = 0; j < room.GetLength(1); j++)
                {
                    if (room[i, j] == 3)
                    {
                        Console.Write(0 + " ");
                    }
                    else {
                        Console.Write(room[i, j] + " ");
                    }
                }
                Console.WriteLine();
            }
        }
        public void printResult()
        {
            char[,] result = new char[row, col];

            for (int i = 0; i < room.GetLength(0); i++)
            {
                for (int j = 0; j < room.GetLength(1); j++)
                {
                    if (room[i,j] == 0)
                    {
                        result[i, j] = ' ';
                    }
                    else if (room[i,j] == 1)
                    {
                        result[i, j] = 'X';
                    }else if (room[i,j] == 2)
                    {
                        result[i, j] = '*';
                    }else if (room[i,j] == 3)
                    {
                        result[i, j] = 'S';
                    }else if (room[i,j] == 4)
                    {
                        result[i, j] = 'D';
                    }
                    else { }
                }
            }
            for (int i = 0; i < room.GetLength(0); i++)
            {
                for (int j = 0; j < room.GetLength(1); j++)
                {
                    Console.Write(result[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        bool isNotArrived(PositionXY traveler)
        {
            if (traveler.x != destination.x && traveler.y != destination.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    class PositionXY
    {
        public int x;
        public int y;
        public PositionXY(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}