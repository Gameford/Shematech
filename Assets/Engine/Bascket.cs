using System;
using System.Collections.Generic;

namespace Engine
{
    public class Bascket : BaseStepable, IInteract
    {
        public Color Color { get; set; }
        public int CountBall { get; set; }
        
        public int Score { get; set; }
        
        /// <summary>
        ///     Создать новую корзину.
        /// </summary>
        /// <param name="color">Цвет шариков попадающих в корзину.</param>
        /// <param name="position">Позиция шарика.</param>
        public Bascket(Color color, Point position)
        {
            Color = color;
            _position = position;
            _actions = new List<StepAction>();
        }

        /// <summary>
        ///     Создать новую корзину.
        /// </summary>
        /// /// <param name="color">Цвет шариков попадающих в корзину.</param>
        /// <param name="x">Координата по X.</param>
        /// <param name="y">Координа по Y.</param>
        public Bascket(Color color, int x, int y)
            : this(color, new Point(x, y))
        {
        }
        
        public void Interact(Ball ball)
        {
            if (Color.Equals(ball.Color))
            {
                _actions.Add(new StepAction(ActionType.BascketColorMatched, null, GetPosition()));
                Score++;
            }
            else
            {
                Score--;
                _actions.Add(new StepAction(ActionType.BascketColorMissing, null, GetPosition()));   
            }

            ball.GetActions().Add(new StepAction(ActionType.BallConsumed, this, GetPosition()));
        }

        public override string DebugPrint()
        {
            return String.Format("\\{0}|{1}/", Color.ShortFormat(), Score).Center(10);
        }
    }
}