using func_10lab.Domain.Events;
using System;
using System.Collections.Generic;

namespace func_10lab.Data
{
    public interface IEventStore
    {
        void Persist(Event e);
        void Persist(IEnumerable<Event> e);
        IEnumerable<Event> GetEvents(Guid id);
    }
}