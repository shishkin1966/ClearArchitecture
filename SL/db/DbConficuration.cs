namespace ClearArchitecture.SL
{
    public class DbConficuration : INamed
    {
        private readonly string _name;

        public DbConficuration(string name)
        {
            _name = name;

            ConnectionString = "Data Source = OSHISHKINPC; Initial Catalog = StackOverflow2010; Integrated Security = True; MultipleActiveResultSets = True";
        }

        public string ConnectionString { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Db { get; set; }



        public string GetName()
        {
            return _name;
        }
    }
}
