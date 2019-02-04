using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Engine
{
    [System.Serializable]
    /// <summary>
    ///     Шарик.
    /// </summary>
    public class Ball : BaseStepable, ICloneable
    {
        protected bool Equals(Ball other)
        {
            return ID.Equals(other.ID);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Ball) obj);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        /// <summary>
        ///     Цвет шарика.
        /// </summary>
        public Color Color;

        /// <summary>
        ///     Уникальный GUID для шарика, необходим для отслеживания истории.
        /// </summary>
        public Guid ID;

        /// <summary>
        ///     Создать новый экземпляр шарика.
        /// </summary>
        /// <param name="color">Цвет шарика.</param>
        /// <param name="position">Позиция шарика.</param>
        public Ball(Color color, Point position, Guid id)
        {
            Color = color;
            _position = position;
            ID = id;
            _actions = new List<StepAction>();
            
            BallHistory.GetInstance().AddBall(this);
        }

        /// <summary>
        ///     Создать новый экземпляр шарика.
        /// </summary>
        /// <param name="color">Цвет шарика.</param>
        /// <param name="x">Координата по X.</param>
        /// <param name="y">Координа по Y.</param>
        public Ball(Color color, int x, int y)
            : this(color, new Point(x, y), Guid.NewGuid())
        {
        }

        /// <summary>
        ///     Создать новый экземпляр шарика.
        ///     Шарик будет иметь начальные значения:
        ///         Цвет - Green;
        ///         Позиция - X: 0, Y: 0;
        /// </summary>
        public Ball(Guid id) : this(default(Color), default(Point), id)
        {
        }
        
        /// <summary>
        ///     Создать новый экземпляр шарика.
        ///     Шарик будет иметь начальные значения:
        ///         Цвет - Green;
        ///         Позиция - X: 0, Y: 0;
        /// </summary>
        public Ball() : this(default(Color), default(Point), Guid.NewGuid())
        {
        }

        /// <summary>
        ///     Склонировать текщий шарик.
        /// </summary>
        /// <returns>Новый обьект шарика.</returns>
        public object Clone()
        {
            var newBall = new Ball(ID);
            newBall.SetPosition(GetPosition());
            newBall.Color = Color;
            newBall._actions = _actions.ToList();

            return newBall;
        }

        public override string DebugPrint()
        {
            return string.Format("B-{0}", Color.ShortFormat()).Center(10);
        }

        public override void Step(IList<IStepable> oldWorld, IList<IStepable> newWorld)
        {
            if (_actions.Last().Type == ActionType.BallConsumed)
            {
                return;
            }
            
            var ball = Clone() as Ball;
            Debug.Assert(ball != null, "ball != null");

            var otherBlock = Game.GetByPos(ball.GetPosition(), oldWorld) as IInteract;
            
            if (otherBlock != null)
            {
                ball.GetActions().Add(new StepAction(ActionType.BallInteract, otherBlock, ball.GetPosition()));
                otherBlock.Interact(ball);
            }
            else
            {
                ball.Move(0, 1);
                ball.GetActions().Add(new StepAction(ActionType.BallMove, null, ball.GetPosition()));
            }

            newWorld.Add(ball);
        }

        /// <summary>
        ///     Сдвинуть шарик на определенное кол-во клеток.
        /// </summary>
        /// <param name="offsetX">Сдвиг по X.</param>
        /// <param name="offsetY">Сдвиг по Y.</param>
        public void Move(int offsetX, int offsetY)
        {
            _position.X += offsetX;
            _position.Y += offsetY;
        }
        
        /// <summary>
        ///     Сдвинуть шарик на определенное кол-во клеток.
        /// </summary>
        /// <param name="point">Вектор движения.</param>
        public void Move(Point point)
        {
            _position.X += point.X;
            _position.Y += point.Y;
        }
    }
}