namespace RailwayLibrary.Unit
{
    /// <summary>
    /// Unit type for use when returning nothing or void
    /// </summary>
    public sealed class Unit
    {
        /// <summary>
        /// The default <see cref="Unit"/> instance.
        /// </summary>
        public static readonly Unit Instance = new Unit();

        /// <summary>
        /// Gets "Unit" as a string value.
        /// </summary>
        public override string ToString() => nameof(Unit);
    }
}
