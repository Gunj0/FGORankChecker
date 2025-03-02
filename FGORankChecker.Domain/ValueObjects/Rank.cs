using FGORankChecker.Domain.Helpers;

namespace FGORankChecker.Domain.ValueObjects
{
    public sealed class Rank : ValueObject<Rank>
    {
        public string Value { get; }

        public Rank(string value)
        {
            Value = value;
        }

        public int ValueInt
        {
            get
            {
                return Value.StringToInt();
            }
        }

        protected override bool EqualsCore(Rank other)
        {
            return Value == other.Value;
        }
    }
}