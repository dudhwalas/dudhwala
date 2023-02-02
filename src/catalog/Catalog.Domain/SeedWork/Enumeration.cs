using System;
using System.Collections.Generic;
using System.Reflection;

namespace Catalog.Domain.SeedWork
{
    /// <summary>
    /// Add Enum behaviour to object.
    /// </summary>
    public abstract class Enumeration : IComparable
    {
        /// <summary>
        /// Name of enum type.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Id of enum type.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Constructor to create enumeration type.
        /// </summary>
        /// <param name="id">Id of enum type.</param>
        /// <param name="name">Name of enum type.</param>
        protected Enumeration(int id, string name) => (Id, Name) = (id, name);

        /// <summary>
        /// String representation of enum type.
        /// </summary>
        /// <returns>Name of enum type.</returns>
        public override string ToString() => Name;

        /// <summary>
        /// Get all enum types defined in enumeration.
        /// </summary>
        /// <typeparam name="T">Enum type.</typeparam>
        /// <returns>All enum types defined in enumeration.</returns>
        public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                        .Select(f => f.GetValue(null))
                        .Cast<T>();

        /// <summary>
        /// Compare and checks if enum is same object as other enum.
        /// </summary>
        /// <param name="obj">enum object to compare with.</param>
        /// <returns>True - If both enums are same.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not Enumeration otherValue)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        /// <summary>
        /// Get hashcode of enum.
        /// </summary>
        /// <returns>
        /// int - HashCode based on Unique Id of enum.
        /// </returns>
        public override int GetHashCode() => Id.GetHashCode();

        /// <summary>
        /// Calculate difference between two enum Ids.
        /// </summary>
        /// <param name="firstValue">First enum</param>
        /// <param name="secondValue">Second enum</param>
        /// <returns>Difference between two enum Ids.</returns>
        public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.Id - secondValue.Id);
            return absoluteDifference;
        }

        /// <summary>
        /// Create enum from unique enum Id.
        /// </summary>
        /// <typeparam name="T">Type of enum.</typeparam>
        /// <param name="value">Unique enum Id.</param>
        /// <returns>Enum type.</returns>
        public static T FromValue<T>(int value) where T : Enumeration
        {
            var matchingItem = Parse<T, int>(value, "value", item => item.Id == value);
            return matchingItem;
        }

        /// <summary>
        /// Create enum from unique enum name.
        /// </summary>
        /// <typeparam name="T">Type of enum.</typeparam>
        /// <param name="displayName">Unique enum name.</param>
        /// <returns>Enum type.</returns>
        public static T FromDisplayName<T>(string displayName) where T : Enumeration
        {
            var matchingItem = Parse<T, string>(displayName, "display name", item => item.Name == displayName);
            return matchingItem;
        }

        /// <summary>
        /// Parse one enum type to another enum type based on predicate.
        /// </summary>
        /// <typeparam name="T">enum type to parse into - target</typeparam>
        /// <typeparam name="K">enum type to parse from - source</typeparam>
        /// <param name="value">Value of enum type to parse from.</param>
        /// <param name="description">Description of parsing operation.</param>
        /// <param name="predicate">Perdicate condition to perform parsing.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

            return matchingItem;
        }

        /// <summary>
        /// Compare and checks if enum is same object as other enum.
        /// </summary>
        /// <param name="other">enum to compare with.</param>
        /// <returns>0 - If both enums are same.</returns>
        public int CompareTo(object? other) => other == null ? 1 : Id.CompareTo(((Enumeration)other).Id);
    }
}