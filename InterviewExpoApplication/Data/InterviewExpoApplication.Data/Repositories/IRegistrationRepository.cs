using InterviewExpoApplication.Data.Entities;
using InterviewExpoApplication.Shared.Company;
using InterviewExpoApplication.Shared.EventRegistration;
using InterviewExpoApplication.Shared.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewExpoApplication.Data.Repositories
{
    public interface IRegistrationRepository
    {
        Task<bool> RegisterEvent(UserDto user, CompanyDto company);

        int GetTotalRegistrations();

        Task<EventRegistrationsResult> GetEventRegistrationsSummaryAsync();

        Task ClearRegistrations();
    }

}
