public partial class Asteroid
{
    public readonly struct CollisionEvt
    {
        public Asteroid Asteroid { get; }
        public Asteroid Other { get; }

        public CollisionEvt(Asteroid asteroid, Asteroid other)
        {
            Asteroid = asteroid;
            Other = other;
        }

        public void Deconstruct(out Asteroid asteroid, out Asteroid otherAsteroid)
        {
            asteroid = Asteroid;
            otherAsteroid = Other;
        }
    }
}
