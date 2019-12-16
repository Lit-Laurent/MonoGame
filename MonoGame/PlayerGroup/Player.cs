using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Managers;
using MonoGame.Models;

namespace MonoGame
{
    class Player
    {
        #region Fields

        protected AnimationManager _animationManager;

        protected Dictionary<string, Animation> _animations;

        protected Vector2 _position;

        protected Texture2D _texture;

        public Vector2 Velocity;

        public float Speed = 2.5f;

        #endregion

        #region Properties

        public Input Input;

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;

                if (_animationManager != null)
                    _animationManager.Position = _position;
            }
        }

        public string dir = "right";

        #endregion

        #region Methods

        public virtual void Draw(SpriteBatch spriteBatch, int scale = 1)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, Position, Color.White);
            else if (_animationManager != null)
                _animationManager.Draw(spriteBatch, scale);
            else throw new Exception("Error");
        }

        public virtual void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Input.Left))
                Velocity.X = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Right))
                Velocity.X = Speed;
            //Velocity.Y = physics.gravity;
        }

        protected virtual void SetAnimations()
        {
            if (Velocity.X > 0)
            {
                _animationManager.Play(_animations["playerrunright"]);
                dir = "right";
            }
            else if (Velocity.X < 0)
            {
                _animationManager.Play(_animations["playerrunleft"]);
                dir = "left";
            }
            else if (dir == "right") _animationManager.Play(_animations["playeridleright"]);
            else if (dir == "left") _animationManager.Play(_animations["playeridleleft"]);
        }

        public Player(Dictionary<string, Animation> animations)
        {
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);
        }

        public Player(Texture2D texture)
        {
            _texture = texture;
        }

        public virtual void Update(GameTime gameTime, List<Player> sprites)
        {
            Move();

            SetAnimations();

            _animationManager.Update(gameTime);

            Position += Velocity;
            Velocity = Vector2.Zero;
        }

        #endregion
    }
}
