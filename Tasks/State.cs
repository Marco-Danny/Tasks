using System.Collections.Generic;

namespace Tasks
{
    public interface State
    {
        void InProgress();
        void Done();
        void Remove(List<Task> tasks, Task task);
    }
}