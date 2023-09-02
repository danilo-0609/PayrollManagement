namespace PayrollManagement.Domain.ValueObjects
{
    public partial record Address
    {
        public string Country { get; init; }

        public string City { get; init; } 

        public string Neighborhood { get; init; }

        public string HouseAddress { get; init; }

        private Address(string country, string city, string neighborhood, string houseAddress)
        {
            Country = country;
            City = city;
            Neighborhood = neighborhood;
            HouseAddress = houseAddress;
        }

        public static Address? Create(string country, string city, string neighborhood, string houseAddress)
        {
            if (string.IsNullOrEmpty(country) || string.IsNullOrEmpty(city) ||
                string.IsNullOrEmpty(neighborhood) || string.IsNullOrEmpty(houseAddress))
            {
                return null;
            }


            return new Address(country, city, neighborhood, houseAddress);
        }

    }
}
