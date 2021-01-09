namespace FastFoodWorkshop.Controllers
{
    using Common.WebConstants;
    using Service.Contracts;
    using ServiceModels.Applicant;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System;
    using Microsoft.AspNetCore.Http;

    public class ApplicantController : BaseController
    {
        private readonly IApplicantService applicantService;

        public ApplicantController(
            IApplicantService applicantService)
        {
            this.applicantService = applicantService;
        }

        [AllowAnonymous]
        public IActionResult JoinUs()
        {
            TempData[TempDataKeys.TempDataJoinUsWorkFlowKey] = true;
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> JoinUs(ApplicantCvViewModel model)
        {
            if (ModelState.IsValid)
            {
                var id = await this.applicantService.AddApplicantCvAsync(model);

                TempData[TempDataKeys.TempDataApplicantIdKey] = id;

                TempData[TempDataKeys.TempDataSuccessJoinUsKey] = SuccessMessages.JoinUsSuccessMessage;

                TempData.Remove(TempDataKeys.TempDataJoinUsWorkFlowKey);
            }

            return RedirectToAction(StringConstants.AddJobAndEducation);
        }

        [AllowAnonymous]
        public IActionResult AddJobAndEducation()
        {
            if (TempData.ContainsKey(TempDataKeys.TempDataJoinUsWorkFlowKey))
            {
                TempData[TempDataKeys.TempDataErrorJoinUsFormKey] = ErrorMessages.JoinUsFormIsNotFilled;
           
                return RedirectToAction(StringConstants.JoinUs);
            }

            TempData[TempDataKeys.TempDataJoinUsWorkFlowKey] = true;
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddJob(JobInputModel model)
        {
            if (ModelState.IsValid)
            {
                var currentApplicantId = Convert.ToInt32(TempData[StringConstants.TempDataApplicantIdKey]);

                await this.applicantService.AddApplicantJobAsync(model, currentApplicantId);

                TempData[TempDataKeys.TempDataSuccessJobKey] = SuccessMessages.JobSuccessMessage;

                TempData.Remove(TempDataKeys.TempDataJoinUsWorkFlowKey);
            }

            return RedirectToAction(StringConstants.AddJobAndEducation, StringConstants.Applicant);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEducation(EducationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentApplicantId = Convert.ToInt32(TempData[StringConstants.TempDataApplicantIdKey]);

                await this.applicantService.AddApplicantEducationAsync(model, currentApplicantId);

                TempData[TempDataKeys.TempDataSuccessEducationKey] = SuccessMessages.EducationSuccessMessage;

                TempData.Remove(TempDataKeys.TempDataJoinUsWorkFlowKey);
            }

            return RedirectToAction(StringConstants.AddJobAndEducation, StringConstants.Applicant);
        }
    }
}
