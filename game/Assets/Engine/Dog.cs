using System.Collections.Generic;

namespace Engine
{
    public class Dog : BaseStepable, IInteract
    {
        /// <summary>
        ///     Создать собачку.
        /// </summary>
        /// <param name="position">Позиция собаки.</param>
        public Dog(Point position)
        {
            _position = position;
            _actions = new List<StepAction>();
        }

        /// <summary>
        ///     Создать собачку.
        /// </summary>
        /// <param name="x">Координата по X.</param>
        /// <param name="y">Координа по Y.</param>
        public Dog(int x, int y)
         : this(new Point(x,y))
        {
        }
        public void Interact(Ball ball)
        {
            _actions.Add(new StepAction(ActionType.BallInteract, null, GetPosition()));
            var newPoint = new Point(_position.X - 1, _position.Y);
            ball.Move(newPoint);
        }

        public override string DebugPrint()
        {
            return "test";
        }
    }
}
