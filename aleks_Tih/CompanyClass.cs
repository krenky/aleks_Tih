
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Windows;

namespace aleks_Tih
{
    public class Comp
    {
        int _first = -1;
        int _tail = -1;
        public int Count;
        Office[] Company;

        public Comp(int IntCount)
        {
            Company = new Office[IntCount];
            Count = 0;
            First = 0;
        }

        public Office[] Company1 { get => Company; set => Company = value; }
        public int Tail { get => _tail; set => _tail = value; }
        public int First { get => _first; set => _first = value; }

        /// <summary>
        /// вставляет элементы высчитавая индекс по формуле
        ///  ((индекс пос.элемента+1) % count).
        /// </summary>
        /// <param name="House">Адрес офиса</param>
        /// <returns>True - при успешном добавление</returns>
        public bool Add(string House)
        {
            Office auto = new Office(House);
            if (Count != Company.Length)
            {
                if (Tail != First)
                {
                    Tail = (Tail + 1) % Company.Length;
                    Company[Tail] = auto;
                    Count++;
                    return true;
                }
                else
                {
                    if (Count == 0)
                    {
                        Company[Tail] = auto;
                        Count++;
                        return true;
                    }
                    Tail = (Tail + 1) % Company.Length;
                    Company[Tail] = auto;
                    Count++;
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public bool Add(Office auto)
        {
            if (Count != Company.Length)
            {
                if (Tail != First)
                {
                    Tail = (Tail + 1) % Company.Length;
                    Company[Tail] = auto;
                    Count++;
                    return true;
                }
                else
                {
                    if (Count == 0)
                    {
                        Company[Tail] = auto;
                        Count++;
                        return true;
                    }
                    Tail = (Tail + 1) % Company.Length;
                    Company[Tail] = auto;
                    Count++;
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Удаление элемента из очереди
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            if (Count == 0)
                return false;
            if (Count != 1)
            {
                Company[First] = null;
                First = (First + 1) % Company.Length;
                Count--;
                return true;
            }
            else
            {
                Company[First] = null;
                Count--;
                return true;
            }
        }
        /// <summary>
        /// Метод поиска офиса
        /// </summary>
        /// <param name="House">Адрес(номер дома)</param>
        /// <returns>Объект типа Office</returns>
        public Office Search(string House)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Company[i].Adress == House)
                {
                    return Company[i];
                }
            }
            return null;
        }
        /// <summary>
        /// метод вывода информации об офисе
        /// </summary>
        /// <param name="Index"></param>
        /// <returns>информация об Офисе</returns>
        public string Print(int Index)
        {
            if (Index != -1)
            {
                string Info = "Адрес: " + Company[Index].Adress + " Количество работников:" + Company[Index].workers.Count;
                return Info;
            }
            else
            {
                return "Not found";
            }
        }
        /// <summary>
        /// метод вывода суммы всех Зарплат
        /// </summary>
        /// <returns></returns>
        public int Sum()
        {
            int sum = 0;
            foreach (Office i in GetOffices())
            {
                sum = sum + i.workers.SumSalary();
            }
            return sum;
        }
        /// <summary>
        /// метод перечисления
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetOffices()
        {
            int current = First;
            yield return Company[current];
            current = (current + 1) % Company.Length;
            do
            {
                if (Company[current] != null)
                {
                    yield return Company[current];
                    current = (current + 1) % Company.Length;
                }
                else break;
            } while (current != First);
        }
        //[STAThread]
        //public bool Save()
        //{
        //    SaveFileDialog saveFileDialog = new SaveFileDialog();
        //    //ConvertToLDepartment();
        //    //if ((bool)saveFileDialog.ShowDialog())
        //    //    using (FileStream fs = (FileStream)saveFileDialog.OpenFile())
        //    //    {
        //    //        JsonSerializer.Serialize<List<LDepartment>>(new Utf8JsonWriter(fs), lDepartments);
        //    //    }
        //    if ((bool)saveFileDialog.ShowDialog())
        //    {
        //        using (FileStream fs = (FileStream)saveFileDialog.OpenFile())
        //        {
        //            JsonSerializer.Serialize<Office[]>(new Utf8JsonWriter(fs), Company);
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        [STAThread]
        public bool Save(FileStream fileStream)
        {
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            BinaryFormatter formatter = new BinaryFormatter();
            //ConvertToLDepartment();
            //if ((bool)saveFileDialog.ShowDialog())
            //    using (FileStream fs = (FileStream)saveFileDialog.OpenFile())
            //    {
            //        JsonSerializer.Serialize<List<LDepartment>>(new Utf8JsonWriter(fs), lDepartments);
            //    }
                //using (FileStream fs = (FileStream)saveFileDialog.OpenFile())
                //{
                    formatter.Serialize(fileStream, Company);
                    return true;
        }
        public void Load(FileStream fileStream)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Office[] offices1 = new Office[10];
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //if ((bool)openFileDialog.ShowDialog())
            //{
                try
                {
                    //using (FileStream fs = (FileStream)openFileDialog.OpenFile())
                    //{
                        offices1 = (Office[])formatter.Deserialize(fileStream);
                        
                    //}
                    Company = new Office[Company.Length];
                    Count = 0;
                    First = 0;
                    Count = 0;
                    foreach (var i in offices1)
                    {
                        if (i != null)
                        {
                            i.workers.RestoreStr();
                            Add(i);
                        }
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            //}
        }
    }
}
