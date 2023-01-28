using System;
namespace Catalog.Domain.SeedWork
{
    /// <summary>
    /// Add entity behaviour to object
    /// </summary>
	public abstract class Entity
	{
        /// <summary>
        /// Unique Id of entity.
        /// </summary>
		private Guid _id;

        /// <summary>
        /// HashCode of entity.
        /// </summary>
		private int? _hashCode;

        /// <summary>
        /// Unique Id of entity.
        /// </summary>
		public Guid Id { get => _id; set => _id = value; }

        /// <summary>
        /// Checks if entity is fully qualified object.
        /// </summary>
        /// <returns>
        /// True - If Unique Id is assigned.
        /// </returns>
        public bool IsTransient { get => this._id == default; }

        /// <summary>
        /// Get hashcode of entity.
        /// </summary>
        /// <returns>
        /// int - HashCode based on Unique Id of entity.
        /// </returns>
        public override int GetHashCode()
        {
            if (IsTransient)
                return base.GetHashCode();

            if (!_hashCode.HasValue)
                _hashCode = this._id.GetHashCode() ^ 31;

            return _hashCode.Value;
        }

        /// <summary>
        /// Compare and checks if entity is same object as other entity.
        /// </summary>
        /// <param name="obj">Entity object to compare with.</param>
        /// <returns>True - If both entity objects are same.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Entity) || this.GetType() != obj.GetType())
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            var objEntity = (Entity)obj;

            if (this.IsTransient || objEntity.IsTransient)
                return false;

            return this.Id == objEntity.Id;
        }

        /// <summary>
        /// Compare and checks if entity is same object as other entity.
        /// </summary>
        /// <param name="left">Left operand entity object to compare.</param>
        /// <param name="right">Right operand entity object to compare.</param>
        /// <returns>True - If both entity objects are same.</returns>
        public static bool operator == (Entity left, Entity right)
        {
            if(Object.Equals(left, null))
                return Object.Equals(right, null);

            return left.Equals(right);
        }

        /// <summary>
        /// Compare and checks if entity is not same object as other entity.
        /// </summary>
        /// <param name="left">Left operand entity object to compare.</param>
        /// <param name="right">Right operand entity object to compare.</param>
        /// <returns>True - If both entity objects are not same.</returns>
        public static bool operator != (Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}