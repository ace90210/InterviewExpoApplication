namespace InterviewExpoApplication.Data.Repositories
{
    using InterviewExpoApplication.Data.Contexts;
    using InterviewExpoApplication.Data.Entities;
    using InterviewExpoApplication.Data.Mappers;
    using InterviewExpoApplication.Shared.Company;
    using InterviewExpoApplication.Shared.EventRegistration;
    using InterviewExpoApplication.Shared.User;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly MainContext _context;

        public RegistrationRepository(MainContext context)
        {
            _context = context;
        }

        public async Task<EventRegistrationsResult> GetEventRegistrationsSummaryAsync()
        {
            var companies = await _context.Companies.Include(c => c.UserCompanies).ThenInclude(uc => uc.User).ToListAsync();

            var companyDtos = companies.ToDeepDtos();

            return new EventRegistrationsResult()
            {
                Companies = companyDtos,
                TotalRegistrants = _context.Users.Count()
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDto"></param>
        /// <param name="companyDto"></param>
        /// <returns>true if new record added, false if already existed</returns>
        public async Task<bool> RegisterEvent(UserDto userDto, CompanyDto companyDto)
        {
            var user = await GetUserInternalsAsync(userDto);
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.CompanyName.Equals(companyDto.CompanyName));

            if(user != null && company != null) {
                // user already registered
                return false;
            }

            if(user == null)
            {
                user = userDto.ToEntity();
                _context.Users.Add(user);
            }

            if(company == null)
            {
                company = companyDto.ToEntity();

                _context.Companies.Add(company);
            }

            await _context.SaveChangesAsync();

            var userCompany = new UserCompany
            {
                UserId = user.Id,
                CompanyId = company.Id
            };

            _context.UserCompanies.Add(userCompany);

            await _context.SaveChangesAsync();
            return true;
        }

        public int GetTotalRegistrations()
        {         
            return _context.UserCompanies.Count();
        }

        private async Task<User> GetUserInternalsAsync(UserDto user)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.FirstName.Equals(user.FirstName) && u.LastName.Equals(user.LastName) && u.DateOfBirth.Date.Equals(user.DateOfBirth.Date));
        }

        public async Task ClearRegistrations()
        {
            _context.UserCompanies.RemoveRange(_context.UserCompanies.ToList());
            _context.Users.RemoveRange(_context.Users.ToList());
            _context.Companies.RemoveRange(_context.Companies.ToList());
            await _context.SaveChangesAsync();
        }
    }
}
