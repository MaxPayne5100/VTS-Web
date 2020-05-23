using System;
using VTS.Core.Constants;

namespace VTS.Services.BookingService
{
    /// <summary>
    /// PaidSickness handler.
    /// </summary>
    public class PaidSickness : AbstractHandler
    {
        /// <inheritdoc />
        public override object Handle(Core.DTO.Holiday request)
        {
            if (request.Category == "PaidSickness")
            {
                var days = Convert.ToUInt32((request.Expires - request.Start).TotalDays);

                if (days > CompanyPolicy.MaxPaidSickness)
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
