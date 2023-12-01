namespace Api.Models.Deduction
{
    public class DependentDeduction : BaseDeduction, IDependentDeduction
    {
        public DependentDeduction()
        {
            DeductionType = DeductionRelationship.Dependent;
        }
        public decimal Deduct(Dependent dependent)
        {
            if (dependent == null)
            {
                throw new ArgumentNullException("Dependant is null");
            }

            return 600;
        }
    }
}
