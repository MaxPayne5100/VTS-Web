using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using VTS.Repos.UnitOfWork;

namespace VTS.Services.BookingService
{
    /// <inheritdoc/>
    public class BookingService : IBookingService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        private string MapToEnum(string dtoCategory)
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

                bookingDto.Category = MapToEnum(bookingDto.Category);

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
            if (holidayaccDto.Status == "Прийняти")
            {
                holidayaccDto.Status = "Approved";
            }
            else if (holidayaccDto.Status == "Скасувати")
            {
                holidayaccDto.Status = "Canceled";
            }

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
        public async Task<IEnumerable<Core.DTO.Holiday>> FindAllBookingsByDate(DateTime? startDate)
        {
            var bookings = await _unitOfWork.Holidays.FindAllBookingsByDate(startDate);
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