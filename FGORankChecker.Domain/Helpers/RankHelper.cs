namespace FGORankChecker.Domain.Helpers
{
    public static class RankHelper
    {
        public static int StringToInt(this string rankString)
        {
            return rankString switch
            {
                "SSS" => 10,
                "SS" => 9,
                "S+" => 8,
                "S" => 7,
                "A+" => 6,
                "A" => 5,
                "B" => 4,
                "C" => 3,
                "D" => 2,
                _ => 0,
            };
        }
    }
}