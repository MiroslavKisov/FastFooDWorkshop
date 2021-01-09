namespace FastFoodWorkshop.Service
{
    using AutoMapper;
    using Common.WebConstants;
    using Data;
    using Extensions;
    using Models;
    using Service.Contracts;
    using ServiceModels.Applicant;
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public class ApplicantService : IApplicantService
    {
        private readonly IRepository<Education> educationRepository;
        private readonly IRepository<Job> jobRepository;
        private readonly IRepository<ApplicantCV> applicantRepository;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public ApplicantService
            (IRepository<ApplicantCV> applicantRepository,
             IMapper mapper,
             IRepository<Job> jobRepository,
             IRepository<Education> educationRepository,
             ILogger<ApplicantService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.applicantRepository = applicantRepository;
            this.jobRepository = jobRepository;
            this.educationRepository = educationRepository;
        }

        public async Task<int> AddApplicantCvAsync(ApplicantCvViewModel model)
        {
            ApplicantCV applicantCv = null;

            try
            {
                applicantCv = this.mapper.Map<ApplicantCV>(model);

                applicantCv.Picture = await model.Picture.UploadAsync();

                await this.applicantRepository.AddAsync(applicantCv);

                await this.applicantRepository.SaveChangesAsync();

                this.logger.LogInformation(LogMessages.UserAppliedForJob);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }

            return applicantCv.Id;
        }

        public async Task<int> AddApplicantJobAsync(JobInputModel model, int currentApplicantId)
        {
            Job job = null;

            try
            {
                job = this.mapper.Map<Job>(model);

                job.ApplicantCVId = currentApplicantId;

                await this.jobRepository.AddAsync(job);

                await this.jobRepository.SaveChangesAsync();

                this.logger.LogInformation(LogMessages.UserAddedJob);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }

            return job.Id;
        }

        public async Task<int> AddApplicantEducationAsync(EducationViewModel model, int currentApplicantId)
        {
            Education education = null;

            try
            {
                education = this.mapper.Map<Education>(model);

                education.ApplicantCVId = currentApplicantId;

                await this.educationRepository.AddAsync(education);

                await this.educationRepository.SaveChangesAsync();

                this.logger.LogInformation(LogMessages.UserAddedEducation);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }

            return education.Id;
        }
    }
}
