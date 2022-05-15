using HomeWork22.DataContext;
using HomeWork22.Interface;
using HomeWork22.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWork22.Data
{
    public class DataDb : IRecord
    {
        private readonly RecordDbContext Context;

        public DataDb(RecordDbContext context)
        {
            Context = context;
        }
        public void Add(Record record)
        {
            Context.Records.Add(record);
            Context.SaveChanges();
        }
        public IEnumerable<Record> GetData()
        {
            return Context.Records;
        }
    }
}
