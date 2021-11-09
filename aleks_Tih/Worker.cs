using System;
using System.Text.Json.Serialization;

namespace aleks_Tih
{
    [Serializable]
    /// <summary>
    /// Класс DoublyNode является основой для замкнутого
    /// Однонаправленного списка
    /// </summary>
    /// <typeparam name="string">Тип данных заголовков</typeparam>
    public class Worker
    {
        /// <summary>
        /// конструктор DoublyNode
        /// </summary>
        /// <param name="famil">Фамилия</param>
        /// <param name="position">Должность</param>
        /// <param name="salary">Оклад</param>
        public Worker(string famil, string position, int salary)
        {
            Famil = famil;
            Position = position;
            Salary = salary;
        }
        public Worker(string famil, string position)
        {
            Famil = famil;
            Position = position;
        }

        public Worker()
        {
        }

        private string _famil;
        private string _position;
        private int _salary;
        public Worker next;
        public string Famil { get => _famil; set => _famil = value; }
        [JsonIgnore]
        public Worker Next
        {
            get
            {
                if (next.Famil != "Head")
                {
                    return next;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                next = value;
            }
        }
        public string Position { get => _position; set => _position = value; }
        public int Salary { get => _salary; set => _salary = value; }
    }
}
