using System;
using System.Collections.Generic;

namespace Lesson_1
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }
        public int Temperature { get; set; }
        public WeatherForecast(string _date, int _temperature)
        {
            Date = Convert.ToDateTime(_date);
            Temperature = _temperature;
        }
    }
    public class ValuesHolder
    {
        public List<WeatherForecast> Values { get; set; }
        public ValuesHolder()
        {
            Values = new List<WeatherForecast>();
        }
        public void Add(string date, int temperature)
        {
            foreach(WeatherForecast value in Values)
            {
                if (value.Date == Convert.ToDateTime(date))
                {
                    throw new Exception($"Ошибка: дата {date} уже существует.");
                }
            }
            Values.Add(new WeatherForecast(date, temperature));
        }
        public object Get(string dateFrom, string dateTo)
        {
            var list = new List<WeatherForecast>();
            foreach (WeatherForecast value in Values)
            {
                if (value.Date >= Convert.ToDateTime(dateFrom) && value.Date <= Convert.ToDateTime(dateTo))
                {
                    list.Add(value);
                }
            }
            if (list.Count > 0)
            {
                return list;
            }
            throw new Exception($"Ошибка: данных за промежуток {dateFrom}-{dateTo} не существует.");
        }
        public void Update(string date, int temperature)
        {
            foreach (WeatherForecast value in Values)
            {
                if (value.Date == Convert.ToDateTime(date))
                {
                    value.Temperature = temperature;
                    return;
                }
            }
            throw new Exception($"Ошибка: данных за дату {date} не существует.");
        }
        public void Delete(string date)
        {
            foreach (WeatherForecast value in Values)
            {
                if (value.Date == Convert.ToDateTime(date))
                {
                    Values.Remove(value);
                    return;
                }
            }
            throw new Exception($"Ошибка: данных за дату {date} не существует.");
        }
    }
}
