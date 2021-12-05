using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.Json.Serialization;

namespace aleks_Tih
{
    [Serializable]
    /// <summary>
    /// Класс офиса.
    /// </summary>
    public class Office
    {
        string adress;
        [JsonInclude]
        public LinkListWorkers workers = new LinkListWorkers();

     

        /// <summary>
        /// For tests
        /// </summary>
        public Office()
        {
            Adress = "0";
        }

        public Office(string house)
        {
            Adress = house;
        }
        //[JsonInclude]
        public List<Worker> Workers
        {
            private get
            {
                return workers.ToList();
            }
            set
            {
                foreach(Worker i in value)
                {
                    workers.Add(i);
                }
            }
        }
        public string Adress { get => adress; set => adress = value; }
        public int Count { get => workers.Count; }
        [JsonIgnore]
        public int AllSalary { get => workers.SumSalary(); }
    }
}
