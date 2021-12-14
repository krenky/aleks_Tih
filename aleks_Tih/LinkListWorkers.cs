using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace aleks_Tih
{
    [Serializable]
    public class LinkListWorkers
    {
        Worker Head;
        Worker Tail;
        int _сount;
        [JsonInclude]
        List<Worker> Workers = new List<Worker>();
        public LinkListWorkers()
        {
            Head = new Worker("Head", "Head", 0);
            Tail = Head;
            Head.next = Tail;
            Count = 0;
        }
        public bool Add(string famil, string position, int salary)//метод добавления работника
        {
            bool check = true;
            Worker prev = Head;
            Worker current = Head.next;
            Worker newWorker = new Worker(famil, position, salary);
            if (Count == 0)
            {
                newWorker.next = Head;
                Head.next = newWorker;
            }
            else
            {
                while (current != Head)
                {
                    if (newWorker.Salary.CompareTo(current.Salary) < 0)
                    {
                        newWorker.next = current;
                        prev.next = newWorker;
                        check = false;
                        break;
                    }
                    else
                    {
                        prev = current;
                        current = current.next;
                    }
                }
                if (check)
                {
                    newWorker.next = Head;
                    prev.next = newWorker;
                }
            }
            Workers.Add(newWorker);
            Workers = Workers.OrderBy(i => i.Salary).ToList<Worker>();
            Count++;
            return true;
        }
        /// <summary>
        /// Метод Добавления
        /// </summary>
        /// <param name="obj">Работник</param>
        /// <returns>True: успешное добавление  False: ошибка добавления</returns>
        public bool Add(Worker obj)
        {
            bool check = true;
            Worker prev = Head;
            Worker current = Head.next;
            Worker newWorker = obj;
            if (Count == 0)
            {
                newWorker.next = Head;
                Head.next = newWorker;
            }
            else
            {
                while (current != Head)
                {
                    if (newWorker.Salary.CompareTo(current.Salary) < 0)
                    {
                        newWorker.next = current;
                        prev.next = newWorker;
                        check = false;
                        break;
                    }
                    else
                    {
                        prev = current;
                        current = current.next;
                    }
                }
                if (check)
                {
                    newWorker.next = Head;
                    prev.next = newWorker;
                }
            }
            Count++;
            return true;
        }
        public bool Delete(string famil)//метод удаления работника
        {
            if (Count != 0)
            {
                Worker current = FindPrevWorker(famil);
                if (current == null)
                {
                    return false;
                }
                else
                {
                    current.next = current.next.next;
                    Count--;
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public Worker FindPrevWorker(string famil)//метод поиска предыдущего искомову работника
        {
            Worker prev = Head;
            Worker current = Head.next;
            while (current != null)
            {
                if (current.Famil == famil)
                {
                    return prev;
                }
                else
                {
                    prev = current;
                    current = current.next;
                }
            }
            return null;
        }
        /// <summary>
        /// проверка на пустоту
        /// </summary>
        public bool IsEmpty { get { return Count == 0; } }
        public int Count { get => _сount; set => _сount = value; }
        public int SumSalary()
        {
            int sum = 0;
            foreach(Worker i in GetWorkers())
            {
                sum = sum + i.Salary;
            }
            return sum;
        }
        /// <summary>
        /// Метод конвертирующий структуру списка в перечисление
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetWorkers()
        {
            Worker Current = Head.next;
            do
            {
                if (Current != null && Current != Head)
                {
                    yield return Current;
                    Current = Current.next;
                }
            }
            while (Current != Head);
        }
        /// <summary>
        /// Метод конвертирующий структуру списка в List
        /// </summary>
        /// <returns></returns>
        public List<Worker> ToList()
        {
            List<Worker> workers = new List<Worker>();
            foreach(Worker i in GetWorkers())
            {
                workers.Add(i);
            }
            return workers;
        }
        public void RestoreStr()
        {
            foreach (Worker i in GetWorkers())
            {
                if (i.Next == null)
                {
                    Tail = i;
                }
            }
        }
    }
}
