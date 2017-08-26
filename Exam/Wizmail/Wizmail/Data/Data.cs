namespace Wizmail.Data
{
    public class Data
    {
        private static WizmailContext context;

        public static WizmailContext Context =>
            context ?? (context = new WizmailContext());
    }
}
