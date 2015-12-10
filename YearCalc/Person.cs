namespace YearCalc
{
    //Object to represent a person with their birth and death year
    class Person
    {
        public int BirthYear { get; set;}
        public int DeathYear { get; set;}

        public Person(int birthYear, int deathYear)
        {
            BirthYear = birthYear;
            DeathYear = deathYear;
        }

    }
}
