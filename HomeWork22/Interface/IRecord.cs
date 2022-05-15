using HomeWork22.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWork22.Interface
{
    public interface IRecord
    {
        IEnumerable<Record> GetData();

        void Add(Record record);
    }
}
