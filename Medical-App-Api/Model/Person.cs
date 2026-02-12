namespace Medical_App_Api.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        // i wonded if i should add an email and phone number properties so that both the
        //  patien and the doctor could either communicate orthe handler could remind either of the deadline
    }
}
