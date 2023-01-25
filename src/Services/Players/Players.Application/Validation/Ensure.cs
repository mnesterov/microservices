using Players.Application.Exceptions;

namespace Players.Application.Validation
{
    public static class Ensure
    {
        public static void IsFound<T>(T o) where T : class
        {
            if (o is null)
                throw new NotFoundException();
        }
    }
}