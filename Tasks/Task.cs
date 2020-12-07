using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Tasks
{
    [DataContract]
    [KnownType(typeof(StateDone))]
    [KnownType(typeof(StateNew))]
    [KnownType(typeof(StateInProgress))]
    
    public class Task : State
    {
        [DataMember]
        public string Description { get; set; }
        
        [DataMember]
        public DateTime CompletionDate { get; set; }
        
        [DataMember]
        public string Priority { get; set; }
        
        [DataMember]
        public State _status;

        public Task()
        {
        }

        public Task(string description, DateTime completionDate, string priority)
        {
            Description = description;
            CompletionDate = completionDate;
            Priority = priority;
            _status = new StateNew(this);
        }

        public Task(string description, string completionDate, string priority, string state)
        {
            var newState = new StateNew(this);
            var inProgressState = new StateInProgress(this);
            var doneState = new StateDone();

            Description = description;
            CompletionDate = convertToDateTime(completionDate);
            Priority = priority;

            switch (state)
            {
                case "новая":
                    _status = newState;
                    break;
                case "в работе":
                    _status = inProgressState;
                    break;
                case "сделано":
                    _status = doneState;
                    break;
            }
        }

        public void ChangeState(State other_status) => _status = other_status;

        public void InProgress() => _status.InProgress();

        public void Done() => _status.Done();

        public void Remove(List<Task> tasks) => _status.Remove(tasks);

        private DateTime convertToDateTime(string value)
        {
            DateTime convertedDate;
            try
            {
                convertedDate = Convert.ToDateTime(value);
                Console.WriteLine("'{0}' converts to {1} {2} time.",
                    value, convertedDate,
                    convertedDate.Kind.ToString());
                return convertedDate;
            }
            catch (FormatException)
            {
                Console.WriteLine("'{0}' is not in the proper format.", value);
            }

            return default;
        }

        public string ToString()
        {
            return Description;
        }

        public void SetState()
        {
            if (_status.ToString() == "новая")
            {
                _status = new StateNew(this);
            }
            else if(_status.ToString() == "в работе")
            {
                _status = new StateInProgress(this);
            }
            else
            {
                _status = new StateDone();
            }
        }
    }
}