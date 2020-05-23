using System;
using VTS.Core.Constants;

namespace VTS.Services.BookingService
{
    /// <summary>
    /// PaidDayOffs handler.
    /// </summary>
    public class PaidDayOffs : AbstractHandler
    {
        /// <inheritdoc />
        public override object Handle(Core.DTO.Holiday request)
        {
            if (request.Category == "PaidDayOffs")
            {
                var days = Convert.ToUInt32((request.Expires - request.Start).TotalDays);

                if (days > CompanyPolicy.MaxPaidDayOffs)
                {
                    throw new ArgumentException($"Забагато днів відпустки за правилами компанії");
                }
                else
                {
                    return base.Handle(request);
                }
            }
            else
            {
                return base.Handle(request);
            }
        }
    }
}
