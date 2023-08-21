namespace NLayer.Repository.Repositories
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public List<Address> Addresses { get; set; }
        public ICollection<Address> Address { get; set; }

    }
    //1 kişinin 2 adresi olabilir
    public class Address
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int PersonId { get; set; }//FK
        public Person Person { get; set; }//bir adres bir kişiye aittir

    }

    //******

    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public EmployeeDetails EmployeeDetails { get; set; }
    }
    public class EmployeeDetails
    {
        public int Id { get; set; }
        public string PassportNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Employee Employee { get; set; }
    }

}
