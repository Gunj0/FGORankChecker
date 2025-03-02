using FGORankChecker.Domain.ValueObjects;

namespace FGORankChecker.Domain.Entities
{
    public sealed class ServantEntity
    {
        public ServantEntity(int id, string name, string rank, string rare, string classType, string npType, string range, string thumbnail)
        {
            Id = id;
            Name = name;
            Rank = new Rank(rank);
            Rare = rare;
            ClassType = classType;
            NPType = npType;
            Range = range;
            Thumbnail = thumbnail;
        }

        public int Id { get; }
        public string Name { get; }
        public Rank Rank { get; }
        public string Rare { get; }
        public string ClassType { get; }
        public string NPType { get; }
        public string Range { get; }
        public string Thumbnail { get; }
    }
}