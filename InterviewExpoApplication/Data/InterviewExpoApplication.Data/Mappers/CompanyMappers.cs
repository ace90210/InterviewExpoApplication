using InterviewExpoApplication.Data.Entities;
using InterviewExpoApplication.Shared.Company;
using InterviewExpoApplication.Shared.User;
using System.Collections.Generic;
using System.Linq;

namespace InterviewExpoApplication.Data.Mappers
{
    public static class CompanyMappers
    {
        public static Company ToEntity(this CompanyDto companyDto)
        {

            var companyEntity = new Company();
            companyEntity.Id = companyDto.Id;
            companyEntity.CompanyName = companyDto.CompanyName;
            companyEntity.CompanyDomain = companyDto.Domain;

            return companyEntity;
        }

        public static CompanyDto ToDto(this Company company)
        {
            var companyDto = new CompanyDto();
            companyDto.Id = company.Id;
            companyDto.CompanyName = company.CompanyName;
            companyDto.Domain = company.CompanyDomain;

            return companyDto;
        }


        public static CompanyDto ToDeepDto(this Company company)
        {
            if (company == null)
                return null;

            var companyDto = company.ToDto();

            var userDtos = company.UserCompanies.Select(uc => uc.User).ToList().ToDtos();

            companyDto.Users = userDtos;

            return companyDto;
        }

        public static List<CompanyDto> ToDeepDtos(this List<Company> companyDtos)
        {
            return companyDtos?.Select(c => c.ToDeepDto()).ToList();
        }



        public static User ToEntity(this UserDto userDto)
        {

            var userEntity = new User();
            userEntity.Id = userDto.Id;
            userEntity.FirstName = userDto.FirstName;
            userEntity.LastName = userDto.LastName;
            userEntity.DateOfBirth = userDto.DateOfBirth;

            return userEntity;
        }

        public static UserDto ToDto(this User user)
        {

            var userDto = new UserDto();
            userDto.Id = user.Id;
            userDto.FirstName = user.FirstName;
            userDto.LastName = user.LastName;
            userDto.DateOfBirth = user.DateOfBirth;

            return userDto;
        }
        public static List<UserDto> ToDtos(this List<User> userDtos)
        {
            return userDtos?.Select(u =>u.ToDto()).ToList();
        }
    }
}
