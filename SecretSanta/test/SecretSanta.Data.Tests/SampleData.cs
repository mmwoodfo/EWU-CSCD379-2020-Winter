namespace SecretSanta.Data.Tests
{
    public static class SampleData
    {

        //Gift Sample Data
        public const string Ring = "Ring Doorbell";
        public const string RingUrl = "www.ring.com";
        public const string RingDescription = "The doorbell that saw too much";

        public const string Arduino = "Arduino";
        public const string ArduinoUrl = "www.arduino.com";
        public const string ArduinoDescription = "Every good geek needs an IOT device";

        public static Gift CreateRingGift() => new Gift(Ring, RingUrl, RingDescription, CreateJonDoe());
        public static Gift CreateArduinoGift() => new Gift(Arduino, ArduinoUrl, ArduinoDescription, CreateBrandonFields());

        //User Sample Data
        public const string River = "River";
        public const string Willis = "Willis";
        public const string RiverWillisAlias = "rwillis";

        public const string Brandon = "Brandon";
        public const string Fields = "Fields";
        public const string BrandonFieldsAlias = "bfields";

        public const string Jon = "Jon";
        public const string Doe = "Doe";
        public const string JonDoeAlias = "jdoe";

        public static User CreateRiverWillis() => new User(River, Willis);
        public static User CreateBrandonFields() => new User(Brandon, Fields);
        public static User CreateJonDoe() => new User(Jon, Doe);

        //Group Sample Data
        public const string EnchantedForest = "Enchanted Forest";

        public static Group CreateEnchantedForestGroup() => new Group(EnchantedForest);
    }
}