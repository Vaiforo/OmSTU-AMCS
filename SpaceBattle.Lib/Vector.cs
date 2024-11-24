namespace SpaceBattle.Lib
{
    public class Vector
    {
        private int[] coords;


        public Vector(params int[] coords)
        {
            this.coords = coords;
        }

        public static Vector operator +(Vector vector1, Vector vector2)
        {
            if (vector1.coords.Length != vector2.coords.Length)
            {
                throw new InvalidDataException("Vectors must have the same length");
            }

            Vector result = new Vector(new int[vector1.coords.Length]);
            result.coords = vector1.coords.Select((value, index) => value + vector2.coords[index]).ToArray();
            return result;
        }
        public override bool Equals(object? obj)
        {
            return obj != null && obj is Vector vector && coords.SequenceEqual(vector.coords);
        }

        public override int GetHashCode()
        {
            return coords.GetHashCode();
        }

        public int[] GetCoords()
        {
            return coords;
        }
    }
}
