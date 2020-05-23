using System;
using VTS.Core.Constants;

namespace VTS.Services.BookingService
{
    /// <summary>
    /// UnPaidSickness handler.
    /// </summary>
    public class UnPaidSickness : AbstractHandler
    {
        /// <inheritdoc />
        public override object Handle(Core.DTO.Holiday request)
        {
            if (request.Category == "UnPaidSickness")
            {
                var days = Convert.ToUInt32((request.Expires - request.Start).TotalDays);

                if (days > CompanyPolicy.MaxUnPaidSickness)
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
