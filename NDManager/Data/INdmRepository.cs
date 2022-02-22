﻿using NDManager.Data.Models;
using System.Collections.Generic;

namespace NDManager.Data
{
    public interface INdmRepository<T> where T : class
    {
        IEnumerable<Group> GetAllGroups();
        IEnumerable<Kid> GetAllKids();
        IEnumerable<Kid> GetAllKidsByGroup(Group group);
        T GetById(object id);
        bool SaveAll();
        void Insert(T obj);
    }
}
