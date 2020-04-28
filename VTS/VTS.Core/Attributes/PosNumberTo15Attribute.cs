using System.ComponentModel.DataAnnotations;

namespace VTS.Core.Attributes
{
    /// <summary>
    /// PosNumberTo15Attribute.
    /// </summary>
    public class PosNumberTo15Attribute : ValidationAttribute
    {
        /// <summary>
        /// Check is String valid.
        /// </summary>
        /// <param name="value">String value.</param>
        /// <returns>True if string valid.</returns>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            if (int.TryParse(value.ToString(), out int getal))
            {
                if (getal >= 0 && getal <= 15)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
