using System;
using VTS.Core.Constants;

namespace VTS.Services.BookingService
{
    /// <summary>
    /// UnPaidDayOffs handler.
    /// </summary>
    public class UnPaidDayOffs : AbstractHandler
    {
        /// <inheritdoc />
        public override object Handle(Core.DTO.Holiday request)
        {
            if (request.Category == "UnPaidDayOffs")
            {
                var days = Convert.ToUInt32((request.Expires - request.Start).TotalDays);

                if (days > CompanyPolicy.MaxUnPaidDayOffs)
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
