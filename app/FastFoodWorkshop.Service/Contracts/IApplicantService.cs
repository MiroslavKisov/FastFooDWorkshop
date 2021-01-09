namespace FastFoodWorkshop.Service.Contracts
{
    using ServiceModels.Applicant;
    using System.Threading.Tasks;

    public interface IApplicantService
    {
        Task<int> AddApplicantCvAsync(ApplicantCvViewModel model);

        Task<int> AddApplicantJobAsync(JobInputModel model, int currentApplicantId);

        Task<int> AddApplicantEducationAsync(EducationViewModel model, int currentApplicantId);
    }
}
