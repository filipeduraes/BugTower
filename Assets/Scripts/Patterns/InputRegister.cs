namespace BugTower.Patterns
{
    public abstract class InputRegister
    {
        protected const string HORIZONTAL_INPUT = "Horizontal";
        protected const string VERTICAL_INPUT = "Vertical";

        public abstract void RegisterInput();
    }
}