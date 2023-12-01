using Api.Models;
using System.Linq;

namespace Api.Models.Database
{
    public class InMemoryRepository : IDataRepository
    {
        //TODO: BEtter way to store/Aquire inside children

        private List<Employee> employees = new List<Employee>
        {
            new()
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                DateOfBirth = new DateTime(1984, 12, 30)
            },
            new()
            {
                Id = 2,
                FirstName = "Ja",
                LastName = "Morant",
                Salary = 92365.22m,
                DateOfBirth = new DateTime(1999, 8, 10),
                Dependents = new List<Dependent>
                {
                    new()
                    {
                        Id = 1,
                        FirstName = "Spouse",
                        LastName = "Morant",
                        Relationship = Relationship.Spouse,
                        DateOfBirth = new DateTime(1998, 3, 3)
                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "Child1",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2020, 6, 23)
                    },
                    new()
                    {
                        Id = 3,
                        FirstName = "Child2",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2021, 5, 18)
                    }
                }
            },
            new()
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                Salary = 143211.12m,
                DateOfBirth = new DateTime(1963, 2, 17),
                Dependents = new List<Dependent>
                {
                    new()
                    {
                        Id = 4,
                        FirstName = "DP",
                        LastName = "Jordan",
                        Relationship = Relationship.DomesticPartner,
                        DateOfBirth = new DateTime(1974, 1, 2)
                    }
                }
            }
        };

        public InMemoryRepository()
        {
        }

        public InMemoryRepository(List<Employee> employees)
        {
            this.employees = employees;
        }

        
        public Employee? GetEmployee(int id)
        {
            return employees.FirstOrDefault(emp => emp.Id == id);
        }

        public List<Employee> GetEmployees()
        {
            return employees;
        }

        public Dependent? GetDependent(int id)
        {
            foreach(Employee emp in employees)
            {
                foreach (Dependent dep in emp?.Dependents)
                {
                    if (dep.Id == id)
                    {
                        return dep;
                    }
                }
            }

            return null;
        }

        public List<Dependent?> GetDependents()
        {
            List<Dependent?> result = new List<Dependent?>();

            foreach(Employee emp in employees)
            {
                result.AddRange(emp.Dependents);
            }

            return result;
        }

        public Paycheck? GetPaycheck(System.Guid id)
        {
            //THis can be simplified with a different data structure
            foreach(Employee emp in employees)
            {
                foreach(Paycheck pay in emp.Paychecks)
                {
                    if (pay.Id == id)
                    {
                        return pay;
                    }
                }
            }

            return null;
        }

        public void AddPaycheck(Employee emp, Paycheck pay)
        {
            emp?.Paychecks.Add(pay);            
        }
    }
}
