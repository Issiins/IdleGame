namespace Player.States
{
    public interface State
    {
        public void OnStateEnter();
        public void OnStateExit();
    }
}