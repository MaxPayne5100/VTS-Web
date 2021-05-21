using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using VTS.Core.Constants;
using VTS.Repos.UnitOfWork;

namespace VTS.Services.BookingService
{
    /// <inheritdoc/>
    public class BookingService : IBookingService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        private string MapToEnumCategory(string dtoCategory)
        {
            if (dtoCategory == "Оплачувана відпустка")
            {
                return "PaidDayOffs";
            }
            else if (dtoCategory == "Неоплачувана відпустка")
            {
                return "UnPaidDayOffs";
            }
            else if (dtoCategory == "Оплачуваний лікарняний")
            {
                return "PaidSickness";
            }
            else if (dtoCategory == "Неоплачуваний лікарняний")
            {
                return "UnPaidSickness";
            }
            else
            {
                return dtoCategory;
            }
        }

        private string MapToEnumStatus(string dtoStatus)
        {
            if (dtoStatus == "Підтверджено" || dtoStatus == "Прийняти")
            {
                return "Approved";
            }
            else if (dtoStatus == "Скасовано" || dtoStatus == "Скасувати")
            {
                return "Cancelled";
            }
            else
            {
                return dtoStatus;
            }
        }

        private bool CheckWorkersNumberInDateRange(
            IEnumerable<DAL.Entities.Holiday> holidayList,
            DateTime start,
            DateTime end)
        {
            var dayList = new List<DateTime>();
            var minHeadNum = CompanyPolicy.MinNumHeadsInCompany;

            dayList = Enumerable.Range(0, 1 + end.Subtract(start).Days)
                      .Select(offset => start.AddDays(offset))
                      .ToList();

            foreach (var day in dayList)
            {
                int workerCounter = 0;
                int headCounter = 0;

                foreach (var holiday in holidayList)
                {
                    if (holiday.Start <= day && holiday.Expires >= day)
                    {
                        workerCounter++;

                        if (headCounter < minHeadNum)
                        {
                            if (_unitOfWork.Heads.FindHeadByUserId(holiday.UserId) != null)
                            {
                                headCounter++;
                            }
                        }
                    }
                }

                if (workerCounter < CompanyPolicy.MinNumWorkersInCompany ||
                    headCounter < minHeadNum)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingService"/> class.
        /// </summary>
        /// <param name="mapper">Mapper.</param>
        /// <param name="unitOfWork">Unit of work.</param>
        public BookingService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        /// <inheritdoc/>
        public async Task Add(Core.DTO.Holiday bookingDto)
        {
            var hours = (bookingDto.Expires - bookingDto.Start).TotalHours;

            if (hours > 0)
            {
                bookingDto.Hours = Convert.ToUInt32(hours);
                bookingDto.SubmissionTime = DateTime.Now;

                bookingDto.Category = MapToEnumCategory(bookingDto.Category);

                var ownHolidayList = await _unitOfWork.Holidays.FindPersonalBookingsInDateRange(
                    bookingDto.UserId,
                    bookingDto.Start,
                    bookingDto.Expires);

                if (ownHolidayList.Any())
                {
                    throw new ArgumentException($"У вас вже є відпустка у вказаних термінах");
                }

                var holidayList = await _unitOfWork.Holidays.FindAllBookingsInDateRange(
                    bookingDto.Start,
                    bookingDto.Expires);

                if (!CheckWorkersNumberInDateRange(holidayList, bookingDto.Start, bookingDto.Expires))
                {
                    throw new ArgumentException($"Замало працівників в ці терміни відпустки");
                }

                var paidDayOffs = new PaidDayOffs();
                var unpaidDayOffs = new UnPaidDayOffs();
                var paidSickness = new PaidSickness();
                var unpaidSickness = new UnPaidSickness();

                paidDayOffs.SetNext(unpaidDayOffs).SetNext(paidSickness).SetNext(unpaidSickness);
                paidDayOffs.Handle(bookingDto);

                var booking = _mapper.Map<DAL.Entities.Holiday>(bookingDto);
                await _unitOfWork.Holidays.AddAsync(booking);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                throw new ArgumentException($"Терміни відпустки неправильно вказані");
            }
        }

        /// <inheritdoc/>
        public async Task Approve(Core.DTO.HolidayAcception holidayaccDto)
        {
            holidayaccDto.Status = MapToEnumStatus(holidayaccDto.Status);

            var bookingAcc = _mapper.Map<DAL.Entities.HolidayAcception>(holidayaccDto);
            await _unitOfWork.HolidaysAcception.AddAsync(bookingAcc);
            await _unitOfWork.CommitAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Core.DTO.Holiday>> FindPersonalBookingsByDate(int userId, DateTime? startDate)
        {
            var bookings = await _unitOfWork.Holidays.FindPersonalBookingsByDate(userId, startDate);
            var bookingDtos = _mapper.Map<IEnumerable<Core.DTO.Holiday>>(bookings);
            return bookingDtos;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Core.DTO.Holiday>> FindPersonalBookingsByDateAndCategoryAndStatus(
            int userId,
            DateTime? startDate,
            string category,
            string status)
        {
            category = MapToEnumCategory(category);
            status = MapToEnumStatus(status);

            var bookings = await _unitOfWork.Holidays.FindPersonalBookingsByDateAndCategoryAndStatus(
                userId,
                startDate,
                category,
                status);

            var bookingDtos = _mapper.Map<IEnumerable<Core.DTO.Holiday>>(bookings);

            return bookingDtos;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Core.DTO.Holiday>> FindAllBookingsByDate(DateTime? startDate)
        {
            var bookings = await _unitOfWork.Holidays.FindAllBookingsByDate(startDate);
            var bookingDtos = _mapper.Map<IEnumerable<Core.DTO.Holiday>>(bookings);
            return bookingDtos;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Core.DTO.Holiday>> FindAllBookingsWithIncludedInfo(
            DateTime? startDate,
            string category)
        {
            category = MapToEnumCategory(category);

            var bookings = await _unitOfWork.Holidays.FindAllBookingsWithIncludedInfo(startDate, category);
            var bookingDtos = _mapper.Map<IEnumerable<Core.DTO.Holiday>>(bookings);
            return bookingDtos;
        }

        /// <inheritdoc/>
        public async Task<Core.DTO.Holiday> FindPersonalBooking(Guid id)
        {
            var booking = await _unitOfWork.Holidays.FindPersonalBooking(id);
            var bookingDto = _mapper.Map<Core.DTO.Holiday>(booking);
            return bookingDto;
        }

        /// <inheritdoc/>
        public async Task Remove(Guid id)
        {
            var booking = await _unitOfWork.Holidays.FindAsync(id);

            if (booking != null)
            {
                _unitOfWork.Holidays.Remove(booking);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                throw new ArgumentException($"Неможливо знайти відпустку з id (Guid){id}");
            }
        }
    }
}