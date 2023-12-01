namespace Api.Models.Deduction
{
    public class AgeDeduction : BaseDeduction, IDependentDeduction
    {
        public AgeDeduction() 
        {
            DeductionType = DeductionRelationship.AgeDependent;
        }

        public decimal Deduct(Dependent dependent)
        {
            if (dependent == null)
            {
                throw new ArgumentNullException("Dependant is null");
            }

            if (dependent.DateOfBirth < DateTime.Today.AddYears(-50))
            {
                return 200;
            }

            return 0;
        }
    }
}
