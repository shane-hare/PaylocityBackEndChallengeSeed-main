using Api.Dtos.Dependent;

namespace Api.Models.Converters
{
    public class DependentDtoConverter
    {
        public static GetDependentDto? Convert(Dependent dependent)
        {
            if (dependent == null)
            {
                return null;
            }

            var dependentDto = new GetDependentDto()
            {
                Id = dependent.Id,
                FirstName = dependent.FirstName,
                LastName = dependent.LastName,
                DateOfBirth = dependent.DateOfBirth,
                Relationship = dependent.Relationship
            };

            return dependentDto;
        }
    }
}
