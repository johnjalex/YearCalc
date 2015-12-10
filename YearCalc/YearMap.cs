using System.Collections.Generic;

namespace YearCalc
{
    class YearMap
    {
        //Dictionary of each year and how many times a person was alive in that year
        private readonly Dictionary<int, int> yearVals;

        //The years that the most people lived in
        private readonly List<int> mostLivedYears;

        //The max number of times people lived in a year
        private int maxVal;

        public YearMap()
        {
            yearVals = new Dictionary<int,int>();
            mostLivedYears = new List<int>();
            maxVal = 0;
        }

        //Add a year to the dictionary if it doesn't exist and set value to 1.  
        //If it's there, increment the count.
        public void AddYear(int year)
        {
            if(!yearVals.ContainsKey(year))
            {
                yearVals.Add(year, 1);
            }
            else
            {
                yearVals[year]++;
            }
        }

        //Find the years that are lived in the most.  
        //Loop through year dictionary and check the count on each.
        //If count > max value, clear out list of most lived years and set new max val.
        //If count == Max val, add year to list of most lived years
        //If count < max val, do nothing
        public void GetMostLivedYears()
        {
            foreach (int year in yearVals.Keys)
            {
                if (yearVals[year] > maxVal)
                {
                    mostLivedYears.Clear();
                    mostLivedYears.Add(year);

                    maxVal = yearVals[year];
                }
                else if (yearVals[year] == maxVal)
                {
                    mostLivedYears.Add(year);
                }
           }
        }

        public Dictionary<int,int> YearVals 
        {
            get
            {
                return yearVals;
            }
        }

        public List<int> MostLivedYears
        {
            get
            {
                return mostLivedYears;
            }
        }

        public int MaxVal
        {
            get
            {
                return maxVal;
            }
        }
    }
}
