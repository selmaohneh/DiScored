using System;

namespace DiScored
{
    public class Moderator
    {
        public string Name { get; }

        public Moderator(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("The name of a moderator is not allowed to be empty.");
            }

            Name = name;
        }


        public bool Equals(Moderator other)
        {
            return other?.Name == Name;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Moderator moderator))
            {
                return false;
            }

            return Equals(moderator);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}