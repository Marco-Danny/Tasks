namespace Tasks
{
    public interface State
    {
        void InProgress();
        void Done();
        void Remove();
    }
}