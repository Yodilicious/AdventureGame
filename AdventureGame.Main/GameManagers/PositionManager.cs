using AdventureGame.Main.Characters;
using Microsoft.Xna.Framework;
using MonogameLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.Main.GameManagers
{
    [Flags]
    public enum PositionInScreen
    {
        Left,
        Center,
        Right
    }
    public class PositionManager
    {
        private const int ROW = 1;
        private const int COL = 0;

        private int absoluteXPosition;
        private readonly Dictionary<Vector2, Tile> _level;

        private Player _player;
        private int[] _position;
        private Vector2[] _initialPosition = new Vector2[2]
        {
            new Vector2(100,100),
            new Vector2(30, 10)
        };

        public PositionManager(Dictionary<Vector2, Tile> level, Player player)
        {
            _level = level;
            _player = player;
            _position = new int[2];
            absoluteXPosition = 0;
        }

        private void getPlayerYPositionIndex(bool nowJumping)
        {
            if (nowJumping)
            {
                _position[ROW]--;
            }
            else{
                _position[ROW]++;
            }
        }

        public Vector2 getPosition(MoveDirection direction)
        {
            _position[ROW] = _player.getBoundToCheckCollision().Center.Y / Values.TileHeight;
            _position[COL] = (direction == MoveDirection.Left || direction == MoveDirection.RunningLeft) ?
                (_player.getBoundToCheckCollision().Center.X - absoluteXPosition) / Values.TileWidth - 1 :
                (_player.getBoundToCheckCollision().Center.X - absoluteXPosition) / Values.TileWidth + 1 ;
            int x = getXPosition(direction);
            int y = getYPosition(direction);
            
            return new Vector2(x, y);
        }

        private int getYPosition(MoveDirection direction)
        {
            getPlayerYPositionIndex(_player.NowJumping);
            return (_player.NowJumping) ? checkTop() :checkBottom();
        }

        //private int checkBottom()
        //{
        //    _position[COL] = (_player.CurrentDirection == MoveDirection.Left || _player.CurrentDirection == MoveDirection.RunningLeft) ?
        //        (_player.getBoundToCheckCollision().X + Values.TileWidth / 2 )/ Values.TileWidth : 
        //        (_player.getBoundToCheckCollision().X - Values.TileWidth / 2) / Values.TileWidth + 1;
        //    KeyValuePair<Vector2, Tile> tile = getTile(true);

        //    int temp =  (tile.Value == null) ?
        //        Values.FallenSpeed :
        //        (int)Math.Min(Values.FallenSpeed, tile.Key.Y - (_player.getBoundToCheckCollision().Y + _player.getBoundToCheckCollision().Height));

        //    if (temp == 0)
        //    {
        //        _player.IsDroped = false;
        //    }
        //    return temp;
        //}

        private int checkBottom()
        {
            int[] temp = new int[2];
            KeyValuePair<Vector2, Tile> tile;

            for (int i = 0; i < 2; i++)
            {
                _position[COL] = (_player.getBoundToCheckCollision().X + _player.getBoundToCheckCollision().Width * i - absoluteXPosition) / Values.TileWidth;
                tile = getTile();
                temp[i] = (tile.Value == null) ?
                    Values.JumpingSpeed :
                    (int)Math.Min(Values.JumpingSpeed, tile.Key.Y - (_player.getBoundToCheckCollision().Y + _player.getBoundToCheckCollision().Height));
                if (temp[i] == 0)
                {
                    _player.NowJumping = false;
                    _player.IsDroped = false;
                    break;
                }
                else
                {
                    _player.IsDroped = true;
                }
            }
            return Math.Min(temp[0], temp[1]);
        }
        private int checkTop()
        {
            int[] temp = new int[2];
            KeyValuePair<Vector2, Tile> tile;

            for(int i = 0; i < 2; i++)
            {
                _position[COL] = (_player.getBoundToCheckCollision().X - absoluteXPosition + _player.getBoundToCheckCollision().Width * i)/ Values.TileWidth;
                tile = getTile();
                temp[i] = (tile.Value == null) ?
                    -Values.JumpingSpeed :
                    -(int)Math.Min(Values.JumpingSpeed, _player.getBoundToCheckCollision().Y - (tile.Key.Y + Values.TileHeight));
                if (temp[i] == 0)
                {
                    _player.NowJumping = false;
                    _player.IsDroped = true;
                    break;
                }
            }
            
            return Math.Max(temp[0], temp[1]);
        }

        private int checkRight()
        {
            KeyValuePair<Vector2, Tile> tile = getTile();
            return (tile.Value == null) ?
                ((_player.NowJumping || _player.IsDroped) ? Values.JumpingSpeed : Values.MoveSpeed) :
                (int)Math.Min(Values.MoveSpeed, tile.Key.X - (_player.getBoundToCheckCollision().X + _player.getBoundToCheckCollision().Width));
        }

        private int checkLeft()
        {
            KeyValuePair<Vector2, Tile> tile = getTile();
            return -((tile.Value == null) ?
                ((_player.NowJumping || _player.IsDroped) ?  Values.JumpingSpeed: Values.MoveSpeed) :
                (int)Math.Min(Values.MoveSpeed, _player.getBoundToCheckCollision().X - absoluteXPosition- (tile.Key.X + Values.TileWidth)));
        }

        private KeyValuePair<Vector2, Tile> getTile(bool isDown = false)
        {
            return (isDown) ?_level.SingleOrDefault(a => a.Key == new Vector2(_position[COL] * Values.TileWidth, _position[ROW] * Values.TileHeight)) :
            _level.SingleOrDefault(a => a.Key == new Vector2(_position[COL] * Values.TileWidth, _position[ROW] * Values.TileHeight));
        }

        private int getXPosition(MoveDirection direction)
        {
            int xPosition = 0;

            if (direction == MoveDirection.Left || direction == MoveDirection.RunningLeft)
            {
                xPosition = checkLeft();
            }
            if (direction == MoveDirection.Right || direction == MoveDirection.RunningRight)
            {
                xPosition = checkRight();
            }
            return xPosition;
        }

        public int convertToBackgroundDistance(Vector2 dist)
        {
            int xDist = 0;
            switch (getPlayerPositionInScreen())
            {
                case PositionInScreen.Left:
                    if (absoluteXPosition < 0 && dist.X < 0)
                        xDist -= (int)dist.X;
                    break;
                case PositionInScreen.Center:
                    break;
                case PositionInScreen.Right:
                    if (dist.X > 0)
                        xDist -= (int)dist.X;
                    break;
                default:
                    break;
            }
            absoluteXPosition += xDist;
            if (absoluteXPosition > 0)
                absoluteXPosition = 0;
            
            return xDist;
        }

        private PositionInScreen getPlayerPositionInScreen()
        {
            if (_player.getBoundToCheckCollision().X < Utility.Stage.X * 3 / 10)
                return PositionInScreen.Left;
            if (_player.getBoundToCheckCollision().X > Utility.Stage.X * 7 / 10)
                return PositionInScreen.Right;
            return PositionInScreen.Center;
        }


        public Vector2 getInitialPosition(int stage)
        {
            return _initialPosition[stage];
        }

        public void Clear()
        {
            absoluteXPosition = 0;
            _level.Clear();
        }
    }
}
