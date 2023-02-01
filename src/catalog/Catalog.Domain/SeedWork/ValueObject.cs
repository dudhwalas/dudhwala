using System;
using System.Collections.Generic;

namespace Catalog.Domain.SeedWork
{
    //Inspired from https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/implement-value-objects

    /// <summary>
    /// Add value object behaviour to object
    /// </summary>
    public abstract class ValueObject
    {
        /// <summary>
        /// Compare and checks if value object is same object as other value object.
        /// </summary>
        /// <param name="left">Left operand value object to compare.</param>
        /// <param name="right">Right operand value object to compare.</param>
        /// <returns>True - If both value objects are same.</returns>
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, null) || left.Equals(right);
        }

        /// <summary>
        /// Compare and checks if value object is not same object as other value object.
        /// </summary>
        /// <param name="left">Left operand value object to compare.</param>
        /// <param name="right">Right operand value object to compare.</param>
        /// <returns>True - If both value objects are not same.</returns>
        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !(EqualOperator(left, right));
        }

        /// <summary>
        /// Override to get properties to be considered for equality comparision.
        /// </summary>
        /// <returns>Properties to be considered for equality comparision.</returns>
        protected abstract IEnumerable<object> GetEqualityComponents();

        /// <summary>
        /// Compare and checks if value object is same object as other value object.
        /// </summary>
        /// <param name="obj">Value object to compare with.</param>
        /// <returns>True - If both value objects are same.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ValueObject)obj;

            return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// Get hashcode of value object.
        /// </summary>
        /// <returns>
        /// int - HashCode based on equality components of value object.
        /// </returns>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        /// <summary>
        /// Create copy of value object.
        /// </summary>
        /// <returns>Cloned value object.</returns>
        public ValueObject? GetCopy()
        {
            return this.MemberwiseClone() as ValueObject;
        }

        /// <summary>
        /// Compare and checks if value object is same object as other value object.
        /// </summary>
        /// <param name="left">Left operand value object to compare.</param>
        /// <param name="right">Right operand value object to compare.</param>
        /// <returns>True - If both value objects are same.</returns>
        public static bool operator == (ValueObject left, ValueObject right)
        {
            return EqualOperator(left, right);
        }

        /// <summary>
        /// Compare and checks if value object is not same object as other value object.
        /// </summary>
        /// <param name="left">Left operand value object to compare.</param>
        /// <param name="right">Right operand value object to compare.</param>
        /// <returns>True - If both value objects are not same.</returns>
        public static bool operator != (ValueObject left, ValueObject right)
        {
            return NotEqualOperator(left, right);
        }
    }
}

