using System;
using System.Collections.Generic;

namespace Lesson_1
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }
        public int Temperature { get; set; }
        public WeatherForecast(DateTime _date, int _temperature)
        {
            Date = _date;
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
        public void Add(DateTime date, int temperature)
        {
            foreach(WeatherForecast value in Values)
            {
                if (value.Date == date)
                {
                    return;
                }
            }
            Values.Add(new WeatherForecast(date, temperature));
        }
        public object Get(DateTime dateFrom, DateTime dateTo)
        {
            var list = new List<WeatherForecast>();
            foreach (WeatherForecast value in Values)
            {
                if (value.Date >= dateFrom && value.Date <= dateTo)
                {
                    list.Add(value);
                }
            }
            return list;
        }
        public void Update(DateTime date, int temperature)
        {
            foreach (WeatherForecast value in Values)
            {
                if (value.Date == date)
                {
                    value.Temperature = temperature;
                    return;
                }
            }
        }
        public void Delete(DateTime dateFrom, DateTime dateTo)
        {
            for (int i = 0; i < Values.Count; i++)
            {
                if (Values[i].Date >= dateFrom && Values[i].Date <= dateTo)
                {
                    Values.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
