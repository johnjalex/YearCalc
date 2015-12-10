using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace YearCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"../../dataset.txt");

            List<Person> people = new List<Person>();
            int count = 0;
            foreach (var line in lines)
            {
                count++;
                //String should have 9 characters if input correctly. 2 years and a space.
                if (line.Length != 9)
                {
                    Console.WriteLine("Invalid input on line: " + count);
                    continue;
                }
                
                //Find the space...it should be at index 4.
                int space = line.IndexOf(" ");
                if (space != 4)
                {
                    Console.WriteLine("Invalid input on line: " + count);
                    continue;
                }

                //First number in string.
                string firstYear = line.Substring(0, 4);
                int firstYearInt;
                
                try
                {
                    firstYearInt = int.Parse(firstYear);
                    if (firstYearInt < 1900 || firstYearInt > 2000)
                    {
                        Console.WriteLine("Invalid first year: " + firstYearInt + " on line: " + count);
                        continue;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("First year invalid on line: " + count);
                    continue;
                }

                string secondYear = line.Substring(5);
                //Shouldn't be possible at this point. Sanity check for good measure.
                if (secondYear.Length != 4)
                {
                    Console.WriteLine("Second year not 4 numbers long on line: " + count);
                    continue;
                }

                int secondYearInt;

                try
                {
                    secondYearInt = int.Parse(secondYear);
                    if (secondYearInt < 1900 || secondYearInt > 2000)
                    {
                        Console.WriteLine("Invalid Second Year: " + secondYearInt + " on line: " + count);
                        continue;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Second year invalid on line: " + count);
                    continue;
                }

                if (firstYearInt > secondYearInt)
                {
                    Console.WriteLine("Second year should be larger than first number on line: " + count);
                    continue;
                }

                //Finally input is clear and we have both numbers.
                Person person = new Person(firstYearInt, secondYearInt);
                people.Add(person);
            }

            if (count == 0 || people.Count == 0)
            {
                Console.WriteLine("No valid input found");
                Console.ReadLine();
                return;
            }

            YearMap retVal = FindMostLivedYear(people);

            CreateGraph(retVal);

            Console.WriteLine("Number of People Living in these years: " + retVal.MaxVal);
            Console.WriteLine("Most Lived Years:");

            foreach (int year in retVal.MostLivedYears)
            {
                Console.WriteLine(year);
            }

            Console.ReadLine();
        }

        //Create map of years and find the most lived years.
        public static YearMap FindMostLivedYear(List<Person> people)
        {
            YearMap years = new YearMap();
            
            //Loop through the users and add the years they lived to the map.
            foreach(Person person in people)
            {
                for(int i = person.BirthYear; i <= person.DeathYear; i++)
                {
                    years.AddYear(i);
                }
            }

            //Creating the list in the map of the most lived years
            years.GetMostLivedYears();

            return years;
        }

        //Creating a fun little graph
        public static void CreateGraph(YearMap yearMap)
        {
            //Order the list of keys to get all of the years and find out how many years there were that someone lived in
            var orderedList = yearMap.YearVals.Keys.OrderBy(x => x).ToArray();
            var numberOfYears = orderedList.Count();

            var xvals = new int[numberOfYears];
            var yvals = new int[numberOfYears];
            for (int i = 0; i < yvals.Length; i++)
            {
                yvals[i] = yearMap.YearVals[orderedList[i]];
                xvals[i] = orderedList[i];
            }

            var chart = new Chart();
            chart.Size = new Size(1000, 1000);
            Title title = new Title("Number of people alive each year");
            title.Font = new Font("Calibri", 16, System.Drawing.FontStyle.Bold);
            chart.Titles.Add(title);

            var chartArea = new ChartArea();
            chartArea.AxisX.LabelStyle.Font = new Font("Calibri", 8);
            chartArea.AxisY.LabelStyle.Font = new Font("Calibri", 8);
            chartArea.AxisY.Minimum = 0;
            chartArea.AxisX.Minimum = 1900;
            chartArea.AxisX.Maximum = 2000;
            chartArea.AxisX.Title = "Years";
            chartArea.AxisX.TitleFont = new Font("Calibri", 14, System.Drawing.FontStyle.Bold);
            chartArea.AxisY.Title = "Number of People Alive";
            chartArea.AxisY.TitleFont = new Font("Calibri", 14, System.Drawing.FontStyle.Bold);
            chartArea.AxisY.Interval = 1;
            chartArea.AxisX.Interval = 5;

            chart.ChartAreas.Add(chartArea);

            var series = new Series();
            series.Name = "Series";
            series.ChartType = SeriesChartType.Bar;
            chart.Series.Add(series);

            chart.Series["Series"].Points.DataBindXY(xvals, yvals);

            chart.Invalidate();

            chart.SaveImage("../../Output/chart.png", ChartImageFormat.Png);
        }
        

    }
}
