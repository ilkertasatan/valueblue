using System;

namespace ValueBlue.MovieSearch.IntegrationTests.MongoDbTests.Repositories
{
    public sealed class FakeEntity :
        IEquatable<FakeEntity>
    {
        public FakeEntity()
        {
        }

        public FakeEntity(Guid id, string field1, string field2)
        {
            Id = id;
            Field1 = field1;
            Field2 = field2;
        }

        public Guid Id { get; private set; }
        public string Field1 { get; private set; }
        public string Field2 { get; private set; }

        public bool Equals(FakeEntity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Field1 == other.Field1 && Field2 == other.Field2;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FakeEntity)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Field1, Field2);
        }
    }
}