namespace SD.pesel
{
    public class PersonId
    {
        private readonly string _id;

        public PersonId(string id)
        {
            if (id.Length != 11 || !long.TryParse(id, out _))
            {
                throw new ArgumentException("PESEL must be an 11-digit number.");
            }
            _id = id;
        }

        /// <summary>
        /// Get full year from PESEL
        /// </summary>
        public int GetYear()
        {
            int year = int.Parse(_id.Substring(0, 2));
            int month = int.Parse(_id.Substring(2, 2));

            if (month > 80) // 1800s
                return year + 1800;
            if (month > 60) // 2200s
                return year + 2200;
            if (month > 40) // 2100s
                return year + 2100;
            if (month > 20) // 2000s
                return year + 2000;
            // 1900s
            return year + 1900;
        }

        /// <summary>
        /// Get month from PESEL
        /// </summary>
        public int GetMonth()
        {
            int month = int.Parse(_id.Substring(2, 2));
            if (month > 80) return month - 80;
            if (month > 60) return month - 60;
            if (month > 40) return month - 40;
            if (month > 20) return month - 20;
            return month;
        }

        /// <summary>
        /// Get day from PESEL
        /// </summary>
        public int GetDay()
        {
            string day = _id.Substring(4, 2);
            return int.Parse(day);
        }

        /// <summary>
        /// Get year of birth from PESEL
        /// </summary>
        public int GetYearOfBirth()
        {
            return GetYear();
        }

        /// <summary>
        /// Get gender from PESEL
        /// </summary>
        public string GetGender()
        {
            int genderDigit = int.Parse(_id[9].ToString());
            return genderDigit % 2 == 0 ? "f" : "m"; // Even - female, odd - male
        }

        /// <summary>
        /// Check if PESEL is valid
        /// </summary>
        public bool IsValid()
        {
            // Validate PESEL checksum
            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            int sum = 0;

            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(_id[i].ToString()) * weights[i];
            }

            int checkDigit = (10 - (sum % 10)) % 10;
            return checkDigit == int.Parse(_id[10].ToString());
        }
    }
}