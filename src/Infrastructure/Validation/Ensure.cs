using Infrastructure.Exceptions;

namespace Infrastructure.Validation 
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