namespace FGORankChecker.Domain.ValueObjects
{
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        // 別インスタンスでも値が同じであれば同じとみなす①
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is not T vo)
            {
                return false;
            }
            return EqualsCore(vo);
        }

        // 別インスタンスでも値が同じであれば同じとみなす②
        public static bool operator ==(ValueObject<T> vo1, ValueObject<T> vo2)
        {
            return vo1.Equals(vo2);
        }

        // ==は同時に!=も実装する必要がある
        public static bool operator !=(ValueObject<T> vo1, ValueObject<T> vo2)
        {
            return !vo1.Equals(vo2);
        }

        // 継承先でイコール判定を実装する
        protected abstract bool EqualsCore(T other);

        // これは警告が出るので書いただけ
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}