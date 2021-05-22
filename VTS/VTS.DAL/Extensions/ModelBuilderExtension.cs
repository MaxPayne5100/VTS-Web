using System;
using Microsoft.EntityFrameworkCore;
using VTS.Core.Constants;
using VTS.DAL.Entities;

namespace VTS.DAL.Extensions
{
    /// <summary>
    /// ModelBuilder extension.
    /// </summary>
    public static class ModelBuilderExtension
    {
        /// <summary>
        /// Method for adding data to the database.
        /// </summary>
        /// <param name="modelBuilder">Param with ModelBuilder type.</param>
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Mykhailo",
                    LastName = "Bordun",
                    Email = "bordun@gmail.com",
                    Role = Roles.Clerk,
                },
                new User
                {
                    Id = 2,
                    FirstName = "Joe",
                    LastName = "Doe",
                    Email = "bordun@gmail.com",
                    Role = Roles.Employee,
                },
                new User
                {
                    Id = 3,
                    FirstName = "Max",
                    LastName = "Payne",
                    Email = "payne@gmail.com",
                    Role = Roles.Manager,
                });

            modelBuilder.Entity<Head>().HasData(
                new Head
                {
                    Id = 1,
                    UserId = 1,
                },
                new Head
                {
                    Id = 2,
                    UserId = 3,
                });

            modelBuilder.Entity<Clerk>().HasData(
                new Clerk
                {
                    Id = 1,
                    HeadId = 1,
                });

            modelBuilder.Entity<Manager>().HasData(
                new Manager
                {
                    Id = 1,
                    HeadId = 2,
                });

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    UserId = 2,
                    ManagerId = 1,
                });

            modelBuilder.Entity<UserVacationInfo>().HasData(
                new UserVacationInfo
                {
                    Id = Guid.NewGuid(),
                    UserId = 1,
                    PaidDayOffs = 15,
                    UnPaidDayOffs = 15,
                    PaidSickness = 15,
                    UnPaidSickness = 15,
                    BonusPaidDayOffs = 0,
                    StartedWorking = new DateTime(2020, 05, 15),
                },
                new UserVacationInfo
                {
                    Id = Guid.NewGuid(),
                    UserId = 2,
                    PaidDayOffs = 15,
                    UnPaidDayOffs = 15,
                    PaidSickness = 15,
                    UnPaidSickness = 15,
                    BonusPaidDayOffs = 0,
                    StartedWorking = new DateTime(2020, 11, 15),
                },
                new UserVacationInfo
                {
                    Id = Guid.NewGuid(),
                    UserId = 3,
                    PaidDayOffs = 15,
                    UnPaidDayOffs = 15,
                    PaidSickness = 15,
                    UnPaidSickness = 15,
                    BonusPaidDayOffs = 0,
                    StartedWorking = new DateTime(2021, 02, 10),
                });

            modelBuilder.Entity<Holiday>().HasData(
                new Holiday
                {
                    Id = Guid.Parse("2ac054d6-8508-4daf-e348-08d91b0fb7e6"),
                    UserId = 1,
                    Category = Holiday.Categories.PaidDayOffs,
                    Hours = 24,
                    Start = new DateTime(2021, 05, 21),
                    Expires = new DateTime(2021, 05, 22),
                    Description = "Mind refresh",
                    SubmissionTime = DateTime.Now,
                },
                new Holiday
                {
                    Id = Guid.Parse("c6e7762b-ec65-42c3-e349-08d91b0fb7e6"),
                    UserId = 1,
                    Category = Holiday.Categories.UnPaidSickness,
                    Hours = 72,
                    Start = new DateTime(2021, 05, 28),
                    Expires = new DateTime(2021, 05, 31),
                    Description = "Flu",
                    SubmissionTime = DateTime.Now,
                },
                new Holiday
                {
                    Id = Guid.Parse("389da9dc-7bdd-43ad-80ab-08d91c73789f"),
                    UserId = 1,
                    Category = Holiday.Categories.PaidSickness,
                    Hours = 48,
                    Start = new DateTime(2021, 05, 23),
                    Expires = new DateTime(2021, 05, 25),
                    Description = "Flu",
                    SubmissionTime = DateTime.Now,
                },
                new Holiday
                {
                    Id = Guid.Parse("1aafbbf1-0261-4522-8267-d6ae96e9fbcb"),
                    UserId = 2,
                    Category = Holiday.Categories.UnPaidDayOffs,
                    Hours = 24,
                    Start = new DateTime(2021, 05, 24),
                    Expires = new DateTime(2021, 05, 25),
                    Description = "Mind refresh",
                    SubmissionTime = DateTime.Now,
                },
                new Holiday
                {
                    Id = Guid.Parse("7e99ea45-e7ef-464a-a779-75c636d90bfb"),
                    UserId = 3,
                    Category = Holiday.Categories.PaidDayOffs,
                    Hours = 72,
                    Start = new DateTime(2021, 05, 27),
                    Expires = new DateTime(2021, 05, 30),
                    Description = "Mind refresh",
                    SubmissionTime = DateTime.Now,
                });

            modelBuilder.Entity<HolidayAcception>().HasData(
                new HolidayAcception
                {
                    Id = Guid.NewGuid(),
                    HolidayId = Guid.Parse("2ac054d6-8508-4daf-e348-08d91b0fb7e6"),
                    HeadId = 1,
                    Status = HolidayAcception.StatusType.Canceled,
                    Description = "Have a good time :)",
                },
                new HolidayAcception
                {
                    Id = Guid.NewGuid(),
                    HolidayId = Guid.Parse("c6e7762b-ec65-42c3-e349-08d91b0fb7e6"),
                    HeadId = 1,
                    Status = HolidayAcception.StatusType.Approved,
                    Description = "Wish you good health :)",
                },
                new HolidayAcception
                {
                    Id = Guid.NewGuid(),
                    HolidayId = Guid.Parse("389da9dc-7bdd-43ad-80ab-08d91c73789f"),
                    HeadId = 1,
                    Status = HolidayAcception.StatusType.Approved,
                    Description = "Wish you good health :)",
                },
                new HolidayAcception
                {
                    Id = Guid.NewGuid(),
                    HolidayId = Guid.Parse("1aafbbf1-0261-4522-8267-d6ae96e9fbcb"),
                    HeadId = 1,
                    Status = HolidayAcception.StatusType.Approved,
                    Description = "Have a good time :)",
                },
                new HolidayAcception
                {
                    Id = Guid.NewGuid(),
                    HolidayId = Guid.Parse("7e99ea45-e7ef-464a-a779-75c636d90bfb"),
                    HeadId = 1,
                    Status = HolidayAcception.StatusType.Approved,
                    Description = "Have a good time :)",
                });
        }
    }
}
