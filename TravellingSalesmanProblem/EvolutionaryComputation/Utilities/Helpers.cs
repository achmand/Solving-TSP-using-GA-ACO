namespace EvolutionaryComputation.Utilities
{
    public static class Helpers
    {
        public static double MapValue(double a0, double a1, double b0, double b1, double a)
        {
            return b0 + (b1 - b0) * ((a - a0) / (a1 - a0));
        }
    }
}
